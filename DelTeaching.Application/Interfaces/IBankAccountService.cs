using DelTeaching.Application.Dtos;
using DelTeaching.Domain.Filters;
using DelTeaching.Domain.Pagination;

namespace DelTeaching.Application.Interfaces;

public interface IBankAccountService
{
    Task<BankAccountDto> Create(BankAccountDto bankAccountDto);
    Task<BankAccountDto> Update(long id, BankAccountDto bankAccountDto);
    Task<BankAccountDto> GetByNumber(string number);
    Task<PageList<BankAccountDto>> Get(PageParams pageParams, BankAccountFilter filter);
}
