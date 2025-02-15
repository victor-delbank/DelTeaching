using AutoMapper;
using DelTeaching.Application.Dtos;
using DelTeaching.Application.Interfaces;
using DelTeaching.Domain.Entities;
using DelTeaching.Domain.Enums;
using DelTeaching.Domain.Exceptions;
using DelTeaching.Domain.Filters;
using DelTeaching.Domain.IRepositories;
using DelTeaching.Domain.Pagination;

namespace DelTeaching.Application.Services;

public class BankAccountService : IBankAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public BankAccountService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PageList<BankAccountDto>> Get(PageParams pageParams, BankAccountFilter filter)
    {
        var bankAccounts = await _unitOfWork.BankAccountRepository.Get(pageParams, filter);
        var bankAccountsDto = _mapper.Map<IEnumerable<BankAccountDto>>(bankAccounts.Items);
        return new PageList<BankAccountDto>(bankAccountsDto.ToList(), bankAccounts.TotalCount, bankAccounts.CurrentPage, bankAccounts.PageSize);
    }

    public async Task<BankAccountDto> GetByNumber(string number)
    {
        var bankAccount = await _unitOfWork.BankAccountRepository.GetByNumber(number);
        return _mapper.Map<BankAccountDto>(bankAccount);
    }

    public async Task<BankAccountDto> Create(BankAccountDto bankAccountDto)
    {
        var document = await _unitOfWork.BankAccountRepository.GetByHolderDocument(bankAccountDto.HolderDocument);
        if (document is not null)
        {
            throw new DelTeachingException("Documento em uso", "DOCUMENT_IN_USE");
        }
        var bankAccount = new BankAccount(
                                bankAccountDto.Branch,
                                Enum.Parse<EBankAccountType>(bankAccountDto.Type, true),
                                bankAccountDto.HolderName,
                                bankAccountDto.HolderEmail,
                                bankAccountDto.HolderDocument,
                                Enum.Parse<EBankAccountHolderType>(bankAccountDto.HolderType, true),
                                EBankAccountStatus.ACTIVE
                            );

        _unitOfWork.BankAccountRepository.Add(bankAccount);
        await _unitOfWork.SaveChangesAsync();
        return bankAccountDto;
    }

    public async Task<BankAccountDto> Update(long id, BankAccountDto bankAccountDto)
    {
        var bankAccount = await _unitOfWork.BankAccountRepository.GetById(id);
        if (bankAccount is null)
        {
            throw new DelTeachingException("Conta bancária não encontrada", "BANK_ACCOUNT_NOT_FOUND");
        }
        bankAccount.SetHolderEmail(bankAccountDto.HolderEmail);
        if (Enum.TryParse<EBankAccountStatus>(bankAccountDto.Status, true, out var status))
        {
            bankAccount.SetStatus(status);
        }
        _unitOfWork.BankAccountRepository.Update(bankAccount);
        await _unitOfWork.SaveChangesAsync();
        return bankAccountDto;
    }
}