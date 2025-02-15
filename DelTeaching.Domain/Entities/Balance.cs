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

    public Balance(
        decimal availableAmount,
        decimal blockedAmount
    )
    {
        AvailableAmount = availableAmount;
        BlockedAmount = blockedAmount;
    }

    public void BlockAmount(decimal amount)
    {
        if (amount <= 0)
        {
            throw new DelTeachingException("O valor a bloquear deve ser maior que 0", "INVALID_BLOCK_AMOUNT");
        }

        if (amount > AvailableAmount)
        {
            throw new DelTeachingException("Saldo insuficiente para bloqueio", "INSUFFICIENT_FUNDS");
        }

        AvailableAmount -= amount;
        BlockedAmount += amount;
    }

    public void UnblockAmount(decimal amount)
    {
        if (amount <= 0)
        {
            throw new DelTeachingException("O valor a desbloquear deve ser maior que 0", "INVALID_UNBLOCK_AMOUNT");
        }

        if (amount > BlockedAmount)
        {
            throw new DelTeachingException("Saldo bloqueado insuficiente para desbloqueio", "INSUFFICIENT_BLOCKED_FUNDS");
        }

        BlockedAmount -= amount;
        AvailableAmount += amount;
    }

    public void AddAvailableAmount(decimal amount)
    {
        if (amount <= 0)
        {
            throw new DelTeachingException("O valor a adicionar deve ser maior que 0", "INVALID_ADD_AMOUNT");
        }

        AvailableAmount += amount;
    }

    public void RemoveAvailableAmount(decimal amount)
    {
        if (amount <= 0)
        {
            throw new DelTeachingException("O valor a remover deve ser maior que 0", "INVALID_REMOVE_AMOUNT");
        }

        if (amount > AvailableAmount)
        {
            throw new DelTeachingException("Saldo disponível insuficiente", "INSUFFICIENT_AVAILABLE_FUNDS");
        }

        AvailableAmount -= amount;
    }
}
