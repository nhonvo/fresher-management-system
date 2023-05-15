using Domain.Enums;

namespace Application.Class.DTOs
{
    public class AdminClass
    {
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
        public List<Admin> Admin { get; set; }
    }
    public class Admin
    {
        public string Email { get; set; }
        public Gender? Gender { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public UserRoleType Role { get; set; }
    }
}