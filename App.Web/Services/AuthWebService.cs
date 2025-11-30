using App.Application.DTOs;
using App.Application.Interfaces;

namespace App.Web.Services
{
    public class AuthWebService
    {
        private readonly ICookieAuthService _authService;

        public AuthWebService(ICookieAuthService authService)
        {
            _authService = authService;
        }
        public async Task<bool> LoginAsync(LoginDTO loginDTO)
        {
            var result = await _authService.LoginAsync(loginDTO);
            return result.Succeeded;
        }
        public async Task LogoutAsync()
        {
            await _authService.LogoutAsync();

        }

    }
}
