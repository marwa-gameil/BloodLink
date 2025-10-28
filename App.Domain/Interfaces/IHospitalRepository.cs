using App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
{
    public interface IHospitalRepository
    {
        Task<Hospital> GetByIdAsync(int id);
        Task<IEnumerable<Hospital>> GetAllAsync();
        Task AddAsync(Hospital hospital);
        void UpdateAsync(Hospital hospital);
        void DeleteAsync(Hospital hospital);
        Task SaveAsync();

    }
}
