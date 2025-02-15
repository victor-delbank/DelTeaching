using DelTeaching.Domain.Entities;
using DelTeaching.Domain.Filters;
using DelTeaching.Domain.Pagination;

namespace DelTeaching.Domain.IRepositories;

public interface IBankAccountRepository : IGenericRepository<BankAccount>
{
    Task<PageList<BankAccount>> Get(PageParams pageParams, BankAccountFilter filter);
    Task<BankAccount> GetById(long id);
    Task<BankAccount> GetByNumber(string number);
    Task<BankAccount> GetByHolderDocument(string holderDocument);
}
