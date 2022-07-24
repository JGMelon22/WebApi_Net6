using WebApi_ManProg.Domain.Entities;

namespace WebApi_ManProg.Domain.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
}