using WebApi_ManProg.Application.DTOs;

namespace WebApi_ManProg.Application.Services.Interfaces;

public interface IPurchaseService
{
    Task<ResultService<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDto);
}