using DelTeaching.Application.Dtos;
using DelTeaching.Application.Interfaces;
using DelTeaching.Domain.Filters;
using DelTeaching.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace DelTeaching.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _service;
    public TransactionController(ITransactionService service)
    {
        _service = service;
    }

    [HttpGet("{accountId}")]
    public async Task<IActionResult> Get(long accountId, [FromQuery] PageParams pageParams, [FromQuery] TransactionFilter filter)
    {
        filter.BankAccountId = accountId;
        var result = await _service.Get(pageParams, filter);
        return Ok(result);
    }

    [HttpPost("{accountId}")]
    public async Task<IActionResult> Create(long accountId,TransactionDto transactionDto)
    {
        transactionDto.BankAccountId = accountId;
        _service.Create(transactionDto);
        return Ok();
    }
}
