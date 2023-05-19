namespace Domain.Entities;

#pragma warning disable
public class ProgramSyllabus : BaseEntity
{
    public int TrainingProgramId { get; set; }
    public TrainingProgram TrainingProgram { get; set; }
    public int SyllabusId { get; set; }
    public Syllabus Syllabus { get; set; }
}
