using App.Domain.Interfaces;
using App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Interfaces
{
    public interface IRequestRepository 
    {
        Task<BloodRequest?> GetByIdAsync(int id);

        Task<IEnumerable<BloodRequest>> GetAllAsync(Guid BankId);
        Task AddAsync(BloodRequest request);
        void UpdateAsync(BloodRequest request);
        void DeleteAsync(BloodRequest request);
        IEnumerable<BloodRequest> GetRequestsByHospitalAsync(Guid hospitalId);

        Task SaveAsync();

    }
}
