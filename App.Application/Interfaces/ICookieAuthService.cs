using App.Domain.Responses;
using App.Application.DTOs;



namespace App.Application.Interfaces
{
    public interface ICookieAuthService
    {

        Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO);
        Task<Result> LogoutAsync();
    }
}
