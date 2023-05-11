namespace Domain.Entities;

public class FeedBack : BaseEntity
{
    public int Rating { get; set; }
    public string Comment { get; set; }
    public int StudentId { get; set; }
    public int? TraineeId { get; set; }
    public User? Trainee { get; set; }
    public int? TrainerId { get; set; }
    public User? Trainer { get; set; }

}