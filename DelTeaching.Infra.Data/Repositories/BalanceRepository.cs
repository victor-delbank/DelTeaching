using DelTeaching.Domain.Entities;
using DelTeaching.Domain.IRepositories;
using DelTeaching.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DelTeaching.Infra.Data.Repositories;

public class BalanceRepository : GenericRepository<Balance>, IBalanceRepository
{
    private readonly ApplicationDbContext _context;
    public BalanceRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Balance> GetByBankAccountId(long bankAccountId)
    {
        return await _context.Balances
                                .FirstOrDefaultAsync(b => b.BankAccountId == bankAccountId);
    }
}
