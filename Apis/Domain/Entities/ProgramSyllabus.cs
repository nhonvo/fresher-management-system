namespace Domain.Entities;

#pragma warning disable
public class ProgramSyllabus : BaseEntity
{
    public int TrainingProgramId;
    public TrainingProgram TrainingProgram;
    public int SyllabusId;
    public Syllabus Syllabus;
}
