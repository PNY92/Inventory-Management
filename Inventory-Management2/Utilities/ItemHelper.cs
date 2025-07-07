using Inventory_Management2.Interfaces;
using Inventory_Management2.Models;
using Inventory_Management2.Models.ViewModels;

namespace Inventory_Management2.Utilities
{
    public class ItemHelper
    {
        public static ItemView ConvertModelToView(Item item)
        {
            ItemView newItemView = new ItemView {
                Id = item.Id.ToString(),
                Name = item.Name,
                Category = item.Category.Name,
                Description = item.Description,
                LowStockThreshold = item.LowStockThreshold,
                StockQuantity = item.StockQuantity,
                UnitPrice = item.UnitPrice
            };
            
            return newItemView;
        }

        public static Item ConvertViewToModel(ItemView itemView, Category category)
        {
            Item newItem = new Item
            {
                Id = Guid.NewGuid(),
                Name = itemView.Name,
                Description = itemView.Description,
                Category = category,
                LowStockThreshold = itemView.LowStockThreshold,
                StockQuantity = itemView.StockQuantity,
                UnitPrice = itemView.UnitPrice

            };

            return newItem;
        }
    }
}
