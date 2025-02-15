using DelTeaching.Application.Dtos;
using DelTeaching.Application.Interfaces;
using DelTeaching.Domain.Filters;
using DelTeaching.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace DelTeaching.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BankAccountController : ControllerBase
{
    private readonly IBankAccountService _service;
    public BankAccountController(IBankAccountService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BankAccountDto bankAccountDto)
    {
        var result = await _service.Create(bankAccountDto);
        return Ok(result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] BankAccountDto bankAccountDto)
    {
        var result = await _service.Update(id, bankAccountDto);
        return Ok(result);
    }

    [HttpGet("number/{number}")]
    public async Task<IActionResult> GetByNumber(string number)
    {
        var account = await _service.GetByNumber(number);
        return Ok(account);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PageParams pageParams, [FromQuery] BankAccountFilter filter)
    {
        var result = await _service.Get(pageParams, filter);
        return Ok(result);
    }
}
