using DelTeaching.Domain.Entities;
using DelTeaching.Domain.Filters;
using DelTeaching.Domain.Pagination;

namespace DelTeaching.Domain.IRepositories;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    Task<PageList<Transaction>> Get(PageParams pageParams, TransactionFilter filter);
    Task<Transaction> GetById(long id);
}
