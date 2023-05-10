using Domain.Enums;

namespace Domain.Entities;

#pragma warning disable
public class UserRole : BaseEntity
{
    public string Name { get; set; }
    public UserRoleType UserRoleType { get; set; }
    public ICollection<User> Users { get; set; }
}
