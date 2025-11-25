using App.Domain.Models;


namespace App.Domain.Interfaces
{
    public interface IStockRepository 
    {
        Task<IEnumerable<Stock>> GetAllAsync(Guid BloodBankId);
        Task<Stock> GetOneAsync(Guid BankId, BloodType bloodType);
        Task AddAsync(Stock stock);
        void UpdateAsync(Stock stock);
        void DeleteAsync(Stock stock);


        Task SaveAsync();
    }
}
