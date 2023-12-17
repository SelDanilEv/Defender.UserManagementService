using Defender.Common.Errors;
using Defender.UserManagementService.Application.Common.Interfaces;
using Defender.UserManagementService.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Defender.UserManagementService.Application.Modules.Users.Commands;

public record CreateUserCommand : IRequest<UserInfo>
{
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Nickname { get; set; }
};

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(s => s.Email)
                  .NotEmpty().WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_ACC_EmptyEmail))
                  .EmailAddress().WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_ACC_InvalidEmail));

        RuleFor(p => p.PhoneNumber)
                  .Matches(ValidationConstants.PhoneNumberRegex).WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_ACC_InvalidPhoneNumber));

        RuleFor(x => x.Nickname)
                  .NotEmpty().WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_ACC_EmptyNickname))
                  .MinimumLength(ValidationConstants.MinNicknameLength).WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_ACC_MinNicknameLength))
                  .MaximumLength(ValidationConstants.MaxNicknameLength).WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_ACC_MaxNicknameLength));
    }
}

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserInfo>
{
    private readonly IUserManagementService _userManagementService;

    public CreateUserCommandHandler(
        IUserManagementService userManagementService
        )
    {
        _userManagementService = userManagementService;
    }

    public async Task<UserInfo> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userInfo = await _userManagementService.CreateUserAsync(request.Email, request.PhoneNumber, request.Nickname);

        return userInfo;
    }
}
