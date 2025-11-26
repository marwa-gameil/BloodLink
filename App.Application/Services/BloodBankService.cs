using App.Application.DTOs;
using App.Application.Interfaces;
using App.Application.Utilities;
using App.Domain.Interfaces;
using App.Domain.Models;
using App.Domain.Responses;
using Microsoft.AspNetCore.Identity;


namespace App.Application.Services
{
    public class BloodBankService : IBloodBankService
    {
        private readonly IBloodBankRepository _bloodBankRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<User> _userManager;
        private readonly ICurrentLoggedInUser _currentLoggedInUser;
        private readonly IRequestRepository _requestRepository;
        public BloodBankService(IBloodBankRepository bloodBankRepository,
            IStockRepository stockRepository,
            UserManager<User> userManager,
            ICurrentLoggedInUser user,
            IRequestRepository requestRepository
            )
        {
            _bloodBankRepository = bloodBankRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
            _currentLoggedInUser = user;
            _requestRepository = requestRepository;
        }
        public async Task<Result<IEnumerable<BloodBankDto>>> GetAllBloodBanksAsync(string Governorate)
        {
            var bank = await _bloodBankRepository.GetAllAsync(Governorate);
            return Result.Success(bank.ConvertAll(b => new BloodBankDto
            {
                Id = b.UserId,
                Name = b.User.Name,
                Address = b.User.Address,
                ContactNumber = b.User.PhoneNumber,
                Email = b.User.Email,
                Governorate = b.User.Governorate

            }
            ));

        }

        public async Task<Result> AddBloodBankAsync(CreateBloodBankDto CreateBloodBankDto)
        {

            var newUser = new User
            {
                UserName = CreateBloodBankDto.Email, //check
                Name = CreateBloodBankDto.Name,
                Address = CreateBloodBankDto.Address,
                Governorate = CreateBloodBankDto.Governorate,
                PhoneNumber = CreateBloodBankDto.PhoneNumber,
                Email = CreateBloodBankDto.Email,
                CreatedAt = DateTime.UtcNow,
                CreatedById = Guid.Parse(_currentLoggedInUser.UserId)

            };
            var createUserResult = await _userManager.CreateAsync(newUser, CreateBloodBankDto.Password);
            if (!createUserResult.Succeeded)
            {
                return Result.Fail(new Response(
                    400,
                    string.Join(", ", createUserResult.Errors.Select(e => e.Description))
                ));
            }
            await _userManager.AddToRoleAsync(newUser, "bloodbank");

            var newBank = new BloodBank

            {
                UserId = newUser.Id,
                Latitude = CreateBloodBankDto.Latitude,
                Longitude = CreateBloodBankDto.Longitude,
                LicenseNumber = CreateBloodBankDto.LicenseNumber,
            };
            await _bloodBankRepository.AddAsync(newBank);
            await _bloodBankRepository.SaveAsync();


            foreach (var blood in Enum.GetValues<BloodType>())
            {
                var stock = new Stock
                {
                    BloodBankId = newBank.UserId,
                    BloodType = blood,
                    Quantity = 0,
                    CreatedAt = DateTime.UtcNow
                };
                await _stockRepository.AddAsync(stock);

            }
            await _stockRepository.SaveAsync();



            return Result.Success(new Response(201, "Blood Bank and Stock created successfully"));
        }

    


/*
        public async Task<Result> DeleteBloodBankAsync(Guid id)
        {
            var existingBank = await _bloodBankRepository.GetByIdAsync(id);
            if (existingBank == null)
            {
                return Result.Fail(new Response(404, "Blood Bank not found"));
            }
           
            return Result.Success(new Response(204, "Blood Bank deleted successfully"));
        }
*/

        public async Task<Result> UpdateRqeuestStatusAsync(int requestId,BloodRequestStatus bloodRequestStatus)
        {
            var currentUser = await _currentLoggedInUser.GetUser();
            
            var bloodRequest = await _requestRepository.GetByIdAsync(requestId);

            if (bloodRequest is null)
            {
                return Result.Fail(new Response(404, "Request not found"));
            }
            if((bloodRequest.BloodBankId != currentUser.Id))
            {
                return Result.Fail(new Response(403, "You are not authorized to confirm this request"));
            }
            if(bloodRequest.Status != BloodRequestStatus.Pending)
            {
                return Result.Fail(new Response(400, "Only pending requests can be confirmed"));
            }

            if (bloodRequestStatus != BloodRequestStatus.Approved &&
                bloodRequestStatus != BloodRequestStatus.Rejected)
            {
                return Result.Fail(new Response(400, "Blood bank can only Approve or Reject pending requests"));
            }

            if(bloodRequestStatus == BloodRequestStatus.Approved)
            {
                var stock = await _stockRepository.GetOneAsync(bloodRequest.BloodBankId, bloodRequest.BloodType);
                if(stock.Quantity < bloodRequest.Quantity)
                {
                    return Result.Fail(new Response(400, "Insufficient stock to approve the request"));
                }
              
            }

            bloodRequest.Status = bloodRequestStatus;
            _requestRepository.UpdateAsync(bloodRequest);
            await _requestRepository.SaveAsync();
            return Result.Success(new Response(200, "Request status updated successfully"));
        }

        public async Task<Result<IEnumerable<RequestDto>>> GetAllRequests()
        {
            var requests = await _requestRepository.GetAllAsync(Guid.Parse(_currentLoggedInUser.UserId));
            return Result.Success(requests.ConvertAll(r => new RequestDto
            {
                Id = r.Id,
                BloodType = r.BloodType.ToString(),
                Quantity = r.Quantity,
                PatientName = r.PatientName,
                PatientPhoneNumber = r.PatientPhoneNumber,
                NationalId = r.NationalId,
                CreatedAt = r.CreatedAt

            }
            ));
        }

        public async Task<Result>StockIncreament(StockIncreamentDto stockIncreamentDto)
        {
             bool result = Enum.TryParse<BloodType>(stockIncreamentDto.bloodType, out var bloodType);
            if (!result)
            {
                return Result.Fail(new Response(400, "Invalid blood type"));
            }
            var stock = await _stockRepository.GetOneAsync(Guid.Parse(_currentLoggedInUser.UserId), bloodType);
            stock.Quantity += stockIncreamentDto.quantity;
            _stockRepository.UpdateAsync(stock);
            await _stockRepository.SaveAsync();
            return Result.Success();
        }

        public async Task<Result<IEnumerable<StockDto>>> GetStockDetailsAsync(string bloodType = null)
        {

          bool result = Enum.TryParse<BloodType>(bloodType, out var parsedBloodType);
          IEnumerable<Stock> stocks = await _stockRepository.GetAllAsync(Guid.Parse(_currentLoggedInUser.UserId));
            if (result)
            {
                return Result.Success(stocks
                    .Where(s => s.BloodType == parsedBloodType)
                    .Select(s => new StockDto
                    {
                        BloodType = s.BloodType.ToString(),
                        Quantity = s.Quantity
                    }));

            }
            return Result.Success(stocks
                .Select(s => new StockDto
                {
                    BloodType = s.BloodType.ToString(),
                    Quantity = s.Quantity
                }));
        }
    }
}
