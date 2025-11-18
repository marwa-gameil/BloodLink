using App.Domain.Models;

namespace App.Domain.Interfaces
{
    public interface IBloodBankRepository
    {
        Task<BloodBank?> GetByIdAsync(int id);
        Task<IEnumerable<BloodBank>> GetAllAsync(string Governorate);
        Task AddAsync(BloodBank bloodBank);
        void UpdateAsync(BloodBank bloodBank);
        void DeleteAsync(BloodBank bloodBank);
        Task SaveAsync();

    }
}
