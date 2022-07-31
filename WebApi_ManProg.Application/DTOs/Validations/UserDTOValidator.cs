using FluentValidation;

namespace WebApi_ManProg.Application.DTOs.Validations;

public class UserDTOValidator : AbstractValidator<UserDTO>
{
    public UserDTOValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("Email deve ser inforemado!");

        RuleFor(x => x.Senha)
            .NotEmpty()
            .NotNull()
            .WithMessage("Senha deve ser informada!");
    }
}