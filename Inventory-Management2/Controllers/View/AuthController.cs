using Inventory_Management2.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management2.Controllers.View
{
    public class AuthController : Controller
    {

        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public async Task<IActionResult> SignInAction(Login LoginModel)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(LoginModel.Username, LoginModel.Password, false, false);

            if (signInResult.Succeeded) {
                TempData["ToastMessage"] = "Successfully logged in";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ToastMessage"] = "Failed to sign in";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
