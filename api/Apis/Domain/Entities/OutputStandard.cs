namespace Domain.Entities;

#pragma warning disable
public class OutputStandard : BaseEntity
{
    public string Code { get; set; }
    public string Description { get; set; }
    public ICollection<Lesson> UnitLessons { get; set; }
}
