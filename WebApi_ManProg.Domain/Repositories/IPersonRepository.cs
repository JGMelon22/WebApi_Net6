using WebApi_ManProg.Domain.Entities;

namespace WebApi_ManProg.Domain.Repositories;

public interface IPersonRepository
{
    /* Buscar uma  
     * Buscar todas
     * Cadastrar   
     * Editar      
     * Deletar     
     */

    Task<Person> GetByIdAsync(int id);
    Task<ICollection<Person>> GetPeopleAsync();
    Task<Person> CreateAsync(Person person);
    Task EditAsync(Person person);
    Task DeleteAsync(Person person);
}