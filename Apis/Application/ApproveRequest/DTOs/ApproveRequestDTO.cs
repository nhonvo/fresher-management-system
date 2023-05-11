using Application.Attendances.DTOs;
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
