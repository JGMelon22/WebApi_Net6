using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using WebApi_ManProg.Domain.Authentication;
using WebApi_ManProg.Domain.Entities;

namespace WebApi_ManProg.Infra.Data.DataContext;

public class TokenGenerator : ITokenGenerator
{
    public dynamic Generator(User user) // Claim necessário para gerar um token válido para o usuário
    {
        var claims = new List<Claim>()
        {
            new Claim("Email", user.Email),
            new Claim("Id", user.Id.ToString()) // O Id na model "User" é um inteiro, por isso a conversão explícita
        };

        // Tempo válido para o token gerado e nossa chave 
        var expiration = DateTime.Now.AddDays(1);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Net6DecoupledAPI"));
        var tokenData =
            new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                expires: expiration,
                claims: claims);

        // Gera o token e retorna
        var token = new JwtSecurityTokenHandler().WriteToken(tokenData);

        return new
        {
            access_toke = token,
            expirations = expiration
        };
    }
}