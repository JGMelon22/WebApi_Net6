using WebApi_ManProg.Application.DTOs;

namespace WebApi_ManProg.Application.Services.Interfaces;

public interface IPurchaseService
{
    Task<ResultService<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDto);
    Task<ResultService<PurchaseDetailDTO>> GetByIdAsync(int id);
    Task<ResultService<ICollection<PurchaseDetailDTO>>> GetAsync();
    Task<ResultService<PurchaseDTO>> UpdateAsync(PurchaseDTO purchaseDto);
    Task<ResultService> RemoveAsync(int id);
}