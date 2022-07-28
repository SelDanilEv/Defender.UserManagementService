using System.Net.Mail;
using Defender.UserManagement.Application.Common.Exceptions;
using Defender.UserManagement.Application.Common.Interfaces;
using Defender.UserManagement.Domain.Entities.User;
using Defender.UserManagement.Domain.Models;
using FluentValidation;
using MediatR;

namespace Defender.UserManagement.Application.Modules.Users.Commands;

public record UpdateAccountFromAdminCommand : IRequest<User>
{
    public User? User { get; set; }
};

public sealed class UpdateAccountFromAdminCommandValidator : AbstractValidator<UpdateAccountFromAdminCommand>
{
    public UpdateAccountFromAdminCommandValidator()
    {
        RuleFor(x => x.User).NotNull().WithMessage("No user info!");

        RuleFor(x => x.User.Id).NotNull().NotEmpty().WithMessage("User id is requirement!");

        RuleFor(x => x.User.Name).NotNull().NotEmpty().WithMessage("User name is requirement!");

        RuleFor(x => x.User.Email).NotNull().NotEmpty().WithMessage("User email is requirement!");

        RuleFor(x => x.User.Email).Matches(@"^\S+@\S+\.\S+$").WithMessage("Invalid email address!");

        RuleFor(x => x.User.Roles.Count).NotEqual(0).WithMessage("Users need at least one role!");
    }
}

public sealed class UpdateAccountFromAdminCommandHandler : IRequestHandler<UpdateAccountFromAdminCommand, User>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserManagementService _userManagementService;

    public UpdateAccountFromAdminCommandHandler(
        ICurrentUserService currentUserService,
        IUserManagementService userManagementService)
    {
        _currentUserService= currentUserService;
        _userManagementService = userManagementService;
    }

    public async Task<User> Handle(UpdateAccountFromAdminCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManagementService.GetUserByIdAsync(request.User.Id);

        if (user == null) throw new NotFoundException(nameof(User), request.User.Id);

        await ValidateAction(user, request.User);

        await UpdateFields(user, request.User);

        return user;
    }

    private async Task ValidateAction(User user, User newUser)
    {
        if (!_currentUserService.User.IsSuperAdmin)
        {
            if (user.HasRole(Roles.SuperAdmin))
            {
                throw new ForbiddenAccessException("update Super Admin", Roles.Admin);
            }

            if (user.HasRole(Roles.Admin) && _currentUserService.User.Id != user.Id)
            {
                throw new ForbiddenAccessException("update another Admin", Roles.Admin);
            }
        }
        else
        {
            var validRolesList = new List<string> { Roles.User, Roles.Admin, Roles.SuperAdmin };

            foreach (var role in newUser.Roles)
            {
                if (!validRolesList.Contains(role))
                {
                    throw new NotFoundException(nameof(Roles), role);
                }
            }
        }
    }

    private async Task UpdateFields(User user, User newUser)
    {
        user.Email = newUser.Email;
        user.Name = newUser.Name;

        if (_currentUserService.User.IsSuperAdmin)
        {
            user.Roles = newUser.Roles;
        }

        await _userManagementService.UpdateUserAsync(user);
    }

}
