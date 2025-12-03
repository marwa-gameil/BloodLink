using Microsoft.EntityFrameworkCore;
using App.Domain.Interfaces;
using App.Domain.Abstractions;
using App.Domain.Specifications;
using App.Infrastructure.Data;

namespace App.Infrastructure.Repositories;

public class GenericRepository<TModel> : IRepository<TModel> where TModel : BaseModel
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TModel> _dbSet;
    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TModel>();
    }

    public async virtual Task<IEnumerable<TModel>> GetAll() =>
        await _dbSet.ToListAsync();

    public async Task<IEnumerable<TModel>> GetAll(Specification<TModel> specification) =>
        await SpecificationQueryBuilder.Build(_dbSet, specification).ToListAsync();

    public async Task<IEnumerable<TResult>> GetAll<TResult>(Specification<TModel, TResult> specification) =>
        await SpecificationQueryBuilder.Build(_dbSet, specification).ToListAsync();

    public async Task<TModel?> GetOne(Specification<TModel> specification) =>
        await SpecificationQueryBuilder.Build(_dbSet, specification).FirstOrDefaultAsync();

    public async Task<TResult?> GetOne<TResult>(Specification<TModel, TResult> specification) =>
        await SpecificationQueryBuilder.Build(_dbSet, specification).FirstOrDefaultAsync();

    public async virtual Task<TModel?> GetById(int id) =>
        await _dbSet.FindAsync(id);

    public async virtual Task<TModel> Add(TModel model) =>
        (await _dbSet.AddAsync(model)).Entity;

    public virtual TModel Update(TModel model) =>
        _dbSet.Update(model).Entity;

    public virtual void Delete(TModel model) =>
        _dbSet.Remove(model);
}
