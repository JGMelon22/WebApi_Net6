using AutoMapper;
using WebApi_ManProg.Application.DTOs;

namespace WebApi_ManProg.Application.Mapping;

public class DtoToDomainMap : Profile
{
    public DtoToDomainMap()
    {
        CreateMap<PersonDTO, PersonDTO>();
    }
}