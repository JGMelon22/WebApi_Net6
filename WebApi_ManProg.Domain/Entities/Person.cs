using WebApi_ManProg.Domain.Validations;

namespace WebApi_ManProg.Domain.Entities;

public sealed class Person
{
    // Id será um dado "Identity" vindo do SQL Server
    // Responsável por inserir
    public Person(string name, string document, string phone)
    {
        Validation(document, name, phone);
        Purchases = new List<Purchase>();
    }

    // Responsável por atualizar
    public Person(int id, string document, string name, string phone)
    {
        DomainValidationException.When(id < 0, "O Id deve ser maior que 0!");
        Id = id;
        Validation(document, name, phone);
        Purchases = new List<Purchase>();
    }

    public int Id { get; }
    public string Name { get; private set; }
    public string Document { get; private set; }
    public string Phone { get; private set; }
    public ICollection<Purchase> Purchases { get; set; } // Uma pessoa pode ter mais uma compra
    public ICollection<Product> Product { get; set; }

    // Chama a validação genérica
    private void Validation(string document, string name, string phone)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name), "O nome deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(document), "O documento deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(phone), "O número de telefone deve ser informado!");

        Name = name;
        Document = document;
        Phone = phone;
    }
}