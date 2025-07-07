
using Inventory_Management2.Interfaces;
using Inventory_Management2.Models;
using Inventory_Management2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management2.Controllers.View
{
    [Authorize]
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly IWebClientRepository _webClientRepository;

        public HomeController(IWebClientRepository webClientRepository)
        {
            _webClientRepository = webClientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return View();
        }

        [HttpGet("Items")]
        public async Task<IActionResult> Items()
        {

            var items = await _webClientRepository.GetItemsAsync();
            return View(items);
        }


        [HttpGet("Details")]
        public async Task<IActionResult> Details(string id)
        {
            
            ItemView item = await _webClientRepository.GetItemByIdAsync(id);
            
            return View(item);
        }
    }
}
