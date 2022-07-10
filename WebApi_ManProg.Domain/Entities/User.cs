using WebApi_ManProg.Domain.Validations;

namespace WebApi_ManProg.Domain.Entities;

public class User
{
    public int Id { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    // Construtor com validação simples
    public User(string email, string password)
    {
        Validation(email, password);
    }

    // Construtor completo
    public User(int id, string email, string password)
    {
        DomainValidationException.When(Id <= 0, "Id Inválido.");
        Id = id;
        Validation(email, password);
    }

    // Validar as informações
    private void Validation(string email, string password)
    {
        DomainValidationException.When(string.IsNullOrEmpty(email), "O E-mail deve ser preenchido");
        DomainValidationException.When(string.IsNullOrEmpty(password), "A Senha deve ser preenchida");

        Email = email;
        Password = password;
    }
}