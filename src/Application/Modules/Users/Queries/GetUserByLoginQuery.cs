using Defender.Common.Errors;
using Defender.UserManagementService.Application.Common.Interfaces;
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
                  .NotEmpty().WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_USM_EmptyLogin));
    }
}

public class GetUserByLoginQueryHandler : IRequestHandler<GetUserByLoginQuery, UserInfo>
{
    private readonly IUserManagementService _userManagementService;

    public GetUserByLoginQueryHandler(
        IUserManagementService userManagementService
        )
    {
        _userManagementService = userManagementService;
    }

    public async Task<UserInfo> Handle(GetUserByLoginQuery query, CancellationToken cancellationToken)
    {
        var userInfo = await _userManagementService.GetUsersByLoginAsync(query.Login);

        return userInfo;
    }

}
