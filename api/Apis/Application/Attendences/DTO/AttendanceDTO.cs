using Domain.Enums;

namespace Application.Attendances.DTO
{
    public class AttendanceDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string ClassName { get; set; }
        public string? Reason { get; set; }
        public StatusAttendance? AttendanceStatus { get; set; }
        public DateTime Day { get; set; }
        public int? AdminId { get; set; }
        public StatusAttendanceApprove ApproveStatus { get; set; } = StatusAttendanceApprove.Pending;
        public DateTime? CreationDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? ModificationBy { get; set; }
    }
}
