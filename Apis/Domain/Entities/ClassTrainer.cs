namespace Domain.Entities;

public class ClassTrainer : BaseEntity
{
    public int TrainingClassId { get; set; }
    public TrainingClass TrainingClass { get; set; }
    public int TrainerId { get; set; }
    public User Trainer { get; set; }
    public DateTime? CreationDate { get; set; }
    public int? CreatedBy { get; set; }
    public User? CreateByUser { get; set; }
    public DateTime? ModificationDate { get; set; }
    public int? ModificationBy { get; set; }
    public User? ModificationByUser { get; set; }
}
