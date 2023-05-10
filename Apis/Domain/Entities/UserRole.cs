using Domain.Enums;

namespace Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public string Name { get; set; }
        public UserRoleType UserRoleType { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
