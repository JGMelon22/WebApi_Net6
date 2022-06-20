using FluentValidation;

namespace WebApi_ManProg.Application.DTOs.Validations;

public class PurchaseDTOValidator : AbstractValidator<PurchaseDTO>
{
    public PurchaseDTOValidator()
    {
        // Valida se o codigo erp e o documento estão sendo informados
        RuleFor(x => x.CodErp)
            .NotEmpty()
            .NotNull()
            .WithMessage("CodErp não informado!");

        RuleFor(x => x.Document)
            .NotEmpty()
            .NotNull()
            .WithMessage("Documento não informado!");
    }
}