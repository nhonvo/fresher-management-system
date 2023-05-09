#nullable disable warnings

using Domain.Entities.Syllabuses;
using Domain.Entities.Users;
using Domain.Enums.ClassEnums;

namespace Domain.Entities
{
    public class TrainingClass : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ClassLocation? Location { get; set; }
        public ClassAttendeeType? AttendeeType { get; set; }
        public ClassFSU? FSU { get; set; }
        public ClassTime ClassTime { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime FinishedOn { get; set; }
        public ClassStatus Status { get; set; }
        public DateTime ApprovedOn { get; set; }
        public int? ApprovedBy { get; set; }
        public User? ApprovedAdmin { get; set; }
        public User? CreatedAdmin { get; set; }
        public int? TrainingProgramId { get; set; }
        public TrainingProgram? TrainingProgram { get; set; }
        public ICollection<ClassUsers> ClassUsers { get; set; }
    }
}