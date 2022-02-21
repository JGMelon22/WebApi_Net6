using WebApi_ManProg.Domain.Validations;

namespace WebApi_ManProg.Domain.Entities;

public sealed class Product
{
    // ctor para adicionar
    public Product(string name, string codErp, decimal price)
    {
        Validation(name, codErp, price);
    }

    // ctor para editar
    public Product(int id, string name, string codErp, decimal price)
    {
        DomainValidationException.When(id < 0, "O Id do produto deve ser maior que 0!");
        Id = id;
        Validation(name, codErp, price);
    }

    public int Id { get; }
    public string Name { get; private set; }
    public string CodErp { get; private set; }
    public decimal Price { get; private set; }
    public ICollection<Person> Persons { get; set; } // E um produto pode ser comprado por mais de uma pessoa
    public ICollection<Purchase> Purchases { get; set; } // E um produto pode ser comprado por mais de uma pessoa

    // Validação
    private void Validation(string name, string codErp, decimal price)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name), "O nome deve ser informado!");
        DomainValidationException.When(string.IsNullOrEmpty(codErp), "O Código erp deve ser informado!");
        DomainValidationException.When(price < 0, "O preço deve ser informado!");

        Name = name;
        CodErp = codErp;
        Price = price;
    }
}