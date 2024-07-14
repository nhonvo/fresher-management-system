using Domain.Enums;

namespace Application.Users.DTO
{
    public class UserContainIdDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public UserRoleType Role { get; set; }
        public UserStatus Status { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}