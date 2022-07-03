using AutoMapper;
using WebApi_ManProg.Application.DTOs;
using WebApi_ManProg.Application.DTOs.Validations;
using WebApi_ManProg.Application.Services.Interfaces;
using WebApi_ManProg.Domain.Entities;
using WebApi_ManProg.Domain.Repositories;

namespace WebApi_ManProg.Application.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IMapper _mapper;
    private readonly IPersonRepository _personRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IUnityOfWork _unityOfWork;

    public PurchaseService(IProductRepository productRepository, IPersonRepository personRepository,
        IPurchaseRepository purchaseRepository, IMapper mapper, IUnityOfWork unityOfWork)
    {
        _productRepository = productRepository;
        _personRepository = personRepository;
        _purchaseRepository = purchaseRepository;
        _mapper = mapper;
        _unityOfWork = unityOfWork;
    }

    public async Task<ResultService<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDto)
    {
        // Verifica se encotra a pessoa e o produto
        if (purchaseDto == null)
            return ResultService.Fail<PurchaseDTO>("Objeto não informado!");

        var validate = new PurchaseDTOValidator().Validate(purchaseDto);
        if (!validate.IsValid)
            return ResultService.RequestError<PurchaseDTO>("Erros ocorreram ao validar.", validate);

        try
        {
            // Abre a transaction
            await _unityOfWork.BeginTransaction();

            // Busca pelo produto e pela pessoa
            var productId = await _productRepository.GetIdByCodErpAsync(purchaseDto.CodErp);

            if (productId == 0) // Produto não existe, cadastrar um novo
            {
                var product =
                    new Product(purchaseDto.ProductName, purchaseDto.CodErp,
                        purchaseDto.Price ?? 0); // Se o valor vier vazio, mete um zero
                await _productRepository.CreateAsync(product);
                productId = product.Id;
            }

            var personId = await _personRepository.GetIdByDocumentAsync(purchaseDto.Document);
            var purchase = new Purchase(productId, personId);

            // Cria os dados
            var data = await _purchaseRepository.CreateAsync(purchase);
            purchaseDto.Id = data.Id;
            await _unityOfWork.Commit();
            return ResultService.Ok(purchaseDto);
        }
        catch (Exception ex)
        {
            await _unityOfWork.Rollback();
            return ResultService.Fail<PurchaseDTO>(ex.Message);
        }
    }

    public async Task<ResultService<PurchaseDetailDTO>> GetByIdAsync(int id)
    {
        var purchase = await _purchaseRepository.GetByIdAsync(id);

        // Procura se o Id existe na base
        if (purchase == null)
            return ResultService.Fail<PurchaseDetailDTO>("Compra não localizada na base de dados!");

        return ResultService.Ok(_mapper.Map<PurchaseDetailDTO>(purchase));
    }

    public async Task<ResultService<ICollection<PurchaseDetailDTO>>> GetAsync()
    {
        var purchases = await _purchaseRepository.GetAllAsync();
        return ResultService.Ok(_mapper.Map<ICollection<PurchaseDetailDTO>>(purchases));
    }

    public async Task<ResultService<PurchaseDTO>> UpdateAsync(PurchaseDTO purchaseDto)
    {
        // Verifica se o objeto foi informado
        if (purchaseDto == null)
            return ResultService.Fail<PurchaseDTO>("O Objeto Deve Ser Informado");

        // Valida o objeto
        var result = new PurchaseDTOValidator().Validate(purchaseDto);

        if (!result.IsValid)
            return ResultService.RequestError<PurchaseDTO>("Erros Ocorreram ao Validar A Operação.", result);

        // Id existe?
        var purchase = await _purchaseRepository.GetByIdAsync(purchaseDto.Id);

        if (purchase == null)
            return ResultService.Fail<PurchaseDTO>("A compra requisitada não existe na base de dados.");

        // Buscando os Ids auxiliares
        var productId = await _productRepository.GetIdByCodErpAsync(purchaseDto.CodErp);
        var personId = await _personRepository.GetIdByDocumentAsync(purchaseDto.Document);
        purchase.Edit(purchase.Id, productId, personId);

        await _purchaseRepository.EditAsync(purchase);
        return ResultService.Ok(purchaseDto);
    }

    public async Task<ResultService> RemoveAsync(int id)
    {
        // Verifica se existe
        var purchase = await _purchaseRepository.GetByIdAsync(id);

        if (purchase == null)
            return ResultService.Fail("Compra Não Encontrada na Base de Dados.");

        await _purchaseRepository.DeleteAsync(purchase);
        return ResultService.Ok($"Compra: {id} Cancelada com Sucesso!");
    }
}