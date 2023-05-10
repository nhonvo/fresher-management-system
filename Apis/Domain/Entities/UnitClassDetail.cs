namespace Domain.Entities;

#pragma warning disable
public class UnitClassDetail : BaseEntity
{
    public int ClassId { get; set; }
    public TrainingClass TrainingClass { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
    public int TrainerId { get; set; }
    public User Trainer { get; set; }
    public int LocationId { get; set; }
    public UnitClassLocation Location { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
