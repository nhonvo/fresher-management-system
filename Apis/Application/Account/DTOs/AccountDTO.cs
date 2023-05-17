using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.Account.DTOs;

public record AccountDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserRoleType Role { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime? ExpireDay { get; set; }
}
