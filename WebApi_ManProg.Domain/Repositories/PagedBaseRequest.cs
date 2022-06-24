namespace WebApi_ManProg.Domain.Repositories;

public class PagedBaseRequest
{
    public PagedBaseRequest()
    {
        Page = 1;
        PageSize = 10;
        OrderByProperty = "Id";
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
    public string OrderByProperty { get; set; } // Campo para ordernar
}