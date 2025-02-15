using DelTeaching.Domain.Entities;

namespace DelTeaching.Domain.IRepositories;

public interface IBalanceRepository
{
    Task<Balance> GetByBankAccountId(long bankAccountId);
}
