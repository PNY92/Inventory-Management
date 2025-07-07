using Inventory_Management2.Interfaces;
using Inventory_Management2.Models;
using Inventory_Management2.Models.ViewModels;
using Inventory_Management2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Inventory_Management2.Controllers.View
{
    [Route("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly IWebClientRepository _webClient;

        public AdminController(IWebClientRepository webClient)
        {
            _webClient = webClient;
        }

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem(ItemView itemView)
        {
            Status status = await _webClient.AddItemAsync(itemView);

            return RedirectToAction("Manage", "Admin");
        }

        [HttpPost("EditItem")]
        public async Task<IActionResult> EditItem(ItemView itemView)
        {
            Status status = await _webClient.EditItemAsync(itemView);
            TempData["ToastMessage"] = status.Description;
            if (status.Code == Enum.StatusCode.STATUS_SUCCESS_CODE)
            {
                
                return RedirectToAction("Manage", "Admin");
            }
            
            return BadRequest(status);
        }

        [HttpDelete("DeleteItem/{id}")]

        public async Task<IActionResult> DeleteItem(string id)
        {
            Status status = await _webClient.DeleteItemAsync(id);
            TempData["ToastMessage"] = status.Description;
            if (status.Code == Enum.StatusCode.STATUS_SUCCESS_CODE)
            {

                return RedirectToAction("Manage", "Admin");
            }

            return BadRequest(status);
        }

        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserView userView)
        {
            
            
            Status status = await _webClient.UpdateUserAsync(userView);
            TempData["ToastMessage"] = status.Description;
            if (status.Code == Enum.StatusCode.STATUS_SUCCESS_CODE)
            {

                return RedirectToAction("Users", "Admin");
            }

            return BadRequest(status);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {


            Status status = await _webClient.DeleteUserAsync(id);
            TempData["ToastMessage"] = status.Description;
            if (status.Code == Enum.StatusCode.STATUS_SUCCESS_CODE)
            {

                return RedirectToAction("Users", "Admin");
            }

            return BadRequest(status);
        }

        [HttpPost("AddUserAction")]
        public async Task<IActionResult> AddUserAction(UserView userView)
        {
            IdentityUser user = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userView.UserName,
                Email = userView.Email,

            };
            user.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(user, userView.Password);

            UserCreate newUser = new UserCreate()
            {
                User = user,
                Role = userView.Role
            };
            Status status = await _webClient.AddUserAsync(newUser);
            TempData["ToastMessage"] = status.Description;
            if (status.Code == Enum.StatusCode.STATUS_SUCCESS_CODE)
            {

                return RedirectToAction("Users", "Admin");
            }

            return BadRequest(status);
        }

        [HttpGet("Users")]
        public async Task<IActionResult> Users()
        {
            List<IdentityUser> users = await _webClient.GetAllUsersAsync();
            return View(users);
        }

        [HttpGet("AddUser")]
        public async Task<IActionResult> AddUser()
        {
            return View();
        }

        [HttpGet("Manage")]
        public async Task<IActionResult> Manage()
        {
            List<ItemView> items = await _webClient.GetItemsAsync();
            return View(items);
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(string id)
        {

            ItemView item = await _webClient.GetItemByIdAsync(id);

            return View(item);
        }

        [HttpGet("AddItem")]
        public async Task<IActionResult> AddItem()
        {
            return View();
        }

        [HttpGet("ViewUser")]

        public async Task<IActionResult> ViewUser(string id)
        {
            IdentityUser user = await _webClient.GetUserByIdAsync(id);
            return View(user);
        }

    }
}
