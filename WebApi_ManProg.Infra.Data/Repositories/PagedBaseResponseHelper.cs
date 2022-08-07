using Microsoft.EntityFrameworkCore;
using WebApi_ManProg.Domain.Repositories;

namespace WebApi_ManProg.Infra.Data.Repositories;

public static class PagedBaseResponseHelper
{
    // Método responsável por retornar o filtro
    /// <summary>
    ///     Passa uma resposta dinâmica e uma entrada dinâmica;
    ///     Por isso o generic
    /// </summary>
    /// <param name="query"></param>
    /// <param name="request"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <returns>TBasedResponse</returns>
    public static async Task<TResponse> GetResponseAsync<TResponse, T>
        (IQueryable<T> query, PagedBaseRequest request)
        where TResponse : PagedBaseResponse<T>, new()
    {
        var response = new TResponse();
        var count = await query.CountAsync(); // Total de linhas / quantidade de páginas informadas
        response.TotalPages = (int)Math.Abs((double)count / request.PageSize);
        response.TotalRegisters = count; // Retorna o total da página

        // Validando
        if (string.IsNullOrEmpty(request.OrderByProperty))
            response.Data = await query.ToListAsync();

        else
            // Se tiver dados, ordena o campo de acordo com o que está vindo 
            response.Data = query.OrderByDynamic(request.OrderByProperty)
                .Skip((request.PageSize - 1) * request.PageSize)
                .Take(request.PageSize).ToList();

        return response;
    }

    // Separamos em métodos a paginação e pegaremos a propriedade
    private static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> query, string propertyName)
    {
        // A propertyName vem do programa e não do banco de dados, logo os nomes são em inglês
        return query.OrderBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null));
    }
}