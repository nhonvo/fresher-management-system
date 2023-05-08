using Domain.Entities.Users;
using Domain.Enums.RoleEnums;

#nullable disable warnings
namespace Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserPermission? SyllabusPermission { get; set; }
        public UserPermission? TrainingProgramPermission { get; set; }
        public UserPermission? ClassPermission { get; set; }
        public UserPermission? LearningMaterialPermission { get; set; }
        public UserPermission? UserPermission { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
