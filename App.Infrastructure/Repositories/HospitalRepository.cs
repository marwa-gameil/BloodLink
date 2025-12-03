using App.Domain.Interfaces;
using App.Domain.Models;
using App.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Hospital> _dbSet;

        public HospitalRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Hospital>();
        }

        public async Task AddAsync(Hospital hospital)
        {
            await _dbSet.AddAsync(hospital);
        }

        public void DeleteAsync(Hospital hospital)
        {
             _dbSet.Remove(hospital);
        }

        public async Task<Hospital?> GetByIdAsync(Guid id)
        {
          return await _dbSet.FirstOrDefaultAsync(h => h.UserId == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateAsync(Hospital hospital)
        {
            _dbSet.Update(hospital);

        }
    }
}
