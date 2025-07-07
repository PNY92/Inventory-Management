using Inventory_Management.Data;
using Inventory_Management2.Interfaces;
using Inventory_Management2.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management2.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ItemContext _itemContext;
        public CategoryRepository(ItemContext itemContext)
        {
            _itemContext = itemContext;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _itemContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _itemContext.Categories.FindAsync(id);
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            List<Category> categoryList = await GetCategoriesAsync();

            return categoryList.FirstOrDefault(
                (Category category) => category.Name == name
            );
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _itemContext.Categories.AddAsync(category);
            await _itemContext.SaveChangesAsync();
        }
    }
}
