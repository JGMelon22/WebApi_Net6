using WebApi_ManProg.Application.DTOs;

namespace WebApi_ManProg.Application.Services.Interfaces;

public interface IPersonService
{
    // Recebe,grava e devolve 
    Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDto);
    Task<ResultService<ICollection<PersonDTO>>> GetAsync();
    Task<ResultService<PersonDTO>> GetByIdAsync(int id);
    Task<ResultService> UpdateAsync(PersonDTO personDto);
    Task<ResultService> DeleteAsync(int id);
}