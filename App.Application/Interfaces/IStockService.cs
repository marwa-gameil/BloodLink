using App.Application.DTOs;
using App.Domain.Models;
using App.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IStockService
    {
        Task<Result<IEnumerable<StockDto>>> GetAllStocksAsync(int BloodBankId);
        Task<Result<Stock?>> GetStockByIdAsync(int bloodBankId);
        Task<Result> CreateStockAsync(CreateStockDto dto);
        Task<Result> UpdateStockAsync(int id, UpdateStockDto dto);
        Task<Result> DeleteStockAsync(int id);
    }
}
