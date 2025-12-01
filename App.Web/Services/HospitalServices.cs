using App.Application.DTOs;

namespace App.Web.Services
{
    public class HospitalServices
    {
        private readonly HttpClient _httpClient;
        public HospitalServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateBloodRequestAsync(CreateRequestDto createRequestDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/hospital/requests", createRequestDto);
            return response.IsSuccessStatusCode;
        }
        public async Task<IEnumerable<RequestDto>?> GetHospitalRequestsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<RequestDto>>(
                $"api/hospital/requests");
        }
        public async Task<bool> CompleteRequestAsync(int requestId)
        {
            var response = await _httpClient.PutAsync(
                $"api/hospital/requests/{requestId}/complete",
                null
            );
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> AddHospitalAsync(CreateHospitalDto createHospitalDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/hospital", createHospitalDto);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> CancelRequestAsync(int requestId)
        {
            var response = await _httpClient.DeleteAsync(
                $"api/hospital/requests/{requestId}/cancle"
            );
            return response.IsSuccessStatusCode;
        }
    }
}
