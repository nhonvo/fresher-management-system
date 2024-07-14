using Domain.Enums;

namespace Application.Users.Queries.ExportUsers;

#pragma warning disable 
public class UserCSV
{
    public string Email { get; init; }
    public Gender Gender { get; init; }
    public string Name { get; init; }
    public string Phone { get; init; }
    public UserRoleType Role { get; init; }
    public UserStatus Status { get; init; }
    public DateTime DateOfBirth { get; init; }
}
