using Inventory_Management2.Models;

namespace Inventory_Management2.Interfaces
{
    public interface IItemRepository
    {
        public Task<Item> GetItemByIdAsync(Guid id);
        public Task<List<Item>> GetItemsAsync();

        public Task<Status> AddItemAsync(Item item);

        public Task UpdateItemAsync(Item item);

        public Task DeleteItemAsync(Guid itemId);
    }
}
