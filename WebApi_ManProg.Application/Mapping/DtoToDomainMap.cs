using AutoMapper;
using WebApi_ManProg.Application.DTOs;
using WebApi_ManProg.Domain.Entities;

namespace WebApi_ManProg.Application.Mapping;

public class DtoToDomainMap : Profile
{
    public DtoToDomainMap()
    {
        CreateMap<PersonDTO, Person>();
        CreateMap<ProductDTO, Product>();
    }
}