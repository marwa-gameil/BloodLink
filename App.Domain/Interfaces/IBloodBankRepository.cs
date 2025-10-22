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
        Task<BloodBank> GetByIdAsync(int id);
        Task<IEnumerable<BloodBank>> GetAllAsync();
        Task AddAsync(BloodBank bloodBank);
        Task UpdateAsync(BloodBank bloodBank);
        Task DeleteAsync(BloodBank bloodBank);

    }
}
