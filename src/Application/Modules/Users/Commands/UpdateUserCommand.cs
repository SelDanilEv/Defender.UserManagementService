using Defender.Common.DB.Model;
using Defender.Common.Errors;
using Defender.UserManagementService.Application.Common.Interfaces;
using Defender.UserManagementService.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Defender.UserManagementService.Application.Modules.Users.Commands;

public record UpdateUserCommand : IRequest<UserInfo>
{
    public Guid UserId { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Nickname { get; set; }

    public UpdateModelRequest<UserInfo> CreateUpdateRequest()
    {
        var updateRequest = UpdateModelRequest<UserInfo>.Init(UserId)
            .UpdateFieldIfNotNull(x => x.Email, Email)
            .UpdateFieldIfNotNull(x => x.PhoneNumber, PhoneNumber)
            .UpdateFieldIfNotNull(x => x.Nickname, Nickname);

        return updateRequest;
    }

    public UserInfo ToUserInfo()
    {
        return new UserInfo
        {
            Id = UserId,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Nickname = Nickname,
        };
    }
};

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(s => s.UserId)
            .NotEmpty()
                .WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_USM_EmptyUserId));

        RuleFor(s => s.Email)
            .EmailAddress()
            .When(command => !string.IsNullOrEmpty(command.Email))
                .WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_USM_InvalidEmail));

        //RuleFor(p => p.PhoneNumber)
        //          .Matches(ValidationConstants.PhoneNumberRegex).WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_USM_InvalidPhoneNumber));

        RuleFor(x => x.Nickname)
            .MinimumLength(ValidationConstants.MinNicknameLength)
            .When(command => !string.IsNullOrEmpty(command.Nickname))
              .WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_USM_MinNicknameLength))
            .MaximumLength(ValidationConstants.MaxNicknameLength)
              .WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_USM_MaxNicknameLength));

        RuleFor(command => command)
            .Must(command => !string.IsNullOrEmpty(command.Email)
            || !string.IsNullOrEmpty(command.PhoneNumber)
            || !string.IsNullOrEmpty(command.Nickname))
                .WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_USM_AtLeastOneFieldRequired));
    }
}

public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserInfo>
{
    private readonly IUserManagementService _userManagementService;

    public UpdateUserCommandHandler(
        IUserManagementService userManagementService
        )
    {
        _userManagementService = userManagementService;
    }

    public async Task<UserInfo> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userInfo = await _userManagementService.UpdateUserAsync(request);

        return userInfo;
    }
}
