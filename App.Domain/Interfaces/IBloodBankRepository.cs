using App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
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
