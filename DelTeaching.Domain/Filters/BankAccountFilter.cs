using DelTeaching.Domain.Enums;

namespace DelTeaching.Domain.Filters;

public class BankAccountFilter
{
    public string? Branch { get; set; }
    public string? Number { get; set; }
    public EBankAccountType? Type { get; set; }
    public string? HolderName { get; set; }
    public string? HolderEmail { get; set; }
    public EBankAccountStatus? Status { get; set; }
}
