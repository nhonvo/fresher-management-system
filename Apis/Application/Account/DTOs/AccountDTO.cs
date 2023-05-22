using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.Account.DTOs;

public record AccountDTO
{
    public int Id { get; set; }
    public string Email { get; set; }
    public Gender Gender { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public UserRoleType Role { get; set; }
    public UserStatus Status { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime? ExpireDay { get; set; }
}
