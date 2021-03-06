using WebApi_ManProg.Domain.Entities;

namespace WebApi_ManProg.Domain.Repositories;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(int id);
    Task<ICollection<Product>> GetProductsAsync();
    Task<Product> CreateAsync(Product product);
    Task EditAsync(Product product);
    Task DeleteAsync(Product product);

    // Informa o código erp e retorna o Id do produto
    Task<int> GetIdByCodErpAsync(string codErp);
}