using Inventory_Management2.Models;

namespace Inventory_Management2.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<Category> GetCategoryByIdAsync(Guid id);

        public Task<Category> GetCategoryByNameAsync(string name);
        public Task<List<Category>> GetCategoriesAsync();

        public Task AddCategoryAsync(Category category);
    }
}
