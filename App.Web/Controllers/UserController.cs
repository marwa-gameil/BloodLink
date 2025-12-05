using App.Application.DTOs;
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

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var user = await _userService.AddUserAsync(dto);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to add user.");
                return View(dto);
            }
            return RedirectToAction(nameof(GetAll));
        }

    }
}