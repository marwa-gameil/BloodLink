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
    internal class RequestRepository : IRequestRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<BloodRequest> _dbSet;
        public RequestRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<BloodRequest>();
        }

        public async Task AddAsync(BloodRequest request)
        {
            await _dbSet.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BloodRequest request)
        {
            _dbSet.Remove(request);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BloodRequest>> GetAllAsync(int BankId)
        {

            // return all requests with related hospital and blood bank data
            return await _dbSet
                .Where(r=>r.BloodBankId == BankId)
                .Select(r=> new BloodRequest
                {
                    Id = r.Id,
                    HospitalId = r.HospitalId,
                    BloodBankId = r.BloodBankId,
                    Quantity = r.Quantity,
                    BloodType = r.BloodType,
                    Status = r.Status,
                    CreatedAt = r.CreatedAt,
                    EndAt = r.EndAt,
                    Hospital = new Hospital
                    {
                        HospitalId = r.Hospital.HospitalId,
                        Name = r.Hospital.Name,
                        Address = r.Hospital.Address
                    },
                   
                })
             .ToListAsync();
        }

        
        public async Task<BloodRequest?> GetByIdAsync(int id)
        {
            // return request by id with related hospital and blood bank data
            return await _dbSet  
                
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        
        public async Task UpdateAsync(BloodRequest request)
        {
            _dbSet.Update(request);
            await _context.SaveChangesAsync();
        }
    }
}
