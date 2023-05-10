using Domain.Enums;

namespace Domain.Entities;

#pragma warning disable
public class TrainingProgram : BaseEntity
{
    public string Name { get; set; }
    public int Duration { get; set; }
    public TrainingProgramStatus Status { get; set; }

    //Navigation properties
    public int? ParentId { get; set; }
    public TrainingProgram? Parent { get; set; }
    public int TrainingClassId { get; set; }
    public TrainingClass TrainingClass { get; set; }
    public ICollection<ProgramSyllabus> ProgramSyllabus { get; set; }
    public DateTime CreationDate { get; set; }
    public int? CreatedBy { get; set; }
    public User? CreateByUser { get; set; }
    public DateTime? ModificationDate { get; set; }
    public int? ModificationBy { get; set; }
    public User? ModificationByUser { get; set; }
}
