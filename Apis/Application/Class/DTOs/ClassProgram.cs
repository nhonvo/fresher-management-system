using Domain.Enums;

namespace Application.Class.DTO
{
    public class ClassProgram
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime ReviewOn { get; set; }
        public DateTime ApproveOn { get; set; }
        public int NumberAttendeePlanned { get; set; }
        public int NumberAttendeeAccepted { get; set; }
        public int NumberAttendeeActual { get; set; }
        public ClassLocation Location { get; set; }
        public ClassStatus Status { get; set; }
        public TrainingProgramss? TrainingProgram { get; set; }
    }
    public class TrainingProgramss
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public DateTime LastModify { get; set; }
        public bool IsActive { get; set; } = true;
        public int DaysDuration { get; set; }
        public int TimeDuration { get; set; }
        
    }
}
