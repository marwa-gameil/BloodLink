using App.Application.DTOs;

namespace App.Web.Services
{
    public class BloodBankService
    {
        private readonly HttpClient _httpClient;
        public BloodBankService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<BloodBankDto>?> GetBloodBanksAsync(string Governorate)
        {
            if (string.IsNullOrWhiteSpace(Governorate))
                throw new ArgumentException("Governorate is required");

            return await _httpClient.GetFromJsonAsync<IEnumerable<BloodBankDto>>(
                $"api/bloodbank?Governorate={Governorate}");

        }
        public async Task<bool> AddBloodBankAsync(CreateBloodBankDto createBloodBankDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/bloodbank", createBloodBankDto);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> ApproveRequestAsync(int requestId)
        {
            var response = await _httpClient.PutAsync(
                $"api/bloodbank/requests/{requestId}/approve",
                null
            );
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RejectRequestAsync(int requestId)
        {
            var response = await _httpClient.PutAsync(
                $"api/bloodbank/requests/{requestId}/reject",
                null
            );
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<RequestDto>> GetAllRequestsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<RequestDto>>("api/bloodbank/requests");
        }
        public async Task<bool> StockIncreamentAsync(StockIncreamentDto stockIncreamentDto)
        {
            var response = await _httpClient.PutAsJsonAsync("api/bloodbank/stock/increment", stockIncreamentDto);
            return response.IsSuccessStatusCode;
        }
        public async Task<IEnumerable<StockDto>?> GetStockDetailsAsync(string bloodType = null)
        {
            var url = "api/bloodbank/stock";
            if (!string.IsNullOrWhiteSpace(bloodType))
                url += $"?bloodType={bloodType}";
            return await _httpClient.GetFromJsonAsync<IEnumerable<StockDto>>(url);
        }
        



    }
}
