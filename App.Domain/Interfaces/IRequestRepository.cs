using App.Domain.Interfaces;
using App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
{
    public interface IRequestRepository 
    {
        Task<BloodRequest> GetByIdAsync(int id);
        Task<IEnumerable<BloodRequest>> GetAllAsync(int BankId);
        Task AddAsync(BloodRequest request);
        void UpdateAsync(BloodRequest request);
        void DeleteAsync(BloodRequest request);
        Task SaveAsync();

    }
}
