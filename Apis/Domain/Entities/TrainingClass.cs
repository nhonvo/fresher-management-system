using Domain.Enums;

namespace Domain.Entities;

#pragma warning disable
public class TrainingClass : BaseEntity
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

    // Navigation Properties
    public ClassLocation ClassLocation { get; set; }
    public ClassStatus Status { get; set; }
    public ICollection<ClassAdmin> Admin { get; set; }
    public ICollection<ClassTrainer> ClassTrainers { get; set; }
    public ICollection<ClassStudent> Students { get; set; }
    public int? ReviewByUserId { get; set; }
    public User ReviewBy { get; set; }
    public int? ApproveByUserId { get; set; }
    public User ApproveBy { get; set; }
    public AttendeeType AttendeeType { get; set; }
    public string? ContactPoint { get; set; }
    public int? TrainingProgramId { get; set; }
    public TrainingProgram? TrainingProgram { get; set; }
    public ICollection<ApproveRequest> ApproveRequests { get; set; }
    public ICollection<TestAssessment> TestAssessments { get; set; }
    public ICollection<Calender> Calenders { get; set; }
}
