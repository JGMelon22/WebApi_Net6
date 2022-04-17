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

        // GET
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var result = await _personService.GetAsync();
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        // GET by Id
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _personService.GetByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        // PUT
        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] PersonDTO personDto)
        {
            var result = await _personService.UpdateAsync(personDto);

            // Tratamento básico
            if (result.IsSuccess)
                Ok();

            return BadRequest(result);
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _personService.DeleteAsync(id);

            // Tratamento básico
            if (result.IsSuccess)
                Ok();

            return BadRequest(result);
        }
    }
}