using Defender.Common.Errors;
using Defender.Common.Extension;
using Defender.UserManagementService.Application.Common.Interfaces.Services;
using FluentValidation;
using MediatR;

namespace Defender.UserManagementService.Application.Modules.Users.Queries;

public record CheckIsEmailTakenQuery : IRequest<bool>
{
    public string? Email { get; set; }
};

public sealed class IsEmailTakenQueryValidator
    : AbstractValidator<CheckIsEmailTakenQuery>
{
    public IsEmailTakenQueryValidator()
    {
        RuleFor(s => s.Email)
                  .NotEmpty().WithMessage(
            ErrorCodeHelper.GetErrorCode(ErrorCode.VL_USM_EmptyEmail));
    }
}

public class IsEmailTakenQueryHandler(
        IUserManagementService userManagementService)
    : IRequestHandler<CheckIsEmailTakenQuery, bool>
{
    public async Task<bool> Handle(CheckIsEmailTakenQuery query, CancellationToken cancellationToken)
    {
        var isEmailTaken = await userManagementService
            .CheckIfEmailTakenAsync(query.Email);

        return isEmailTaken;
    }

}
