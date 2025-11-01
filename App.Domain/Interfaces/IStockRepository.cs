using App.Domain.Models;


namespace App.Domain.Interfaces
{
    public interface IStockRepository 
    {
        Task<IEnumerable<Stock>> GetAllAsync(int BloodBankId);
        Task<Stock?> GetOneAsync(int BankId, BloodType bloodType);
        Task AddAsync(Stock stock);
        void UpdateAsync(Stock stock);
        void DeleteAsync(Stock stock);

        Task SaveAsync();
    }
}
