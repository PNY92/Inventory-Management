using Inventory_Management2.Interfaces;
using Inventory_Management2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management2.Controllers.Model
{
    [Route("/api")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("categories")]
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _categoryRepository.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}
