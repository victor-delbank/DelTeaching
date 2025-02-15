using AutoMapper;
using DelTeaching.Domain.Entities;

namespace DelTeaching.Application.Dtos.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BankAccount, BankAccountDto>().ReverseMap();
        CreateMap<Balance, BalanceDto>().ReverseMap();
        CreateMap<Transaction, TransactionDto>().ReverseMap();
    }
}