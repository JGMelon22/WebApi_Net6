using Microsoft.AspNetCore.Mvc;
using WebApi_ManProg.Application.DTOs;
using WebApi_ManProg.Application.Services.Interfaces;
using WebApi_ManProg.Domain.Entities;

namespace WebApi_ManProg.Api.Controllers;

// Anotações
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // Método responsável por cadastrar um produto
    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] ProductDTO productDto)
    {
        var result = await _productService.CreateAsync(productDto);

        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    // Produtos
    [HttpGet]
    public async Task<ActionResult> GetAsync()
    {
        var result = await _productService.GetAsync();
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var result = await _productService.GetByIdAsync(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] ProductDTO productDto)
    {
        var result = await _productService.UpdateAsync(productDto);

        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var result = await _productService.RemoveAsync(id);

        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}