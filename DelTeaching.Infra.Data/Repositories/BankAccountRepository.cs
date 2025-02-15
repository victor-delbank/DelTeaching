using DelTeaching.Domain.Entities;
using DelTeaching.Domain.Filters;
using DelTeaching.Domain.IRepositories;
using DelTeaching.Domain.Pagination;
using DelTeaching.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DelTeaching.Infra.Data.Repositories;

public class BankAccountRepository : GenericRepository<BankAccount>, IBankAccountRepository
{
    private readonly ApplicationDbContext _context;
    public BankAccountRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<PageList<BankAccount>> Get(PageParams pageParams, BankAccountFilter filter)
    {
        IQueryable<BankAccount> query = _context.BankAccounts;

        if (!string.IsNullOrEmpty(filter.Branch))
        {
            query = query.Where(s => s.Branch == filter.Branch);
        }

        if (!string.IsNullOrEmpty(filter.Number))
        {
            query = query.Where(s => s.Number == filter.Number);
        }

        if (filter.Type.HasValue)
        {
            query = query.Where(s => s.Type == filter.Type);
        }

        if (!string.IsNullOrEmpty(filter.HolderName))
        {
            query = query.Where(s => s.HolderName == filter.HolderName);
        }

        if (!string.IsNullOrEmpty(filter.HolderEmail))
        {
            query = query.Where(s => s.HolderEmail == filter.HolderEmail);
        }

        if (filter.Status.HasValue)
        {
            query = query.Where(s => s.Status == filter.Status);
        }

        var totalCount = await query.CountAsync();

        var items = await query.Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
                            .Take(pageParams.PageSize)
                            .ToListAsync();

        return new PageList<BankAccount>(items, totalCount, pageParams.PageNumber, pageParams.PageSize);
    }

    public async Task<BankAccount> GetByHolderDocument(string holderDocument)
    {
        return await _context.BankAccounts
                                .FirstOrDefaultAsync(b => b.HolderDocument == holderDocument);
    }

    public async Task<BankAccount> GetById(long id)
    {
        return await _context.BankAccounts
                                .Include(b => b.Balance)
                                .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<BankAccount> GetByNumber(string number)
    {
        return await _context.BankAccounts
                                .Include(b => b.Balance)
                                 .FirstOrDefaultAsync(b => b.Number == number);
    }
}
