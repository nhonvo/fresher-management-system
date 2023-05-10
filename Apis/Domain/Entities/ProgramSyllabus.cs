namespace Domain.Entities
{
    public class ProgramSyllabus : BaseEntity
    {
        public int TrainingProgramId;
        public TrainingProgram TrainingProgram;
        public int SyllabusId;
        public Syllabus Syllabus;
    }
}
