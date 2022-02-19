namespace WebApi_ManProg.Domain.Validations;

public class DomainValidationException : Exception
{
    public DomainValidationException(string error) : base(error)
    {
    }

    // Validação genérica em si
    public static void When(bool hasError, string message)
    {
        if (hasError) throw new DomainValidationException(message);
    }
}