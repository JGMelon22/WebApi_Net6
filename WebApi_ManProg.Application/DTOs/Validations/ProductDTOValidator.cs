using FluentValidation;

namespace WebApi_ManProg.Application.DTOs.Validations;

public class ProductDTOValidator : AbstractValidator<ProductDTO>
{
    public ProductDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("O nome do produto deve ser informado!");

        RuleFor(x => x.CodErp)
            .NotEmpty()
            .NotNull()
            .WithMessage("O código Erp deve ser informado!");

        // Regra para o preco (incompleta)
        RuleFor(x => x.Price)
            .NotEmpty()
            .NotNull()
            .WithMessage("O preço deve ser informado e maior do que zero!");
    }
}