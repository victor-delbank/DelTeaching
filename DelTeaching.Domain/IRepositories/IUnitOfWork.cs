namespace DelTeaching.Domain.IRepositories;

public interface IUnitOfWork
{
    IBankAccountRepository BankAccountRepository { get; }  
    ITransactionRepository TransactionRepository { get; } 
    IBalanceRepository BalanceRepository { get; }                      
    Task<bool> SaveChangesAsync();
}