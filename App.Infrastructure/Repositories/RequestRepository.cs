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
    public class RequestRepository : IRequestRepository
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
        }

        public void DeleteAsync(BloodRequest request)
        {
            _dbSet.Remove(request);
        }

        public async Task<IEnumerable<BloodRequest>> GetAllAsync(Guid BankId)
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
                        UserId = r.HospitalId,
                        User = new User
                        {
                            Name = r.Hospital.User.Name,
                            Address = r.Hospital.User.Address,
                            PhoneNumber = r.Hospital.User.PhoneNumber,
                            Email = r.Hospital.User.Email,
                            Governorate = r.Hospital.User.Governorate
                        }
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
        
        public void UpdateAsync(BloodRequest request)
        {
            _dbSet.Update(request);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<BloodRequest> GetRequestsByHospitalAsync(Guid hospitalId)
        {
           return  _dbSet.Where(r => r.HospitalId == hospitalId);

        }
    }
}
