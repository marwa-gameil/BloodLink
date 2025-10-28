﻿using App.Domain.Models;
using App.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories
{
    public class BloodBankRepository : IBloodBankRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<BloodBank> _dbSet;

        public BloodBankRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<BloodBank>();
        }
        public async Task<IEnumerable<BloodBank>> GetAllAsync(string Governorate)
        {
            return await _dbSet.Where(g => g.Governorate == Governorate).ToListAsync();

        }
        public async Task<BloodBank?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task AddAsync(BloodBank bloodBank)
        {
            await _dbSet.AddAsync(bloodBank);
        }

        public void UpdateAsync(BloodBank bloodBank)
        {
            _dbSet.Update(bloodBank);
        }
        public void DeleteAsync(BloodBank bloodBank)
        {
            _dbSet.Remove(bloodBank);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
