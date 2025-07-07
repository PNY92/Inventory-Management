using Inventory_Management.Data;
using Inventory_Management2.Interfaces;
using Inventory_Management2.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management2.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ItemContext _itemContext;
        public ItemRepository(ItemContext itemContext) {
            _itemContext = itemContext;
        }

        public async Task<Status> AddItemAsync(Item item)
        {
            var existingItem = await _itemContext.Items.FindAsync(item.Id);

            if (existingItem == null)
            {
                try
                {
                    if (item.UnitPrice < 0)
                    {
                        item.UnitPrice = 0;
                    }

                    if (item.LowStockThreshold < 0)
                    {
                        item.LowStockThreshold = 0;
                    }


                    await _itemContext.AddAsync(item);
                    await _itemContext.SaveChangesAsync();
                    return new Status()
                    {
                        Code = Enum.StatusCode.STATUS_SUCCESS_CODE,
                        Description = "Successfully added the item"
                    };
                }
                catch
                {
                    return new Status() {
                        Code = Enum.StatusCode.STATUS_FAILED_CODE,
                        Description = "An error has occured while making query to database"
                    };
                }
            }


            return new Status()
            {
                Code = Enum.StatusCode.STATUS_FAILED_CODE,
                Description = "Found duplicated ID from database, consider changing the `Item.Id`"
            };
        }

        public async Task<Item> GetItemByIdAsync(Guid id)
        {
            return await _itemContext.Items
                .Include(x => x.Category)
                .FirstOrDefaultAsync(y => y.Id == id);
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            return await _itemContext.Items
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task DeleteItemAsync(Guid itemId)
        {
            var existingItem = await _itemContext.Items.FindAsync(itemId);
            if (existingItem != null)
            {
                _itemContext.Items.Remove(existingItem);
                await _itemContext.SaveChangesAsync();
            }
        }


        public async Task UpdateItemAsync(Item item)
        {

            if (item.UnitPrice < 0)
            {
                item.UnitPrice = 0;
            }

            if (item.LowStockThreshold < 0)
            {
                item.LowStockThreshold = 0;
            }

            _itemContext.Items.Update(item);
            await _itemContext.SaveChangesAsync();


        }
    }
}
