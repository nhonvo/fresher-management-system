using System.Text.Json.Serialization;
using Domain.Enums;

namespace Application.Account.DTOs;

public record AccountDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserRoleType Role { get; set; }
    public string Token { get; set; }
    public DateTime? ExpireDay { get; set; }
}
