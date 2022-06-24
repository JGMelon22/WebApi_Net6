using WebApi_ManProg.Domain.Repositories;

namespace WebApi_ManProg.Domain.FiltersDb;

public class PersonFilterDb : PagedBaseRequest
{
    // Filtros que serão usados no repositório, no serviço e no conteúdo
    public string Name { get; set; }
}