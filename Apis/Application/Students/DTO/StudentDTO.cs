namespace Application.Students.DTO;

public class StudentDTO
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsMale { get; set; } = true;
    public string AvatarURL { get; set; }
}
