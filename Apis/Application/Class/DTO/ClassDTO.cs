using Domain.Enums;

namespace Application.Class.DTO
{
    public class ClassDTO
    {
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
        public DateTime ClassTimeStart { get; set; }
        public DateTime ClassTimeEnd { get; set; }
        public DateTime ReviewOn { get; set; }
        public DateTime ApproveOn { get; set; }
        public int NumberAttendeePlanned { get; set; }
        public int NumberAttendeeAccepted { get; set; }
        public int NumberAttendeeActual { get; set; }

        // Navigation Properties
        public ClassLocation ClassLocation { get; set; }
        public ClassStatus Status { get; set; }
        public int ReviewByUserId { get; set; }
        public int ApproveByUserId { get; set; }
        public AttendeeType AttendeeType { get; set; }
        public int FSUId { get; set; }
        public string ContactPoint { get; set; }
        public int? TrainingProgramId { get; set; }
    }

}
