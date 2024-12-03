using Defender.Common.Errors;
using Defender.Common.Extension;
using Defender.UserManagementService.Application.Common.Interfaces.Services;
using Defender.UserManagementService.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Defender.UserManagementService.Application.Modules.Users.Queries;

public record GetUserByLoginQuery : IRequest<UserInfo>
{
    public string? Login { get; set; }
};

public sealed class GetUserByLoginQueryValidator : AbstractValidator<GetUserByLoginQuery>
{
    public GetUserByLoginQueryValidator()
    {
        RuleFor(s => s.Login)
            .NotEmpty()
            .WithMessage(ErrorCode.VL_USM_EmptyLogin);
    }
}

public class GetUserByLoginQueryHandler(
        IUserManagementService userManagementService)
    : IRequestHandler<GetUserByLoginQuery, UserInfo>
{
    public async Task<UserInfo> Handle(GetUserByLoginQuery query, CancellationToken cancellationToken)
    {
        var userInfo = await userManagementService.GetUserByLoginAsync(query.Login);

        return userInfo;
    }

}
