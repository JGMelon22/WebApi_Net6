using AutoMapper;
using WebApi_ManProg.Application.DTOs;
using WebApi_ManProg.Application.DTOs.Validations;
using WebApi_ManProg.Application.Services.Interfaces;
using WebApi_ManProg.Domain.Entities;
using WebApi_ManProg.Domain.Repositories;

namespace WebApi_ManProg.Application.Services;

public class PersonService : IPersonService
{
    private readonly IMapper _mapper;
    private readonly IPersonRepository _personRepository;

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

        return ResultService.Ok(_mapper.Map<PersonDTO>(data));
    }

    public async Task<ResultService<ICollection<PersonDTO>>> GetAsync()
    {
        // Busca as pessoas e as retorna 
        var people = await _personRepository.GetPeopleAsync();
        return ResultService.Ok(_mapper.Map<ICollection<PersonDTO>>(people));
    }

    public async Task<ResultService<PersonDTO>> GetByIdAsync(int id)
    {
        var person = await _personRepository.GetByIdAsync(id);

        if (person == null)
            return ResultService.Fail<PersonDTO>("Pessoa não encontrada");

        return ResultService.Ok(_mapper.Map<PersonDTO>(person));
    }

    public async Task<ResultService> UpdateAsync(PersonDTO personDto)
    {
        // Validando se a DTO está preenchida
        if (personDto == null)
            return ResultService.Fail("Alguns campos não foram preenchidos. Por favor, verifique os");

        // Validando os atributos obrigatórios
        var validation = new PersonDTOValidator().Validate(personDto);

        if (!validation.IsValid)
            return ResultService.RequestError("Ocorreu um erro ao validar os campos.", validation);

        var person = await _personRepository.GetByIdAsync(personDto.Id);

        if (person == null)
            return ResultService.Fail("Pessoa não encontrada no banco de dados.");

        // Caso a pessoa que estamos buscando seja encontrada...
        // Usaremos o o map para pegar as pessoas vindas da DTO
        // E jogar para dentro da pessoa
        person = _mapper.Map(personDto, person);
        await _personRepository.EditAsync(person);
        return ResultService.Ok("Pessoa atualizada com êxito");
    }

    public async Task<ResultService> DeleteAsync(int id)
    {
        // Antes de deletar, precisamos checar se o id passado existe no bd
        var person = await _personRepository.GetByIdAsync(id);

        if (person == null)
            return ResultService.Fail("O id da pessoa informada não existe");

        await _personRepository.DeleteAsync(person);
        return ResultService.Ok($"A pessoa com {id} foi excluída da base com sucesso!");
    }
}