using DelTeaching.Domain.Enums;

namespace DelTeaching.Domain.Entities;

public class Transaction : BaseEntity
{
    public ETransactionType Type { get; protected set; }
    public decimal Amount { get; protected set; }
    public long BankAccountId { get; protected set; }
    public BankAccount BankAccount { get; protected set; }
    public string CounterPartyBankCode { get; protected set; }
    public string CounterPartyBankName { get; protected set; }
    public string CounterPartyBranch { get; protected set; }
    public string CounterPartyAccountNumber { get; protected set; }
    public string CounterPartyAccountType { get; protected set; }
    public string CounterPartyHolderName { get; protected set; }
    public string CounterPartyHolderType { get; protected set; }
    public string CounterPartyHolderDocument { get; protected set; }

    protected Transaction() { }

    public Transaction(
        ETransactionType type,
        decimal amount,
        long bankAccountId,
        string counterPartyBankCode,
        string counterPartyBankName,
        string counterPartyBranch,
        string counterPartyAccountNumber,
        string counterPartyAccountType,
        string counterPartyHolderName,
        string counterPartyHolderType,
        string counterPartyHolderDocument)
    {
        Type = type;
        Amount = amount;
        BankAccountId = bankAccountId;
        CounterPartyBankCode = counterPartyBankCode;
        CounterPartyBankName = counterPartyBankName;
        CounterPartyBranch = counterPartyBranch;
        CounterPartyAccountNumber = counterPartyAccountNumber;
        CounterPartyAccountType = counterPartyAccountType;
        CounterPartyHolderName = counterPartyHolderName;
        CounterPartyHolderType = counterPartyHolderType;
        CounterPartyHolderDocument = counterPartyHolderDocument;
    }
}
