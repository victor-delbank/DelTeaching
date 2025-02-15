using DelTeaching.Domain.IRepositories;
using DelTeaching.Infra.Data.Context;

namespace DelTeaching.Infra.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IBankAccountRepository _bankAccountRepository;
    private ITransactionRepository _transactionRepository;
    private IBalanceRepository _balanceRepository;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IBankAccountRepository BankAccountRepository
    {
        get
        {
            return _bankAccountRepository ??= new BankAccountRepository(_context);
        }
    }

    public ITransactionRepository TransactionRepository
    {
        get
        {
            return _transactionRepository ??= new TransactionRepository(_context);
        }
    }

    public IBalanceRepository BalanceRepository
    {
        get
        {
            return _balanceRepository ??= new BalanceRepository(_context);
        }
    }
    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()) > 0;
    }
}
