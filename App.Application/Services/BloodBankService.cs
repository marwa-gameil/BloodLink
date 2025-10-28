using App.Application.DTOs;
using App.Application.Interfaces;
using App.Application.Utilities;
using App.Domain.Interfaces;
using App.Domain.Models;
using App.Domain.Responses;
using App.Infrastructure.Repositories;


namespace App.Application.Services
{
    public class BloodBankService : IBloodBankService
    {
        private readonly IBloodBankRepository _bloodBankRepository;
        private readonly IStockRepository _stockRepository;
        public BloodBankService(IBloodBankRepository bloodBankRepository,IStockRepository stockRepository)
        {
            _bloodBankRepository = bloodBankRepository;
            _stockRepository = stockRepository;
        }
        public async Task<Result<IEnumerable<BloodBankDto>>> GetAllBloodBanksAsync(string Governorate)
        {
            var bank = await _bloodBankRepository.GetAllAsync(Governorate);
            return Result.Success( bank.ConvertAll(b => new BloodBankDto
            {
                Id = b.Id,
                Name = b.Name,
                Address = b.Address,
                ContactNumber = b.PhoneNumber,
                Email = b.Email,
                Governorate = b.Governorate

            }
            ));

        }
        /*
        public async Task<Result<BloodBankDto>> GetBloodBankByIdAsync(int id)
        {
           var bank = await _bloodBankRepository.GetByIdAsync(id);
            if (bank == null)
            {
                return Result.Fail<BloodBankDto>(new Response(404, "Blood Bank not found"));
            }
            var dto = new BloodBankDto
            {
                Id = bank.Id,
                Name = bank.Name,
                Address = bank.Address,
                ContactNumber = bank.PhoneNumber,
                Email = bank.Email
            };
            return Result.Success(dto);
        }
        */
        public async Task<Result> AddBloodBankAsync(CreateBloodBankDto CreateBloodBankDto)
        {
            var newBank = new BloodBank
            
            {
                Name = CreateBloodBankDto.Name,
                Address = CreateBloodBankDto.Address,
                Governorate = CreateBloodBankDto.Governorate,
                PhoneNumber = CreateBloodBankDto.PhoneNumber,
                Email = CreateBloodBankDto.Email,
                Latitude = CreateBloodBankDto.Latitude,
                Longitude = CreateBloodBankDto.Longitude,
                LicenseNumber = CreateBloodBankDto.LicenseNumber,
                CreatedAt = DateTime.UtcNow
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
