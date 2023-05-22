using Domain.Enums;

namespace Application.Class.DTO
{
    public class TrainingProgramDuplicate
    {
        public string Name { get; set; }
        public TrainingProgramStatus Status { get; set; }
        public ICollection<ProgramSyllabusDuplicate> ProgramSyllabus { get; set; }
    }
    public class ProgramSyllabusDuplicate
    {
        public int SyllabusId { get; set; }
        public TrainingProgramSyllabusDuplicate Syllabus { get; set; }
    }
    public class TrainingProgramSyllabusDuplicate
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int AttendeeNumber { get; set; }
        public string CourseObjective { get; set; }

        public SyllabusLevel SyllabusLevel { get; set; }

        public float QuizScheme { get; set; }
        public float AssignmentScheme { get; set; }
        public float FinalScheme { get; set; }
        public float FinalTheoryScheme { get; set; }
        public float FinalPracticeScheme { get; set; }
        public float GPAScheme { get; set; }

        // Navigation Property

        public ICollection<TrainingProgramUnitDuplicate> Units { get; set; }
        // public ICollection<ProgramSyllabus>? ProgramSyllabus { get; set; }
        public ICollection<TrainingProgramTestAssessmentDuplicate>? TestAssessments { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public class TrainingProgramTestAssessmentDuplicate
    {

        public float? Score { get; set; }
        public TestAssessmentType TestAssessmentType { get; set; }
        public int AttendeeId { get; set; }
        public int SyllabusId { get; set; }
        public int TrainingClassId { get; set; }
        public ICollection<TrainingProgramTrainingMaterialDuplicate> Materials { get; set; }
    }
    public class TrainingProgramUnitDuplicate
    {
        public string Name { get; init; }
        public int SyllabusSession { get; init; }
        public int UnitNumber { get; init; }
        public List<TrainingProgramLessonDuplicate> UnitLessons { get; init; }
    }
    public class TrainingProgramLessonDuplicate
    {
        public string Name { get; init; }
        public int Duration { get; init; }
        public LessonType LessonType { get; init; }
        public DeliveryType DeliveryType { get; init; }
        public List<TrainingProgramTrainingMaterialDuplicate> TrainingMaterials { get; init; }
    }
    public class TrainingProgramTrainingMaterialDuplicate
    {
        public string FileName { get; init; }
        public string FilePath { get; init; }
        public long FileSize { get; init; }

    }
}
