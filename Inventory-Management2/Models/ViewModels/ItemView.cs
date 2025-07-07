using Inventory_Management2.Models;

namespace Inventory_Management2.Models.ViewModels
{
    public class ItemView
    {
        public string? Id { get; set; }
        public string Name { get; set; } = "Untitled Item";
        public string Description { get; set; } = "No description";
        public string Category { get; set; } = "None";
        public decimal UnitPrice { get; set; } = 0;

        public uint StockQuantity { get; set; } = 0;
        public int LowStockThreshold { get; set; } = 10;
    }
}
