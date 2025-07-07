using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_Management2.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }

        public decimal UnitPrice { get; set; } = 0;
        public uint StockQuantity { get; set; }
        public int LowStockThreshold { get; set; } = 10;
    }
}
