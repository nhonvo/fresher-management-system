using Domain.Enums;

namespace Application.Students.DTO;

public class StudentDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
}
