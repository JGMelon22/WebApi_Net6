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
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _purchaseService.GetAsync();
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    // GET by Id
    // GET
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var result = await _purchaseService.GetByIdAsync(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    // Editar
    [HttpPut]
    public async Task<IActionResult> EditAsync([FromBody] PurchaseDTO purchaseDto)
    {
        // Tratando a exception
        try
        {
            var result = await _purchaseService.UpdateAsync(purchaseDto);
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

    // Remover
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> RemoveAsync(int id)
    {
        var result = await _purchaseService.RemoveAsync(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}