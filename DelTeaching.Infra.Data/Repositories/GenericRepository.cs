using System.Linq.Expressions;
using DelTeaching.Domain.IRepositories;
using DelTeaching.Domain.Pagination;
using DelTeaching.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DelTeaching.Infra.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }
    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
    public async Task<PageList<T>> Get(PageParams? pageParams, Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = _context.Set<T>().AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        var totalItems = await query.CountAsync();

        List<T> items = [];

        if (pageParams == null)
        {
            items = await query.ToListAsync();
            return new PageList<T>(items, totalItems, 1, totalItems);
        }

        items = await query.Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
                               .Take(pageParams.PageSize)
                               .ToListAsync();

        return new PageList<T>(items, totalItems, pageParams.PageNumber, pageParams.PageSize);
    }

}
