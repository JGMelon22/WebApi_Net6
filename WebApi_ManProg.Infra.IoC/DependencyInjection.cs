using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi_ManProg.Application.Mapping;
using WebApi_ManProg.Application.Services.Interfaces;
using WebApi_ManProg.Domain.Repositories;
using WebApi_ManProg.Infra.Data.DataContext;

namespace WebApi_ManProg.Infra.IoC;

public static class DependencyInjection
{
    // Injetar os serviços pertencentes a infraestrutura 
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(""))); // Aqui vai a string de conexão do banco

        // As demais classes serão adicionadas a DI posteriormente
        services.AddScoped<IPersonRepository, IPersonRepository>();
        return services;
    }

    // Injetar os serviços
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(DomainToDtoMap));
        services.AddScoped<IPersonService, IPersonService>();

        return services;
    }
}