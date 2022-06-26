using WebApi_ManProg.Domain.Entities;
using WebApi_ManProg.Domain.FiltersDb;

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

    // Passa o documento e retorna o Id pessoa
    Task<int> GetIdByDocumentAsync(string document);

    // Busca os dados paginados
    Task<PagedBaseResponse<Person>> GetPagedAsync(PersonFilterDb request);
}