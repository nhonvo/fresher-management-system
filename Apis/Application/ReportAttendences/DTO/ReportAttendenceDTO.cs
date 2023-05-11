using Domain.Enums;

namespace Application.ReportAttendences.DTO
{
    public class ReportAttendenceDTO
    {
        public string Reason { get; set; }
        public StatusAttendance statusAttendance { get; set; } = StatusAttendance.Waiting;
        public DateTime expectedDates { get; set; }
        public string StudentId { get; set; }
    }
}
