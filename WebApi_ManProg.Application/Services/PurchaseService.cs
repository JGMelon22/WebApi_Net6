using WebApi_ManProg.Application.DTOs;
using WebApi_ManProg.Application.DTOs.Validations;
using WebApi_ManProg.Application.Services.Interfaces;
using WebApi_ManProg.Domain.Entities;
using WebApi_ManProg.Domain.Repositories;

namespace WebApi_ManProg.Application.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IPersonRepository _personRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPurchaseRepository _purchaseRepository;

    public PurchaseService(IProductRepository productRepository, IPersonRepository personRepository,
        IPurchaseRepository purchaseRepository)
    {
        _productRepository = productRepository;
        _personRepository = personRepository;
        _purchaseRepository = purchaseRepository;
    }

    public async Task<ResultService<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDto)
    {
        // Verifica se encotra a pessoa e o produto
        if (purchaseDto == null)
            return ResultService.Fail<PurchaseDTO>("Objeto não informado!");

        var validate = new PurchaseDTOValidator().Validate(purchaseDto);
        if (!validate.IsValid)
            return ResultService.RequestError<PurchaseDTO>("Erros ocorreram ao validar.", validate);

        // Busca pelo produto e pela pessoa
        var productId = await _productRepository.GetIdByCodErpAsync(purchaseDto.CodErp);
        var personId = await _personRepository.GetIdByDocumentAsync(purchaseDto.Document);
        var purchase = new Purchase(productId, personId);

        // Cria os dados
        var data = await _purchaseRepository.CreateAsync(purchase);
        purchaseDto.Id = data.Id;

        return ResultService.Ok(purchaseDto);
    }
}