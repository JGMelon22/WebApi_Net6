using Microsoft.EntityFrameworkCore;
using WebApi_ManProg.Domain.Entities;
using WebApi_ManProg.Domain.FiltersDb;
using WebApi_ManProg.Domain.Repositories;
using WebApi_ManProg.Infra.Data.DataContext;

namespace WebApi_ManProg.Infra.Data.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PersonRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Person> GetByIdAsync(int id)
    {
        return await _dbContext.People.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Person>> GetPeopleAsync()
    {
        return await _dbContext.People.ToListAsync();
    }

    public async Task<Person> CreateAsync(Person person)
    {
        _dbContext.Add(person);
        await _dbContext.SaveChangesAsync();
        return person;
    }

    public async Task EditAsync(Person person)
    {
        _dbContext.Update(person);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Person person)
    {
        _dbContext.Remove(person);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> GetIdByDocumentAsync(string document)
    {
        return (await _dbContext.People.FirstOrDefaultAsync(x => x.Document == document))?.Id ?? 0;
    }

    public async Task<PagedBaseResponse<Person>> GetPagedAsync(PersonFilterDb request)
    {
        var people = _dbContext.People.AsQueryable();

        // Busca se foi passado um filtro 
        if (!string.IsNullOrEmpty(request.Name))
            people = people.Where(x => x.Name.Contains(request.Name)); // "Contais" similar a SELECT com LIKE

        return await PagedBaseResponseHelper
            .GetResponseAsync<PagedBaseResponse<Person>, Person>(people, request);
    }
}