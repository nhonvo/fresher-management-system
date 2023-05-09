#nullable disable warnings

using Domain.Entities.Users;
using Domain.Enums.ClassUserEnums;

namespace Domain.Entities
{
    public class ClassUsers
    {
        public int Id { get; set; }
        public ClassRole Role { get; set; }
        public int ClassId { get; set; }
        public int UserId { get; set; }

        public TrainingClass? Class { get; set; }
        public User? User { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}