using AutoMapper;
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
    private readonly IMapper _mapper;

    public PurchaseService(IProductRepository productRepository, IPersonRepository personRepository,
        IPurchaseRepository purchaseRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _personRepository = personRepository;
        _purchaseRepository = purchaseRepository;
        _mapper = mapper;
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

    public async Task<ResultService<PurchaseDetailDTO>> GetByIdAsync(int id)
    {
        var purchase = await _purchaseRepository.GetByIdAsync(id);

        // Procura se o Id existe na base
        if (purchase == null)
            return ResultService.Fail<PurchaseDetailDTO>("Compra não localizada na bvase de dados!");

        return ResultService.Ok(_mapper.Map<PurchaseDetailDTO>(purchase));
    }

    public async Task<ResultService<ICollection<PurchaseDetailDTO>>> GetAsync()
    {
        var purchases = await _purchaseRepository.GetAllAsync();
        return ResultService.Ok(_mapper.Map<ICollection<PurchaseDetailDTO>>(purchases));
    }
}