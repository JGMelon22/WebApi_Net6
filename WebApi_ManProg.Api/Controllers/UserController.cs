using Microsoft.AspNetCore.Mvc;
using WebApi_ManProg.Application.DTOs;
using WebApi_ManProg.Application.Services.Interfaces;

namespace WebApi_ManProg.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // POST - Método Token
    [HttpPost]
    [Route("token")]
    public async Task<IActionResult> PostTokenAsync([FromForm] UserDTO userDTO)
    {
        var result = await _userService.GenerateTokenAsync(userDTO);

        // Validação
        if (result.IsSuccess)
            return Ok(result.Data);

        return BadRequest(result);
    }
}