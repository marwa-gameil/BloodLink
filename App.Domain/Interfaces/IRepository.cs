using System.Linq.Expressions;
using App.Domain.Abstractions;
using App.Domain.Specifications;

namespace App.Domain.Interfaces;

public interface IRepository<TModel> where TModel : BaseModel
{
    Task<IEnumerable<TModel>> GetAll();
    Task<IEnumerable<TModel>> GetAll(Specification<TModel> specification);
    Task<IEnumerable<TResult>> GetAll<TResult>(Specification<TModel, TResult> specification);
    Task<TModel?> GetOne(Specification<TModel> specification);
    Task<TResult?> GetOne<TResult>(Specification<TModel, TResult> specification);
    Task<TModel> Add(TModel model);
    TModel Update(TModel model);
    void Delete(TModel model);
}
