namespace Domain.Entities
{
    public class OutputStandard : BaseEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public ICollection<UnitLesson> UnitLessons { get; set; }
    }
}
