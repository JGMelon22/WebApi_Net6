using WebApi_ManProg.Application.DTOs;
using WebApi_ManProg.Application.DTOs.Validations;
using WebApi_ManProg.Application.Services.Interfaces;
using WebApi_ManProg.Domain.Authentication;
using WebApi_ManProg.Domain.Repositories;

namespace WebApi_ManProg.Application.Services;

public class UserService : IUserService
{
    private readonly ITokenGenerator _tokenGenerator;
    /*
     * Necessita dos respositórios de usuário e token...
     * para saber se o usuário e senha em questão existem no banco
     */

    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, ITokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ResultService<dynamic>> GenerateTokenAsync(UserDTO userDTO)
    {
        // Valida se a DTO tem dados
        if (userDTO == null)
            return ResultService.Fail<dynamic>("Objeto deve ser informado!");

        var validator = new UserDTOValidator().Validate(userDTO);
        if (!validator.IsValid)
            return ResultService.RequestError<dynamic>("Ocorreram erros ao validar a operação.", validator);

        // Usuário e senha estão na base?
        var user = await _userRepository.GetUserByEmailAndPasswordAsync(userDTO.Email, userDTO.Senha);
        if (user == null)
            return ResultService.Fail<dynamic>("Usuário e/ou senha não encontrados!");

        // Se achar, gera o token para o usuário em questão
        return ResultService.Ok(_tokenGenerator.Generator(user));
    }
}