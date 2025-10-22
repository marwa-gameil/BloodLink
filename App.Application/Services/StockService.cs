using App.Application.DTOs;
using App.Application.Interfaces;
using App.Domain.Interfaces;
using App.Domain.Models;
using App.Domain.Responses;
using App.Infrastructure.Repositories;

namespace App.Application.Services
{
    public class StockService : IStockService
    {
        private readonly IBloodBankRepository _bloodBankRepository;
        private readonly IStockRepository _stockRepository;
        public StockService(IBloodBankRepository bloodBankRepository,IStockRepository stockRepository)
        {
            _bloodBankRepository = bloodBankRepository;
            _stockRepository = stockRepository;
        }
        public async Task<Result<IEnumerable<StockDto>>> GetAllStocksAsync(int BloodBankId)
        {
            var stocks = await _stockRepository.GetAllAsync(BloodBankId);
            return Result.Success(stocks.Select(stock => new StockDto
            {
                Id = stock.Id,
                BloodBankName = stock.BloodBank.Name,
                Quantity = stock.Quantity,
                BloodType = stock.BloodType.ToString(),
                CreatedAt = stock.CreatedAt,
                UpdatedAt = stock.UpdatedAt
            }
            ));
            
        }
        public async Task<Result<Stock>> GetStockByIdAsync(int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
                return Result.Fail<Stock>(new Response(404, "Request not found"));

            return Result.Success(stock);
        }
        public async Task<Result> CreateStockAsync(CreateStockDto dto)
        {
            var bloodBank = await _bloodBankRepository.GetByIdAsync(dto.BloodBankId);
            if (bloodBank == null)
                return Result.Fail(new Response(404, "Blood Bank not found"));

            var stock = new Stock
            {
                BloodBankId = bloodBank.Id,
                Quantity = dto.Quantity,
                BloodType = Enum.Parse<BloodType>(dto.BloodType),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _stockRepository.AddAsync(stock);
            return Result.Success(new Response(200, "Stock created successfully"));
        
        }
        public async Task<Result> UpdateStockAsync(int id, UpdateStockDto dto)
        {
            var existing = await _stockRepository.GetByIdAsync(id);
            if (existing == null)
                return Result.Fail(new Response(404, "Stock not found"));

            existing.Quantity = dto.Quantity;
            existing.BloodType = Enum.Parse<BloodType>(dto.BloodType);
            existing.UpdatedAt = DateTime.UtcNow;

            await _stockRepository.UpdateAsync(existing);
            return Result.Success(new Response(200, "Stock updated successfully"));
        }

        public async Task<Result> DeleteStockAsync(int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
                return Result.Fail(new Response(404, "Stock not found"));

            await _stockRepository.DeleteAsync(stock);
            return Result.Success(new Response(200, "Stock deleted successfully"));
        }



    }
}
