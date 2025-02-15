using DelTeaching.Domain.Enums;

namespace DelTeaching.Application.Dtos;

public class BankAccountDto
{
    public long Id { get; set; }
    public string Branch { get; set; }
    public string? Number { get; set; }
    public string Type { get; set; }
    public string HolderName { get; set; }
    public string HolderEmail { get; set; }
    public string HolderDocument { get; set; }
    public string HolderType { get; set; }
    public string Status { get; set; }
    public BalanceDto? Balance { get; set; }
    public IEnumerable<TransactionDto>? Transaction { get; set; }
}
