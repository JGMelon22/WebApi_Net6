namespace WebApi_ManProg.Application.DTOs;

public class PersonDTO
{
    public int Id { get; }
    public string Name { get; set; } // private set
    public string Document { get; set; } // private set
    public string Phone { get; set; } // private set
}