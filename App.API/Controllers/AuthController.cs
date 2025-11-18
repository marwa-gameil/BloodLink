using App.Application.DTOs;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class AuthController :ApiBaseController
    {
        private readonly ICookieAuthService _authService;
        public AuthController(ICookieAuthService authService)
            => _authService = authService;

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginDTO loginDTO) =>
            HandleResult(await _authService.LoginAsync(loginDTO));

        [HttpPost("logout")]
        public async Task<ActionResult> Logout() =>
            HandleResult(await _authService.LogoutAsync());

      

    }
}
