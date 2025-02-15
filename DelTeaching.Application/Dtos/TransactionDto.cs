namespace DelTeaching.Application.Dtos;

public class TransactionDto
{
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public long BankAccountId { get; set; }
    public BankAccountDto? BankAccount { get; set; }
    public string CounterpartyBankCode { get; set; }
    public string CounterPartyBankName { get; set; }
    public string CounterPartyBranch { get; set; }
    public string CounterPartyAccountNumber { get; set; }
    public string CounterPartyAccountType { get; set; }
    public string CounterPartyHolderName { get; set; }
    public string CounterPartyHolderType { get; set; }
    public string CounterPartyHolderDocument { get; set; }
}
