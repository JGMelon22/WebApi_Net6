using System.Reflection;
using WebApi_ManProg.Domain.Validations;

namespace WebApi_ManProg.Domain.Entities;

public sealed class Person
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Document { get; private set; }
    public string Phone { get; private set; }

    // Id será um dado "Identity" vindo do SQL Server
    // Responsável por inserir
    public Person(string name, string document, string phone)
    {
        Validation(document, name, phone);
    }

    // Responsável por atualizar
    public Person(int id, string document, string name, string phone)
    {
        DomainValidationException.When(id < 0, "O Id deve ser maior que 0!");
        Id = id;
        Validation(document, name, phone);

    }
    
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