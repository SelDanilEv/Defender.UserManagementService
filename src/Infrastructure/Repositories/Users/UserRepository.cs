using Defender.UserManagement.Application.Common.Interfaces.Repositories;
using Defender.UserManagement.Application.Configuration.Options;
using Defender.UserManagement.Domain.Entities.User;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Defender.UserManagement.Infrastructure.Repositories.Users;

public class UserRepository : MongoRepository<User>, IUserRepository
{
    public UserRepository(IOptions<MongoDbOption> mongoOption) : base(mongoOption.Value)
    {
    }

    public async Task<IList<User>> GetAllUsersAsync()
    {
        return await GetItemsAsync();
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        return await GetItemAsync(id);
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var emailFilter = CreateFilterDefinition<string>(x => x.Email, email);

        var users = await GetItemsWithFilterAsync(emailFilter);

        return users.FirstOrDefault();
    }

    public async Task<User> CreateUserAsync(User user)
    {
        return await AddItemAsync(user);
    }

    public async Task<User> UpdateUserAsync(User updatedUser)
    {
        return await UpdateItemAsync(updatedUser);
    }

    public async Task RemoveUserAsync(Guid id)
    {
        await RemoveItemAsync(id);
    }


}
