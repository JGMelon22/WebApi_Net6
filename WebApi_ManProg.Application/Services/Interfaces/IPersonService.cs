using WebApi_ManProg.Application.DTOs;

namespace WebApi_ManProg.Application.Services.Interfaces;

public interface IPersonService
{
    // Recebe,grava e devolve 
    Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDto);
}