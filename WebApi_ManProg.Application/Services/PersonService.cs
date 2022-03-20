using AutoMapper;
using WebApi_ManProg.Application.DTOs;
using WebApi_ManProg.Application.DTOs.Validations;
using WebApi_ManProg.Application.Services.Interfaces;
using WebApi_ManProg.Domain.Entities;
using WebApi_ManProg.Domain.Repositories;

namespace WebApi_ManProg.Application.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonService(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDto)
    {
        if (personDto == null)
            return ResultService.Fail<PersonDTO>("Objeto deve informado");

        var result = new PersonDTOValidator().Validate(personDto);
        if (!result.IsValid)
            return ResultService.RequestError<PersonDTO>("Problemas de validade", result);

        var person = _mapper.Map<Person>(personDto);
        var data = await _personRepository.CreateAsync(person);

        return ResultService.Ok<PersonDTO>(_mapper.Map<PersonDTO>(data));
    }
}