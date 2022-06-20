using Microsoft.EntityFrameworkCore;
using WebApi_ManProg.Domain.Entities;
using WebApi_ManProg.Domain.Repositories;
using WebApi_ManProg.Infra.Data.DataContext;

namespace WebApi_ManProg.Infra.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _dbContext.Product.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Product>> GetProductsAsync()
    {
        return await _dbContext.Product.ToListAsync();
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _dbContext.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task EditAsync(Product product)
    {
        _dbContext.Update(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _dbContext.Product.Remove(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> GetIdByCodErpAsync(string codErp)
    {
        // Busca o código Erp, se não achar, joga um zero
        return (await _dbContext.Product.FirstOrDefaultAsync(x => x.CodErp == codErp))?.Id ?? 0;
    }
}