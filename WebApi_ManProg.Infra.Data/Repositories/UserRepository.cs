using Microsoft.EntityFrameworkCore;
using WebApi_ManProg.Domain.Entities;
using WebApi_ManProg.Domain.Repositories;
using WebApi_ManProg.Infra.Data.DataContext;

namespace WebApi_ManProg.Infra.Data.Repositories;

public class UserRepository : IUserRepository
{
    // DI
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        // Verifica se existe os dados no banco. Se existir, retorna eles. Ademais, traz nullo
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
    }
}