using Domain.Enums;

namespace Domain.Entities;

public class ApproveRequest : BaseEntity
{
    public StatusApprove Status { get; set; } = StatusApprove.Waiting;
    public int StudentId { get; set; }
    public User Student { get; set; }
    public int ApproveBy { get; set; }
    public User Admin { get; set; }
    public int ClassId { get; set; }
    public TrainingClass TrainingClass { get; set; }
}
