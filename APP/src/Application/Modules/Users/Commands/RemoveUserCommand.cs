using Defender.UserManagement.Application.Common.Exceptions;
using Defender.UserManagement.Application.Common.Interfaces;
using Defender.UserManagement.Domain.Entities.User;
using Defender.UserManagement.Domain.Models;
using FluentValidation;
using MediatR;

namespace Defender.UserManagement.Application.Modules.Users.Commands;

public record RemoveUserCommand : IRequest
{
    public Guid Id { get; set; }
};

public sealed class RemoveUserCommandValidator : AbstractValidator<RemoveUserCommand>
{
    public RemoveUserCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("No user id!");
    }
}

public sealed class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserManagementService _userManagementService;

    public RemoveUserCommandHandler(
        ICurrentUserService currentUserService,
        IUserManagementService userManagementService)
    {
        _currentUserService = currentUserService;
        _userManagementService = userManagementService;
    }

    public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManagementService.GetUserByIdAsync(request.Id);

        await ValidateAction(user);

        await _userManagementService.RemoveUserAsync(request.Id);

        return Unit.Value;
    }

    private async Task ValidateAction(User user)
    {
        if (!_currentUserService.User.IsSuperAdmin)
        {
            if (user.HasRole(Roles.SuperAdmin))
            {
                throw new ForbiddenAccessException("delete Super Admin account", Roles.Admin);
            }

            if (user.HasRole(Roles.Admin) && _currentUserService.User.Id != user.Id)
            {
                throw new ForbiddenAccessException("delete another Admin account", Roles.Admin);
            }
        }
    }
}
