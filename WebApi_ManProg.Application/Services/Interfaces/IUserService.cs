using WebApi_ManProg.Application.DTOs;

namespace WebApi_ManProg.Application.Services.Interfaces;

public interface IUserService
{
    Task<ResultService<dynamic>> GenerateTokenAsync(UserDTO userDTO);
}