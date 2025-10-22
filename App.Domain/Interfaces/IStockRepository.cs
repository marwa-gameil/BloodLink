using App.Domain.Models;


namespace App.Domain.Interfaces
{
    public interface IStockRepository 
    {
        Task<IEnumerable<Stock>> GetAllAsync(int BloodBankId);
        Task<Stock> GetByIdAsync(int id);
        Task AddAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        Task DeleteAsync(Stock stock);
    }
}
