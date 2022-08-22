using Defender.UserManagement.Application.Common.Exceptions;
using Defender.UserManagement.Application.Common.Interfaces;
using Defender.UserManagement.Domain.Entities.User;
using Defender.UserManagement.Domain.Models;
using FluentValidation;
using MediatR;

namespace Defender.UserManagement.Application.Modules.Users.Commands;

public record UpdateAccountFromUserCommand : IRequest<User>
{
    public User? User { get; set; }
};

public sealed class UpdateAccountFromUserCommandValidator : AbstractValidator<UpdateAccountFromUserCommand>
{
    public UpdateAccountFromUserCommandValidator()
    {
        RuleFor(x => x.User).NotNull().WithMessage("No user info!");

        RuleFor(x => x.User.Id).NotNull().NotEmpty().WithMessage("User id is requirement!");

        RuleFor(x => x.User.Name).NotNull().NotEmpty().WithMessage("User name is requirement!");

        RuleFor(x => x.User.Email).NotNull().NotEmpty().WithMessage("User email is requirement!");
    }
}

public sealed class UpdateAccountFromUserCommandHandler : IRequestHandler<UpdateAccountFromUserCommand, User>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserManagementService _userManagementService;

    public UpdateAccountFromUserCommandHandler(
        ICurrentUserService currentUserService,
        IUserManagementService userManagementService)
    {
        _currentUserService = currentUserService;
        _userManagementService = userManagementService;
    }

    public async Task<User> Handle(UpdateAccountFromUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManagementService.GetUserByIdAsync(request.User.Id);

        if (user == null) throw new NotFoundException(nameof(User), request.User.Id);

        await ValidateAction(user);

        await UpdateFields(user, request.User);

        return user;
    }

    private async Task ValidateAction(User user)
    {
        if (_currentUserService.User.Id != user.Id)
        {
            throw new ForbiddenAccessException("update another User", Roles.User);
        }
    }

    private async Task UpdateFields(User user, User newUser)
    {
        user.Email = newUser.Email;
        user.Name = newUser.Name;

        await _userManagementService.UpdateUserAsync(user);
    }

}
