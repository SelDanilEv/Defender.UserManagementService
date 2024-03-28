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

public sealed class CreateUserCommandValidator
    : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(s => s.Email)
                  .NotEmpty()
                  .WithMessage(ErrorCodeHelper.GetErrorCode(
                      ErrorCode.VL_USM_EmptyEmail))
                  .EmailAddress()
                  .WithMessage(ErrorCodeHelper.GetErrorCode(
                      ErrorCode.VL_USM_InvalidEmail));

        //RuleFor(p => p.PhoneNumber)
        //          .Matches(ValidationConstants.PhoneNumberRegex).WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_USM_InvalidPhoneNumber));

        RuleFor(x => x.Nickname)
                  .NotEmpty()
                  .WithMessage(ErrorCodeHelper.GetErrorCode(
                      ErrorCode.VL_USM_EmptyNickname))
                  .MinimumLength(
            ValidationConstants.MinNicknameLength)
                  .WithMessage(ErrorCodeHelper.GetErrorCode(
                      ErrorCode.VL_USM_MinNicknameLength))
                  .MaximumLength(
            ValidationConstants.MaxNicknameLength)
                  .WithMessage(ErrorCodeHelper.GetErrorCode(
                      ErrorCode.VL_USM_MaxNicknameLength));
    }
}

public sealed class CreateUserCommandHandler(
        IUserManagementService userManagementService)
    : IRequestHandler<CreateUserCommand, UserInfo>
{
    public async Task<UserInfo> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userInfo = await userManagementService
            .CreateUserAsync(
                request.Email,
                request.PhoneNumber,
                request.Nickname);

        return userInfo;
    }
}
