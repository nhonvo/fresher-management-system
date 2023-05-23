using Domain.Enums;

namespace Application.TrainingPrograms.DTOs
{
    public class TrainingProgramHasIdRelated
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TrainingProgramStatus Status { get; set; }
        public ICollection<TrainingProgramProgramSyllabusHasIdRelated> ProgramSyllabus { get; set; }
    }
    public class TrainingProgramProgramSyllabusHasIdRelated
    {
        public int Id { get; set; }
        // public int TrainingProgramId { get; set; }
        // public TrainingProgramHasIdRelated TrainingProgram { get; set; }
        public int SyllabusId { get; set; }
        public TrainingProgramSyllabusHasIdRelated Syllabus { get; set; }
    }
    public class TrainingProgramSyllabusHasIdRelated
    {
        public int Id { get; set; }
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

        public ICollection<TrainingProgramUnitHasIdRelated> Units { get; set; }
        public ICollection<TrainingProgramTestAssessmentHasIdRelated>? TestAssessments { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public class TrainingProgramTestAssessmentHasIdRelated
    {
        public int Id { get; set; }
        public float? Score { get; set; }
        public TestAssessmentType TestAssessmentType { get; set; }
        public int AttendeeId { get; set; }
        public int SyllabusId { get; set; }
        public int TrainingClassId { get; set; }
        public ICollection<TrainingProgramTrainingMaterialHasIdRelated> Materials { get; set; }
    }
    public class TrainingProgramUnitHasIdRelated
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public int SyllabusSession { get; init; }
        public int UnitNumber { get; init; }
        public List<TrainingProgramLessonHasIdRelated> Lessons { get; init; }
    }
    public class TrainingProgramLessonHasIdRelated
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public int Duration { get; init; }
        public LessonType LessonType { get; init; }
        public DeliveryType DeliveryType { get; init; }
        public List<TrainingProgramTrainingMaterialHasIdRelated> TrainingMaterials { get; init; }
    }
    public class TrainingProgramTrainingMaterialHasIdRelated
    {
        public int Id { get; set; }
        public string FileName { get; init; }
        public string FilePath { get; init; }
        public long FileSize { get; init; }

    }
}
