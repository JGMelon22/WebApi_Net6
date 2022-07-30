using WebApi_ManProg.Domain.Entities;

namespace WebApi_ManProg.Domain.Authentication;

public interface ITokenGenerator
{
    dynamic Generator(User user);
}