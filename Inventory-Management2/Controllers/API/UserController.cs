using Inventory_Management2.Interfaces;
using Inventory_Management2.Models;
using Inventory_Management2.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Inventory_Management2.Controllers.API
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        protected readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<IdentityUser> users = await _userRepository.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            IdentityUser user = await _userRepository.GetUserByIdAsync(userId);

            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserView userView)
        {
            IdentityUser user = new IdentityUser()
            {
                Email = userView.Email,
                UserName = userView.UserName,
                Id = userView.Id
            };

           

            Status status = await _userRepository.UpdateUserAsync(user, userView.Role);
            return Ok(status);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
           
            Status status = await _userRepository.DeleteUserAsync(userId);
            return Ok(status);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserCreate user)
        {
            

            Status status = await _userRepository.AddUserAsync(user);

            return Ok(status);
        }

        

    }
}
