using Domain.Entities.Users;
using Domain.Entities;
using Domain.Enums.ClassEnums;

namespace Application.Class.DTO
{
    public class ClassDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ClassLocation? Location { get; set; }
        public ClassAttendeeType? AttendeeType { get; set; }
        public ClassFSU? FSU { get; set; }
        public ClassTime ClassTime { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime FinishedOn { get; set; }
        public string LectureStartedTime { get; set; }
        public string LectureFinishedTime { get; set; }
        public int DaysDuration { get; set; }
        public int TimeDuration { get; set; }
        public ClassStatus Status { get; set; }
        public DateTime ApprovedOn { get; set; }
        public int? ApprovedBy { get; set; }
        public int? TrainingProgramId { get; set; }
    }
   
}
