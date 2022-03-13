namespace WebApi_ManProg.Application.DTOs;

public class ProductDTO
{
    public int Id { get; }
    public string Name { get; private set; }
    public string CodErp { get; private set; }
    public decimal Price { get; private set; }
}