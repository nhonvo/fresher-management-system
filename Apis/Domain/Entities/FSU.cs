namespace Domain.Entities;

#pragma warning disable
public class FSU : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<TrainingClass> TrainingClasses { get; set; }
}
