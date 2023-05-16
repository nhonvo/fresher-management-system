using Application.Users.DTO;
using Domain.Enums;

namespace Application.ApproveRequests.DTOs;

public class ApproveRequestDTO
{
    public int Id { get; set; }
    public StatusApprove Status { get; set; }
    public int StudentId { get; set; }
    public UserDTO Student { get; set; }
    public int ClassId { get; set; }
    public TrainingClasses TrainingClass { get; set; }
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
public class ApproveResponseDTO
{
    public int Id { get; set; }
    public StatusApprove Status { get; set; }
    public int StudentId { get; set; }
    public int ClassId { get; set; }
    public int ApprovedBy { get; set; }
}
