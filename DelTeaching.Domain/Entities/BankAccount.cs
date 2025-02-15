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
    public string HolderDocument { get; protected set; }
    public EBankAccountHolderType HolderType { get; protected set; }
    public EBankAccountStatus Status { get; protected set; }
    public Balance Balance { get; protected set; }
    public IEnumerable<Transaction> Transaction { get; protected set; }

    protected BankAccount() { }

    public BankAccount(
        string branch,
        EBankAccountType type,
        string holderName,
        string holderEmail,
        string holderDocument,
        EBankAccountHolderType holderType,
        EBankAccountStatus status
        )
    {
        SetBranch(branch);
        Number = GenerateNumber();
        Type = type;
        HolderName = holderName;
        SetHolderEmail(holderEmail);
        HolderDocument = holderDocument;
        HolderType = holderType;
        Status = status;
        Balance = new Balance(0, 0);
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
    private static readonly HashSet<string> _numbers = new HashSet<string>();
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
    public void SetStatus(EBankAccountStatus status)
    {
        if (status == Status)
        {
            throw new DelTeachingException("A conta já está no status solicitado.");
        }

        Status = status;
    }
    public void SetBranch(string branch)
    {
        if (branch.Length > 5)
        {
            throw new DelTeachingException("Agencia deve ter no maximo 5 numeros", "");
        }
        if (!Regex.IsMatch(branch, @"^\d+$"))
        {
            throw new DelTeachingException("Agência deve conter apenas números", "INVALID_BRANCH_FORMAT");
        }
        Branch = branch;
    }
}
