namespace WebApi_ManProg.Application.DTOs;

public class PagedBaseResponseDTO<T>
{
    public PagedBaseResponseDTO(int totalRegisters, List<T> data)
    {
        TotalRegisters = totalRegisters;
        Data = data;
    }

    public int TotalRegisters { get; }
    public List<T> Data { get; }
}