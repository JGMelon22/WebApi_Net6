using Microsoft.AspNetCore.Mvc;
using WebApi_ManProg.Application.DTOs;
using WebApi_ManProg.Application.Services;
using WebApi_ManProg.Application.Services.Interfaces;
using WebApi_ManProg.Domain.Validations;

namespace WebApi_ManProg.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public PurchaseController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] PurchaseDTO purchaseDto)
    {
        // Tratando a exception
        try
        {
            var result = await _purchaseService.CreateAsync(purchaseDto);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
        catch (DomainValidationException e)
        {
            var result = ResultService.Fail(e.Message);
            return BadRequest(result);
        }
    }

    // GET
}