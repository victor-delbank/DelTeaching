using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using DelTeaching.Application.Dtos;
using DelTeaching.Application.Interfaces;
using DelTeaching.Domain.Enums;
using DelTeaching.Domain.Exceptions;
using DelTeaching.Domain.Filters;
using DelTeaching.Domain.IRepositories;
using DelTeaching.Domain.Pagination;

namespace DelTeaching.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMessageBus _messageBus;
    public TransactionService(IUnitOfWork unitOfWork, IMapper mapper, IMessageBus messageBus)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _messageBus = messageBus;
    }
    public async Task<PageList<TransactionDto>> Get(PageParams pageParams, TransactionFilter filter)
    {
        var transaction = await _unitOfWork.TransactionRepository.Get(pageParams, filter);
        var transactionDto = _mapper.Map<IEnumerable<TransactionDto>>(transaction.Items);
        return new PageList<TransactionDto>(transactionDto.ToList(), transaction.TotalCount, transaction.CurrentPage, transaction.PageSize);
    }

    public async Task<TransactionDto> GetById(long id)
    {
        var transaction = await _unitOfWork.TransactionRepository.GetById(id);
        return _mapper.Map<TransactionDto>(transaction);
    }

    public async void Create(TransactionDto transactionDto)
    {
        var balance = await _unitOfWork.BalanceRepository.GetByBankAccountId(transactionDto.BankAccountId);
        if (balance.AvailableAmount < transactionDto.Amount)
        {
            throw new DelTeachingException("Saldo insuficiente para realizar a operação");
        }
        if (Enum.TryParse<ETransactionType>(transactionDto.Type, true, out var status))
        {
            switch (status)
            {
                case ETransactionType.AMOUNT_HOLD:
                    balance.BlockAmount(transactionDto.Amount);
                    break;

                case ETransactionType.AMOUNT_RELEASE:
                    balance.UnblockAmount(transactionDto.Amount);
                    break;

                case ETransactionType.CREDIT:
                    balance.AddAvailableAmount(transactionDto.Amount);
                    break;

                case ETransactionType.DEBIT:
                    balance.RemoveAvailableAmount(transactionDto.Amount);
                    break;

                default:
                    throw new DelTeachingException("Tipo de transação inválido", "INVALID_TRANSACTION_TYPE");
            }
        }
        await _unitOfWork.SaveChangesAsync();
        _messageBus.Publish<TransactionDto>("amv-save-transaction-in", transactionDto);
    }
}
