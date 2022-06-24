using Microsoft.EntityFrameworkCore;
using WebApi_ManProg.Domain.Entities;
using WebApi_ManProg.Domain.Repositories;
using WebApi_ManProg.Infra.Data.DataContext;

namespace WebApi_ManProg.Infra.Data.Repositories;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PurchaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<Purchase>> GetAllAsync()
    {
        return await _dbContext.Purchase
            .Include(x => x.Person) // Lazy loading
            .Include(x => x.Product)
            .ToListAsync();
    }

    public async Task<Purchase> GetByIdAsync(int id)
    {
        return await _dbContext.Purchase
            .Include(x => x.Person) // Lazy loading
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Purchase> CreateAsync(Purchase purchase)
    {
        _dbContext.Add(purchase);
        await _dbContext.SaveChangesAsync();
        return purchase;
    }

    public async Task EditAsync(Purchase purchase)
    {
        _dbContext.Update(purchase);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Purchase purchase)
    {
        _dbContext.Purchase.Remove(purchase);
        await _dbContext.SaveChangesAsync();
    }

    // Person By Id
    public async Task<ICollection<Purchase>> GetByPersonIdAsync(int personId)
    {
        return await _dbContext.Purchase
            .Include(x => x.Person) // Lazy loading
            .Include(x => x.Product)
            .Where(x => x.PersonId == personId).ToListAsync();
    }

    // Product By Id 
    public async Task<ICollection<Purchase>> GetByProductIdAsync(int productId)
    {
        return await _dbContext.Purchase
            .Include(x => x.Product)
            .Include(x => x.Person) // Lazy loading
            .Where(x => x.ProductId == productId).ToListAsync();
    }
}