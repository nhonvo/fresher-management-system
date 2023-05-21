using Domain.Enums;

namespace Application.ApproveRequests.DTOs;

public class ApproveResponseDTO
{
    public int Id { get; set; }
    public StatusApprove Status { get; set; }
    public int StudentId { get; set; }
    public int ClassId { get; set; }
    public int ApprovedBy { get; set; }
}
