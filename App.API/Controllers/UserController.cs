using App.Application.DTOs;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace App.API.Controllers
{
    [Authorize(Roles = "admin") ]
    public class UserController : ApiBaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
            => _userService = userService;
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll() =>
            HandleResult(await _userService.GetAll());

        [HttpGet("search")]
        public async Task<ActionResult<UserDTO>> GetByEmail([FromQuery,Required] string email) =>
            HandleResult(await _userService.GetByEmailAsync(email));

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeactivateUser(Guid id) =>
            HandleResult(await _userService.DeactivateAsync(id));

    }
}
