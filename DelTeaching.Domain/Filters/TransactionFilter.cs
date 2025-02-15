using System.Text.Json.Serialization;
using DelTeaching.Domain.Enums;

namespace DelTeaching.Domain.Filters;

public class TransactionFilter
{
    [JsonIgnore]
    public long? BankAccountId { get; set; }
    public ETransactionType? Type { get; set; }
    public decimal? Amount { get; set; }
}
