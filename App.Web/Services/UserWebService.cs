using App.Application.DTOs;

namespace App.Web.Services
{
    public class UserWebService
    {
        private readonly HttpClient _httpClient;

        public UserWebService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<UserDTO>?> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<UserDTO>>("api/user");
        }
        public async Task<UserDTO?> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required");

            return await _httpClient.GetFromJsonAsync<UserDTO>(
                $"api/user/search?email={Uri.EscapeDataString(email)}");
        }
        public async Task<bool> DeactivateUserAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/user/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<UserDTO?> AddUserAsync(AddUserDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/Add", dto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDTO>();
            }
            return null;
        }

    }
}