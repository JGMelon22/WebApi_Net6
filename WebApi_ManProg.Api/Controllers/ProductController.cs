using Microsoft.AspNetCore.Mvc;
using WebApi_ManProg.Application.DTOs;
using WebApi_ManProg.Application.Services.Interfaces;

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
        
        if(result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}