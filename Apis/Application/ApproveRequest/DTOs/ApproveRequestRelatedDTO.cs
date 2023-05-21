using Application.Users.DTO;
using Domain.Enums;

namespace Application.ApproveRequests.DTOs;

public class ApproveRequestRelatedDTO
{
    public int Id { get; set; }
    public StatusApprove Status { get; set; }
    public int StudentId { get; set; }
    public UserDTO? Student { get; set; }
    public int? ApproveBy { get; set; }
    public UserDTO? Admin { get; set; }
    public int ClassId { get; set; }
    public ApproveRequestTrainingClass? TrainingClass { get; set; }
}
public class ApproveRequestTrainingClass
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTime ClassTimeStart { get; set; }
    public DateTime ClassTimeEnd { get; set; }
    public DateTime ReviewOn { get; set; }
    public DateTime ApproveOn { get; set; }
    public int NumberAttendeePlanned { get; set; }
    public int NumberAttendeeAccepted { get; set; }
    public int NumberAttendeeActual { get; set; }
    public ClassLocation Location { get; set; }
    public ClassStatus Status { get; set; }
}
