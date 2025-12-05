using App.Application.DTOs;

namespace App.Web.Services
{
    public class BloodBankService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BloodBankService(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<BloodBankDto>?> GetBloodBanksAsync(string Governorate)
        {
            if (string.IsNullOrWhiteSpace(Governorate))
                throw new ArgumentException("Governorate is required");

            return await _httpClient.GetFromJsonAsync<IEnumerable<BloodBankDto>>(
                $"api/bloodbank?Governorate={Governorate}");

        }
        public async Task<bool> AddBloodBankAsync(CreateBloodBankDto dto)
        {

            var request = new HttpRequestMessage(HttpMethod.Post, "api/BloodBank")
            {
                Content = JsonContent.Create(dto)
            };

            var cookie = _httpContextAccessor.HttpContext?.Request.Headers["Cookie"].ToString();
            if (!string.IsNullOrEmpty(cookie))
            {
                request.Headers.Add("Cookie", cookie);
            }

            var response = await _httpClient.SendAsync(request);

            Console.WriteLine("Request URL: " + _httpClient.BaseAddress + "api/BloodBank");
            Console.WriteLine("Status Code: " + response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response: " + content);

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