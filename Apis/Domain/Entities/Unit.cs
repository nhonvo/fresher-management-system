namespace Domain.Entities;

#pragma warning disable
public class Unit : BaseEntity
{
    public string Name { get; set; }
    public int SyllabusSession { get; set; }
    public int UnitNumber { get; set; }

    //Navigation Property
    public int SyllabusId { get; set; }
    public Syllabus Syllabus { get; set; }
    public ICollection<Lesson> Lessons { get; set; }
    public DateTime CreationDate { get; set; }

    public int? CreatedBy { get; set; }
    public User? CreateByUser { get; set; }

    public DateTime? ModificationDate { get; set; }

    public int? ModificationBy { get; set; }
    public User? ModificationByUser { get; set; }
}
