using Defender.UserManagement.Application.Common.Interfaces;
using Defender.UserManagement.Domain.Entities.User;
using FluentValidation;
using MediatR;

namespace Defender.UserManagement.Application.Modules.Users.Queries;

public record GetAllUsersQuery : IRequest<IList<User>>
{
};

public sealed class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
{
    public GetAllUsersQueryValidator()
    {
    }
}

public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IList<User>>
{
    private readonly IUserManagementService _userManagementService;

    public GetAllUsersQueryHandler(
        IUserManagementService userManagementService)
    {
        _userManagementService = userManagementService;
    }

    public async Task<IList<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userManagementService.GetAllUsersAsync();
    }
}
