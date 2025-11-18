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
        public BloodBankService(IBloodBankRepository bloodBankRepository,
            IStockRepository stockRepository,
            UserManager<User> userManager,
            ICurrentLoggedInUser user)
        {
            _bloodBankRepository = bloodBankRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
            _currentLoggedInUser = user;
        }
        public async Task<Result<IEnumerable<BloodBankDto>>> GetAllBloodBanksAsync(string Governorate)
        {
            var bank = await _bloodBankRepository.GetAllAsync(Governorate);
            return Result.Success( bank.ConvertAll(b => new BloodBankDto
            {
                Id = b.Id,
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
                Name = CreateBloodBankDto.Name,
                Address = CreateBloodBankDto.Address,
                Governorate = CreateBloodBankDto.Governorate,
                PhoneNumber = CreateBloodBankDto.PhoneNumber,
                Email = CreateBloodBankDto.Email,
                CreatedAt = DateTime.UtcNow,
                CreatedById = Guid.Parse(_currentLoggedInUser.UserId)

            };
            await _userManager.CreateAsync( newUser,CreateBloodBankDto.Password );
            await _userManager.AddToRoleAsync(newUser,"bloodbank");

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
                    BloodBankId = newBank.Id,
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
        public async Task<Result> UpdateBloodBankAsync(int id, UpdateBloodBankDto UpdateBloodBankDto)
        {
            var existingBank = await _bloodBankRepository.GetByIdAsync(id);
            if (existingBank == null)
            {
                return Result.Fail(new Response(404, "Blood Bank not found"));
            }

            existingBank.PhoneNumber = UpdateBloodBankDto.PhoneNumber;
            existingBank.Email = UpdateBloodBankDto.Email;
             _bloodBankRepository.UpdateAsync(existingBank);
            await _bloodBankRepository.SaveAsync();
            return Result.Success(new Response(200, "Blood Bank updated successfully"));
        }
*/
        public async Task<Result> DeleteBloodBankAsync(int id)
        {
            var existingBank = await _bloodBankRepository.GetByIdAsync(id);
            if (existingBank == null)
            {
                return Result.Fail(new Response(404, "Blood Bank not found"));
            }
           
            return Result.Success(new Response(204, "Blood Bank deleted successfully"));
        }
    }
}
