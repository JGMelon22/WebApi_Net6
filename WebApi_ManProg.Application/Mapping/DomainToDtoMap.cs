using AutoMapper;
using WebApi_ManProg.Application.DTOs;
using WebApi_ManProg.Domain.Entities;

namespace WebApi_ManProg.Application.Mapping;

public class DomainToDtoMap : Profile
{
    public DomainToDtoMap()
    {
        CreateMap<Person, PersonDTO>();
        CreateMap<Product, ProductDTO>();
        CreateMap<Purchase,
                PurchaseDetailDTO>() // Precisamos pegar o nome da pessoa. Sem ignorar, pegariamos a entidade
            .ForMember(x => x.Person,
                opt => opt.Ignore())
            .ForMember(x => x.Product, opt => opt.Ignore())
            // Costumizando os campos
            .ConstructUsing((model, context) =>
            {
                var dto = new PurchaseDetailDTO
                {
                    Product = model.Product.Name,
                    Id = model.Id,
                    Date = model.Date,
                    Person = model.Person.Name
                };

                return dto;
            });
    }
}