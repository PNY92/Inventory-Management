using Inventory_Management2.Models;
using Inventory_Management2.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Inventory_Management2.Interfaces
{
    public interface IWebClientRepository
    {
        public Task<List<ItemView>> GetItemsAsync();

        public Task<ItemView> GetItemByIdAsync(string itemId);

        public Task<List<IdentityUser>> GetAllUsersAsync();
        public Task<Status> AddItemAsync(ItemView item);

        public Task<Status> EditItemAsync(ItemView item);
        public Task<IdentityUser> GetUserByIdAsync(string id);

        public Task<Status> UpdateUserAsync(UserView user);

        public Task<Status> DeleteUserAsync(string id);

        public Task<Status> DeleteItemAsync(string id);
        public Task<Status> AddUserAsync(UserCreate user);
    }
}
