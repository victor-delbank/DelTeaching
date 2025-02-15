using DelTeaching.Application.Dtos;
using DelTeaching.Domain.Filters;
using DelTeaching.Domain.Pagination;

namespace DelTeaching.Application.Interfaces;

public interface ITransactionService
{
    Task<PageList<TransactionDto>> Get(PageParams pageParams, TransactionFilter filter);
    Task<TransactionDto> GetById(long id);
    void Create(TransactionDto transactionDto);
}
