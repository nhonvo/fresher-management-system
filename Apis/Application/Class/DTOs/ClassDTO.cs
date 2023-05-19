using Domain.Enums;

namespace Application.Class.DTO
{
    public class ClassDTO
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
        public DateTime ClassTimeStart { get; set; }
        public DateTime ClassTimeEnd { get; set; }
        public DateTime ReviewOn { get; set; }
        public DateTime ApproveOn { get; set; }
        public AttendeeType AttendeeType { get; set; }
        public int NumberAttendeePlanned { get; set; }
        public int NumberAttendeeAccepted { get; set; }
        public int NumberAttendeeActual { get; set; }
        public ClassLocation ClassLocation { get; set; }
        public ClassStatus Status { get; set; }
    }
    public class ClassDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime ReviewOn { get; set; }
        public DateTime ApproveOn { get; set; }
        public AttendeeType AttendeeType { get; set; }
        public int NumberAttendeePlanned { get; set; }
        public int NumberAttendeeAccepted { get; set; }
        public int NumberAttendeeActual { get; set; }
        public ClassLocation Location { get; set; }
        public ClassStatus Status { get; set; }
        public TrainingProgramss trainingProgram { get; set; }
    }


}
