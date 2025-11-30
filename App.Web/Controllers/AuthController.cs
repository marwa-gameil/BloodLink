using App.Application.DTOs;
using App.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthWebService _authWebService;

        public AuthController(AuthWebService authWebService)
        {
            _authWebService = authWebService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return View(loginDTO);

            
                var result = await _authWebService.LoginAsync(loginDTO);
                if (!result)
                {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(loginDTO);
                }
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Logout()
        {
            await _authWebService.LogoutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}
