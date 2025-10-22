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
        Task UpdateAsync(Hospital hospital);
        Task DeleteAsync(Hospital hospital);
    }
}
