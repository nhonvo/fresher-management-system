using Domain.Enums;

namespace Application.Class.DTO
{
    public class ClassProgram
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
        public DateTime ClassTimeStart { get; set; }
        public DateTime ClassTimeEnd { get; set; }
        public DateTime ReviewOn { get; set; }
        public DateTime ApproveOn { get; set; }
        public int NumberAttendeePlanned { get; set; }
        public int NumberAttendeeAccepted { get; set; }
        public int NumberAttendeeActual { get; set; }
        public ClassLocation ClassLocation { get; set; }
        public ClassStatus Status { get; set; }
        public TrainingPrograms? TrainingPrograms { get; set; }
    }
    public class TrainingPrograms
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public DateTime LastModify { get; set; }
        public bool IsActive { get; set; } = true;
        public int DaysDuration { get; set; }
        public int TimeDuration { get; set; }
    }
}
