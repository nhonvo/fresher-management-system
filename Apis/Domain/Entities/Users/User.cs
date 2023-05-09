using Domain.Enums.UserEnums;

namespace Domain.Entities.Users
{
    public partial class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        // public int RoleId { get; set; }
        public UserRole Role { get; set; } // remove this property after add role feature
        public UserLevel? Level { get; set; } = null;
        public UserStatus? Status { get; set; } = null;
        public bool IsMale { get; set; } = true;
        public string AvatarURL { get; set; }
        public string? ResetToken { get; set; } // consider remove
    }
}
