using Domain.Enums;

namespace Application.Account.DTOs;

public class AttendanceDTO
{
    public int AttendanceId { get; set; }
    public int ClassUserId { get; set; }
    public DateTime Day { get; set; }
    public AttendanceStatus? AttendanceStatus { get; set; }
    public string Reason { get; set; }
    public TrainingClasses? ClassUser { get; set; }

}
public class TrainingClasses
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
}