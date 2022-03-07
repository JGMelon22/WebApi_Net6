using Microsoft.EntityFrameworkCore;
using WebApi_ManProg.Domain.Entities;
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
}