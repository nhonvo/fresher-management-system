namespace Domain.Entities;

#pragma warning disable
public class Calender : BaseEntity
{
    public int TrainingClassId { get; set; }
    public DateTime Date { get; set; }
    public int Order { get; set; }
    public int Count { get; set; }
    //ref
    public TrainingClass TrainingClass { get; set; }
    public User DeletedByUser { get; set; }
}
