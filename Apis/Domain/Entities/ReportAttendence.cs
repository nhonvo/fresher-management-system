using Domain.Enums;

namespace Domain.Entities;

#pragma warning disable
public class ReportAttendence : BaseEntity
{
    public string Reason { get; set; }
    public StatusAttendance statusAttendance { get; set; } = StatusAttendance.Waiting;
    public DateTime expectedDates { get; set; }
    public int StudentId { get; set; }
    public User Student { get; set; }

}
