using Inventory_Management2.Interfaces;
using Inventory_Management2.Models;
using Inventory_Management2.Repositories;
using Inventory_Management2.Utilities;
using Inventory_Management2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Inventory_Management2.Controllers.Model
{

    [Route("api")]
    public class ItemController : Controller
    {

        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMailRepository _mailRepository;

        public ItemController(IItemRepository itemRepository,
            ICategoryRepository categoryRepository,
            IMailRepository mailRepository)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
            _mailRepository = mailRepository;
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetItems()
        {
            List<Item> items = await _itemRepository.GetItemsAsync();

            List<ItemView> itemViews = new List<ItemView>();

            items.ForEach((item) =>
            {
                ItemView newItemView = ItemHelper.ConvertModelToView(item);
                itemViews.Add(newItemView);
            });



            
            

            return Ok(itemViews);

        }

        [HttpGet("items/{itemId}")]
        public async Task<IActionResult> GetItemById(string itemId)
        {
            Item item = await _itemRepository.GetItemByIdAsync(Guid.Parse(itemId));
            ItemView itemView = ItemHelper.ConvertModelToView(item);
            return Ok(itemView);
        }

        [HttpPost("items")]
        public async Task<IActionResult> UploadItem([FromBody] ItemView item)
        {
            Category itemCategory = await _categoryRepository.GetCategoryByNameAsync(item.Category);

            if (itemCategory == null)
            {

                Category newCategory = new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = item.Category,
                };
                await _categoryRepository.AddCategoryAsync(newCategory);
                itemCategory = newCategory;
            }

                        
            Item itemModel = ItemHelper.ConvertViewToModel(item, itemCategory);

            Status status = await _itemRepository.AddItemAsync(itemModel);

            if (status.Code == Enum.StatusCode.STATUS_SUCCESS_CODE)
            {
                return Ok(status);
            }
            return BadRequest(status);
        }

        [HttpPut("items/{itemId}")]
        public async Task<IActionResult> UpdateItem(string itemId, [FromBody] ItemView item)
        {
            Category itemCategory = await _categoryRepository.GetCategoryByNameAsync(item.Category);

            if (itemCategory == null)
            {

                Category newCategory = new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = item.Category,
                };
                await _categoryRepository.AddCategoryAsync(newCategory);
                itemCategory = newCategory;
            }

            Item itemModel = ItemHelper.ConvertViewToModel(item, itemCategory);

            itemModel.Id = Guid.Parse(item.Id);

            await _itemRepository.UpdateItemAsync(itemModel);

            if (itemModel.StockQuantity < itemModel.LowStockThreshold)
            {
                _mailRepository.NotifyEveryone(new MailRequest()
                {
                    Body = $"{itemModel.Name}'s quantity is lower than low stock threshold",
                    Subject = $"{itemModel.Name} Low Stock Threshold Alert"
                });
            }

            

            return Ok(itemModel);
        }

        [HttpDelete("items/{itemId}")]
        public async Task<IActionResult> DeleteItem(string itemId)
        {
            Item item = await _itemRepository.GetItemByIdAsync(Guid.Parse(itemId));

            if (item == null) return BadRequest("Unable to find the item before running delete query");

            await _itemRepository.DeleteItemAsync(Guid.Parse(itemId));

            return Ok();
        }
    }
}
