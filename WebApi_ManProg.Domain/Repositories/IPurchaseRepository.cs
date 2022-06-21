using WebApi_ManProg.Domain.Entities;

namespace WebApi_ManProg.Domain.Repositories;

public interface IPurchaseRepository
{
    Task<Purchase> GetByIdAsync(int id);
    Task<ICollection<Purchase>> GetAllAsync();
    Task<Purchase> CreateAsync(Purchase purchase);
    Task EditAsync(Purchase purchase);
    Task DeleteAsync(Purchase purchase);
}