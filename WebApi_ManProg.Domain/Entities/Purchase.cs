using WebApi_ManProg.Domain.Validations;

namespace WebApi_ManProg.Domain.Entities;

public class Purchase
{
    // Validação para inserir
    public Purchase(int productId, int personId)
    {
        Validation(productId, personId);
    }

    // Validação para atualizar
    public Purchase(int id, int productId, int personId)
    {
        DomainValidationException.When(id < 0, "O Id é inválido");
        Id = id;
        Validation(productId, personId);
    }

    public int Id { get; }
    public int ProductId { get; private set; }
    public int PersonId { get; private set; }
    public DateTime Date { get; private set; }
    public Person Person { get; set; }
    public Product Product { get; private set; } // Auxilia na hora de mapear no Bd

    private void Validation(int productId, int personId)
    {
        DomainValidationException.When(productId <= 0, "O ProductId deve ser informado!");
        DomainValidationException.When(productId <= 0, "O PersonId deve ser informado!");

        ProductId = productId;
        PersonId = personId;
        Date = DateTime.Now;
    }
}