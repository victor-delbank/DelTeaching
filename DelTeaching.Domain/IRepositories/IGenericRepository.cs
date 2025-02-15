using System.Linq.Expressions;
using DelTeaching.Domain.Pagination;

namespace DelTeaching.Domain.IRepositories;

public interface IGenericRepository<T>
{
    Task<PageList<T>> Get(PageParams? pageParams, Expression<Func<T, bool>>? filter = null);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}