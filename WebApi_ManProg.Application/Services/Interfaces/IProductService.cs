using WebApi_ManProg.Application.DTOs;

namespace WebApi_ManProg.Application.Services.Interfaces;

public interface IProductService
{
    // Criar produto
    Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDto);
}