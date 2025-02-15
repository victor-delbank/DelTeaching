

using DelTeaching.Domain.Entities;
using DelTeaching.Domain.Filters;
using DelTeaching.Domain.IRepositories;
using DelTeaching.Domain.Pagination;
using DelTeaching.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DelTeaching.Infra.Data.Repositories;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    private readonly ApplicationDbContext _context;
    public TransactionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PageList<Transaction>> Get(PageParams pageParams, TransactionFilter filter)
    {
        IQueryable<Transaction> query = _context.Transactions;

        if (filter.BankAccountId.HasValue)
        {
            query = query.Where(s => s.BankAccountId == filter.BankAccountId);
        }

        if (filter.Type.HasValue)
        {
            query = query.Where(s => s.Type == filter.Type);
        }

        if (filter.Amount.HasValue)
        {
            query = query.Where(s => s.Amount == filter.Amount);
        }

        var totalCount = await query.CountAsync();

        var items = await query.Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
                            .Take(pageParams.PageSize)
                            .ToListAsync();

        return new PageList<Transaction>(items, totalCount, pageParams.PageNumber, pageParams.PageSize);
    }

    public async Task<Transaction> GetById(long id)
    {
        return await _context.Transactions
                                .FirstOrDefaultAsync(t => t.Id == id);
    }
}
