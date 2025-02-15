namespace DelTeaching.Domain.IRepositories;

public interface IUnitOfWork
{
    IBankAccountRepository BankAccountRepository { get; }  
    ITransactionRepository TransactionRepository { get; }                      
    Task<bool> SaveChangesAsync();
}