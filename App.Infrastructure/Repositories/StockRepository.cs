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
                .ToListAsync();

        }
        public async Task<Stock?> GetOneAsync(int BloodBankId,BloodType bloodType)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.BloodBankId == BloodBankId && r.BloodType == bloodType );
        }
        public async Task AddAsync(Stock stock)
        {
            await _dbSet.AddAsync(stock);
        }


        public  void UpdateAsync(Stock stock)
        {
           _dbSet.Update(stock);
        }

        public  void DeleteAsync(Stock stock)
        {
            _dbSet.Remove(stock);
        }
        public async Task SaveAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
