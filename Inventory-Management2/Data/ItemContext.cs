using Inventory_Management2.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management.Data
{
    public class ItemContext : DbContext
    {

        public ItemContext(DbContextOptions<ItemContext> options): base(options)
        {

        }

        public DbSet<Item> Items { get; set; }

        public DbSet<Category> Categories { get; set; }

    }
}
