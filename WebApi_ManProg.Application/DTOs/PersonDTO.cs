namespace WebApi_ManProg.Application.DTOs;

public class PersonDTO
{
    public int Id { get; }
    public string Name { get; private set; }
    public string Document { get; private set; }
    public string Phone { get; private set; }
}