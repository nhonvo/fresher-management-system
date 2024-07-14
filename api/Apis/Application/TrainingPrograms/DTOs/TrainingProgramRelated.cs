using Domain.Enums;

namespace Application.TrainingPrograms.DTOs
{
    public class TrainingProgramRelated
    {
        public string Name { get; set; }
        public TrainingProgramStatus Status { get; set; }
        public ICollection<TrainingProgramProgramSyllabusRelated> ProgramSyllabus { get; set; }
    }
    public class TrainingProgramProgramSyllabusRelated
    {
        // public int TrainingProgramId { get; set; }
        // public TrainingProgramRelated TrainingProgram { get; set; }
        public int SyllabusId { get; set; }
        public TrainingProgramSyllabusRelated Syllabus { get; set; }
    }
    public class TrainingProgramSyllabusRelated
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

        public ICollection<TrainingProgramUnitRelated> Units { get; set; }
        public ICollection<TrainingProgramTestAssessmentRelated>? TestAssessments { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public class TrainingProgramTestAssessmentRelated
    {
        public float? Score { get; set; }
        public TestAssessmentType TestAssessmentType { get; set; }
        public int AttendeeId { get; set; }
        public int SyllabusId { get; set; }
        public int TrainingClassId { get; set; }
        public ICollection<TrainingProgramTrainingMaterialRelated> Materials { get; set; }
    }
    public class TrainingProgramUnitRelated
    {
        public string Name { get; init; }
        public int SyllabusSession { get; init; }
        public int UnitNumber { get; init; }
        public List<TrainingProgramLessonRelated> Lessons { get; init; }
    }
    public class TrainingProgramLessonRelated
    {
        public string Name { get; init; }
        public int Duration { get; init; }
        public LessonType LessonType { get; init; }
        public DeliveryType DeliveryType { get; init; }
        public List<TrainingProgramTrainingMaterialRelated> TrainingMaterials { get; init; }
    }
    public class TrainingProgramTrainingMaterialRelated
    {
        public string FileName { get; init; }
        public string FilePath { get; init; }
        public long FileSize { get; init; }

    }
}
