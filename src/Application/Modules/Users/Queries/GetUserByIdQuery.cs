using Defender.Common.Errors;
using Defender.UserManagementService.Application.Common.Interfaces;
using Defender.UserManagementService.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Defender.UserManagementService.Application.Modules.Users.Queries;

public record GetUserByIdQuery : IRequest<UserInfo>
{
    public Guid UserId { get; set; }
};

public sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(s => s.UserId)
                  .NotEmpty().WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_USM_EmptyUserId));
    }
}

public class GetUserByIdQueryHandler(
        IUserManagementService userManagementService) 
    : IRequestHandler<GetUserByIdQuery, UserInfo>
{
    public async Task<UserInfo> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var userInfo = await userManagementService.GetUserByIdAsync(query.UserId);

        return userInfo;
    }

}
