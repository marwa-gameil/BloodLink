using System.Linq.Expressions;
using App.Domain.Abstractions;

namespace App.Domain.Specifications;

public abstract class Specification<TModel> where TModel : BaseModel
{
    public Expression<Func<TModel, bool>>? Criteria { get; }
    public List<Expression<Func<TModel, object>>>? Includes { get; } = new();
    public Expression<Func<TModel, object>>? OrderBy { get; set; }

    public Specification()
    {

    }

    public Specification(Expression<Func<TModel, bool>> criteria)
    {
        Criteria = criteria;
    }
}

public abstract class Specification<TModel, TResult> : Specification<TModel> where TModel : BaseModel
{
    public Specification(Expression<Func<TModel, bool>> criteria) : base(criteria) { }
    public Expression<Func<TModel, TResult>>? Selector { get; set; }
}
