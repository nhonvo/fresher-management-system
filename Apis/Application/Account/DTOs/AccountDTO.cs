namespace Application.Account.DTOs;

public record AccountDTO
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int RoleId { get; set; }
    public string Token { get; set; }
    public DateTime? ExpireDay { get; set; }
}
