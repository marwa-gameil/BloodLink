using App.Domain.Interfaces;
using App.Domain.Models;
using App.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace App.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<Stock> _dbSet;
        public StockRepository( ApplicationDbContext applicationDbContext) 
        {

            _applicationDbContext = applicationDbContext;
            _dbSet = _applicationDbContext.Set<Stock>();
        }
        public async Task<IEnumerable<Stock>> GetAllAsync(int BloodBankId)
        {
            return await _dbSet
                .Where(s => s.BloodBankId == BloodBankId)
                .Select(s => new Stock
                {
                    Id = s.Id,
                    BloodBankId = s.BloodBankId,
                    Quantity = s.Quantity,
                    BloodType = s.BloodType,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt,
                    BloodBank = new BloodBank
                    {
                        Id = s.BloodBank.Id,
                        Name = s.BloodBank.Name,
                        Address = s.BloodBank.Address

                    }
                })
                .ToListAsync();

        }
        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task AddAsync(Stock stock)
        {
            await _dbSet.AddAsync(stock);
            await _applicationDbContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(Stock stock)
        {
           _dbSet.Update(stock);
              await _applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Stock stock)
        {
            _dbSet.Remove(stock);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
