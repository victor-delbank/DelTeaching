using DelTeaching.Domain.Exceptions;

namespace DelTeaching.Domain.Entities;

public class Balance : BaseEntity
{
    public long BankAccountId { get; protected set; }
    public BankAccount BankAccount { get; set; }
    public decimal AvailableAmount { get; protected set; }
    public decimal BlockedAmount { get; protected set; }

    protected Balance() { }

    public Balance(
        long bankAccountId,
        decimal availableAmount,
        decimal blockedAmount
    )
    {
        BankAccountId = bankAccountId;
        AvailableAmount = availableAmount;
        BlockedAmount = blockedAmount;
    }

    public void SetAvailableAmount(decimal availableAmount)
    {
        if(availableAmount <= 0)
        {
            throw new DelTeachingException("O valor disponível não pode ser menor que 0", "INVALID_AVAILABLE_AMOUNT");
        }
        AvailableAmount = availableAmount;
    }

    public void SetBlockedAmount(decimal blockedAmount)
    {
        if(blockedAmount <= 0)
        {
            throw new DelTeachingException("O valor bloqueado não pode ser menor que 0", "INVALID_BLOCKED_AMOUNT");
        }
        BlockedAmount = blockedAmount;
    }
}
