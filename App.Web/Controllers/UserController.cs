using App.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserWebService _userService;

        public UserController(UserWebService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeactivateUser(Guid id)
        {
            var success = await _userService.DeactivateUserAsync(id);
            if (!success)
            {
                return BadRequest("Failed to deactivate user.");
            }
            return RedirectToAction(nameof(GetAll));
        }

    }
}
