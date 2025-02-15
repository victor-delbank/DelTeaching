using System.Text.RegularExpressions;
using DelTeaching.Domain.Enums;
using DelTeaching.Domain.Exceptions;

namespace DelTeaching.Domain.Entities;

public class BankAccount : BaseEntity
{
    public string Branch { get; protected set; }
    public string Number { get; protected set; }
    public EBankAccountType Type { get; protected set; }
    public string HolderName { get; protected set; }
    public string HolderEmail { get; protected set; }
    public EBankAccountHolderType HolderType { get; protected set; }
    public EBankAccountStatus Status { get; protected set; }
    public Balance Balance { get; protected set; }
    public Transaction Transaction { get; protected set; }

    protected BankAccount() { }

    public BankAccount(
        string branch,
        EBankAccountType type,
        string holderName,
        string holderEmail,
        EBankAccountHolderType holderType,
        EBankAccountStatus status
        )
    {
        Branch = branch;
        GenerateNumber();
        Type = type;
        HolderName = holderName;
        SetHolderEmail(holderEmail);
        HolderType = holderType;
        Status = status;
    }

    public void SetHolderEmail(string holderEmail)
    {
        string pattern = @"^[\w\-\.]+@([\w\-]+\.)+[\w\-]{2,4}$";

        if (!Regex.IsMatch(holderEmail, pattern))
        {
            throw new DelTeachingException("O e-mail digitado é inválido", "INVALID_EMAIL");
        }
        HolderEmail = holderEmail;
    }

    private static readonly object _lock = new();
    private static readonly HashSet<string> _numbers = [];

    public static string GenerateNumber()
    {
        lock (_lock)
        {
            string number;
            do
            {
                number = (DateTime.Now.Ticks % 10000000).ToString("D7");
            }
            while (_numbers.Contains(number));

            _numbers.Add(number);
            return number;
        }
    }

    public void SetAccountType(EBankAccountType type)
    {
        if (type == Type)
        {
            throw new DelTeachingException("A já está no tipo desejado.", "ACCOUNT_ALREADY_FINISHED");
        }
        Type = type;
    }

    public void Finish()
    {
        if (Status == EBankAccountStatus.FINISHED)
        {
            throw new DelTeachingException("A conta já está finalizada.", "ACCOUNT_ALREADY_FINISHED");
        }

        Status = EBankAccountStatus.FINISHED;
    }

    public void Block()
    {
        if (Status == EBankAccountStatus.BLOCKED || Status == EBankAccountStatus.FINISHED)
        {
            throw new DelTeachingException("A conta já está bloqueada ou cancelada.", "ACCOUNT_ALREADY_BLOCKED_OR_CANCELED");
        }

        Status = EBankAccountStatus.BLOCKED;
    }
}
