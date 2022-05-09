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
}