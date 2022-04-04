using Microsoft.AspNetCore.Mvc;
using WebApi_ManProg.Application.DTOs;
using WebApi_ManProg.Application.Services.Interfaces;

namespace WebApi_ManProg.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        // Importandos os serviços
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PersonDTO personDto)
        {
            var result = await _personService.CreateAsync(personDto);

            // Tratamento básico
            if (result.IsSuccess)
                Ok();

            return BadRequest(result);
        }
    }
}