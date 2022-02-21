using WebApi_ManProg.Domain.Validations;

namespace WebApi_ManProg.Domain.Entities;

public class Purchase
{
    // Validação para inserir
    public Purchase(int productId, int personId, DateTime date)
    {
        Validation(productId, personId, date);
    }

    // Validação para atualizar
    public Purchase(int id, int productId, int personId, DateTime date)
    {
        DomainValidationException.When(id < 0, "O Id é inválido");
        Id = id;
        Validation(productId, personId, date);
    }

    public int Id { get; }
    public int ProductId { get; private set; }
    public int PersonId { get; private set; }
    public DateTime Date { get; private set; }
    public Person Person { get; set; }
    public Product Product { get; private set; } // Auxilia na hora de mapear no Bd

    private void Validation(int productId, int personId, DateTime? date)
    {
        DomainValidationException.When(productId < 0, "O ProductId não pode ser igual a 0!");
        DomainValidationException.When(productId < personId, "O PersonId não pode ser igual a 0!");
        DomainValidationException.When(date.HasValue, "A Data de deve ser informada!");

        ProductId = productId;
        PersonId = personId;
        Date = date.Value;
    }
}