using Domain.Enums;

namespace Domain.Entities;

#pragma warning disable
public class Attendance : BaseEntity
{
    public int StudentId { get; set; }
    public ClassStudent ClassStudent { get; set; }
    public string? Reason { get; set; }
    public StatusAttendance? AttendanceStatus { get; set; }
    public DateTime Day { get; set; }
    public int? AdminId { get; set; }
    public User? Admin { get; set; }
    public StatusAttendanceApprove ApproveStatus { get; set; } = StatusAttendanceApprove.Pending;
    public DateTime? CreationDate { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? ModificationDate { get; set; }
    public int? ModificationBy { get; set; }
}
