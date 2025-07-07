using Inventory_Management2.Models;
using Inventory_Management2.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Inventory_Management2.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<IdentityUser>> GetAllUsersAsync();

        public Task<IdentityUser> GetUserByIdAsync(string id);

        public Task<Status> AddUserAsync(UserCreate user);
        public Task<Status> UpdateUserAsync(IdentityUser user, string role);

        public Task<Status> DeleteUserAsync(string id);

        public Task<Status> SetRoleAsync(IdentityUser user, string role);
    }
}
