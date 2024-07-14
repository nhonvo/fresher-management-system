using Domain.Enums;

namespace Application.Class.DTOs
{
    public class ClassDTO
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
    }


}
