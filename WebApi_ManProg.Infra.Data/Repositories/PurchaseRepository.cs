using Microsoft.EntityFrameworkCore;
using WebApi_ManProg.Domain.Entities;
using WebApi_ManProg.Domain.Repositories;
using WebApi_ManProg.Infra.Data.DataContext;

namespace WebApi_ManProg.Infra.Data.Repositories;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly ApplicationDbContext _dbContext;

    public async Task<Purchase> GetByIdAsync(int id)
    {
        return await _dbContext.Purchase.FirstOrDefaultAsync(x => x.Id == id);
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
}