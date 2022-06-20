using WebApi_ManProg.Application.DTOs;

namespace WebApi_ManProg.Application.Services.Interfaces;

public interface IProductService
{
    // Criar produto
    Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDto);
    // Listar todos os produtos
    Task<ResultService<ProductDTO>> GetByIdAsync(int id);
    // Listar produto pelo Id
    Task<ResultService<ICollection<ProductDTO>>> GetAsync();
}