using Defender.Common.DB.Pagination;
using Defender.UserManagementService.Application.Models;
using Defender.UserManagementService.Domain.Entities;

namespace Defender.UserManagementService.Application.Common.Interfaces;

public interface IUserManagementService
{
    Task<PagedResult<UserInfo>> GetUsersAsync(PaginationRequest paginationRequest);
    Task<UserInfo> GetUserByIdAsync(Guid userId);
    Task<UserInfo> GetUserByLoginAsync(string login);
    Task<bool> CheckIfEmailTakenAsync(string email);
    Task<UserInfo> CreateUserAsync(string email, string phoneNumber, string nickname);
    Task<UserInfo> UpdateUserAsync(UpdateUserInfoRequest updateRequest);

}
