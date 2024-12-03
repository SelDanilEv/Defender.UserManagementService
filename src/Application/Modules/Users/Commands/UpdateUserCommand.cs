using Defender.Common.Errors;
using Defender.Common.Exceptions;
using Defender.Common.Extension;
using Defender.Common.Helpers;
using Defender.Common.Interfaces;
using Defender.UserManagementService.Application.Common.Interfaces.Services;
using Defender.UserManagementService.Application.Models;
using Defender.UserManagementService.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Defender.UserManagementService.Application.Modules.Users.Commands;

public class UpdateUserCommand : UpdateUserInfoRequest, IRequest<UserInfo>
{
    public int? Code { get; set; }
}

public sealed class UpdateUserCommandValidator
    : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(s => s.Id)
            .NotEmpty()
            .WithMessage(ErrorCode.VL_USM_EmptyUserId);

        RuleFor(s => s.Email)
            .EmailAddress()
            .When(command => !string.IsNullOrEmpty(command.Email))
            .WithMessage(ErrorCode.VL_USM_InvalidEmail);

        //RuleFor(p => p.PhoneNumber)
        //          .Matches(ValidationConstants.PhoneNumberRegex)
        //          .WithMessage(ErrorCode.VL_USM_InvalidPhoneNumber));

        RuleFor(x => x.Nickname)
            .MinimumLength(ValidationConstants.MinNicknameLength)
            .When(command => !string.IsNullOrEmpty(command.Nickname))
                .WithMessage(ErrorCode.VL_USM_MinNicknameLength)
            .MaximumLength(ValidationConstants.MaxNicknameLength)
                .WithMessage(ErrorCode.VL_USM_MaxNicknameLength);

        RuleFor(command => command)
            .Must(command => !string.IsNullOrEmpty(command.Email)
            || !string.IsNullOrEmpty(command.PhoneNumber)
            || !string.IsNullOrEmpty(command.Nickname))
                .WithMessage(ErrorCode.VL_USM_AtLeastOneFieldRequired);
    }
}

public sealed class UpdateUserCommandHandler(
        ICurrentAccountAccessor currentAccountAccessor,
        IUserManagementService userManagementService,
        IAccessCodeService accessCodeService,
        IAuthorizationCheckingService authorizationCheckingService)
    : IRequestHandler<UpdateUserCommand, UserInfo>
{
    public async Task<UserInfo> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        if (!RolesHelper.IsAdmin(currentAccountAccessor.GetRoles()))
        {
            if (request.Code.HasValue)
            {
                var isCodeValid = await accessCodeService
                    .VerifyUpdateUserAccessCodeAsync(request.Code.Value);

                if (!isCodeValid)
                    throw new ServiceException(ErrorCode.BR_ACC_InvalidAccessCode);
            }
            else
            {
                request.AsUser();
            }
        }

        var userInfo = await authorizationCheckingService.ExecuteWithAuthCheckAsync(
            request.Id,
            async () => await userManagementService.UpdateUserAsync(request));

        return userInfo;
    }
}
