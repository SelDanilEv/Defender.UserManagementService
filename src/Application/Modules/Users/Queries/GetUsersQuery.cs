using Defender.Common.DB.Pagination;
using Defender.UserManagementService.Application.Common.Interfaces;
using Defender.UserManagementService.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Defender.UserManagementService.Application.Modules.Users.Queries;

public record GetUsersQuery : PaginationRequest, IRequest<PagedResult<UserInfo>>
{
};

public sealed class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersQueryValidator()
    {

    }
}

public class GetUsersQueryHandler(
        IUserManagementService userManagementService) 
    : IRequestHandler<GetUsersQuery, PagedResult<UserInfo>>
{
    public async Task<PagedResult<UserInfo>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await userManagementService.GetUsersAsync(query);

        return users;
    }

}
