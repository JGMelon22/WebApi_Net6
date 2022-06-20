using AutoMapper;
using WebApi_ManProg.Application.DTOs;
using WebApi_ManProg.Application.DTOs.Validations;
using WebApi_ManProg.Application.Services.Interfaces;
using WebApi_ManProg.Domain.Entities;
using WebApi_ManProg.Domain.Repositories;

namespace WebApi_ManProg.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDto)
    {
        // Validando
        if (productDto is null)
            return ResultService.Fail<ProductDTO>("O Objeto deve ser informado");

        var result = new ProductDTOValidator().Validate(productDto);
        if (!result.IsValid)
            return ResultService.RequestError<ProductDTO>("Erro ao realizar a validação", result);

        // DTO --> Entidade
        var product = _mapper.Map<Product>(productDto);
        var data = await _productRepository.CreateAsync(product);

        // Dando certo
        return ResultService.Ok<ProductDTO>(_mapper.Map<ProductDTO>(data));
    }

    public async Task<ResultService<ProductDTO>> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        // Valida
        if (product == null)
            return ResultService.Fail<ProductDTO>("Produto com o Id buscado não existe na base de dados.");

        return ResultService.Ok<ProductDTO>(_mapper.Map<ProductDTO>(product));
    }

    public async Task<ResultService<ICollection<ProductDTO>>> GetAsync()
    {
        var products = await _productRepository.GetProductsAsync();
        return ResultService.Ok<ICollection<ProductDTO>>(_mapper.Map<ICollection<ProductDTO>>(products));
    }

    public async Task<ResultService> UpdateAsync(ProductDTO productDTO)
    {
        // DTO nula ou não
        if (productDTO == null)
            return ResultService.Fail("O objeto deve ser informado");

        var validation = new ProductDTOValidator().Validate(productDTO);
        if (!validation.IsValid)
            return ResultService.RequestError("Ocorreu um problema ao validar.", validation);

        // Tudo joia, busca
        var product = await _productRepository.GetByIdAsync(productDTO.Id);

        if (product == null)
            return ResultService.Fail("Produto não encontrado na base dados.");

        product = _mapper.Map<ProductDTO, Product>(productDTO, product);
        await _productRepository.EditAsync(product);

        return ResultService.Ok("Produto editado com sucesso!");
    }

    public async Task<ResultService> RemoveAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        // Verifica se o produto existe
        if (product == null)
            return ResultService.Fail("Produto não existe na base de dados para deletar.");

        await _productRepository.DeleteAsync(product);
        return ResultService.Ok($"Produto com Id {id} deletado com sucesso!");
    }
}