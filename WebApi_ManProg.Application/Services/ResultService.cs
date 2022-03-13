using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace WebApi_ManProg.Application.Services;

/*
 * Ao tentar cadastrar una pessoa e, caso funcione
 * retorna "Ok", se der falha, retorna uma falha.
 * Se a falha ocorrer por N motivo, retorna um retorno específico (ex: BadRequest)
 */

public class ResultService
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public ICollection<ErrorValidation> Errors { get; set; }

    // Métodos para cada retorno possível
    // Requisição
    public static ResultService RequestError(string message, ValidationResult validationResult)
    {
        return new ResultService
        {
            IsSuccess = false,
            Message = message,
            Errors = validationResult.Errors.Select(x => new ErrorValidation
                {Field = x.PropertyName, Message = x.ErrorMessage}).ToList()
        };
    }

    public static ResultService<T> RequestError<T>(string message, ValidationResult validationResult)
    {
        return new ResultService<T>
        {
            IsSuccess = false,
            Message = message,
            Errors = validationResult.Errors.Select(x => new ErrorValidation
                {Field = x.PropertyName, Message = x.ErrorMessage}).ToList()
        };
    }

    // Outros problemas
    public static ResultService Fail(string message) => new ResultService {IsSuccess = false, Message = message};

    public static ResultService<T> Fail<T>(string message) =>
        new ResultService<T> {IsSuccess = false, Message = message};

    // Quando der sucesso
    public static ResultService Ok(string message) => new ResultService {IsSuccess = true, Message = message};
    public static ResultService<T> Ok<T>(T data) => new ResultService<T> {IsSuccess = true, Data = data};
}

public class ResultService<T> : ResultService
{
    public T Data { get; set; }
}