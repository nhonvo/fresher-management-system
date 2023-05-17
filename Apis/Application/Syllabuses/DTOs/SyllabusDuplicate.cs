using Domain.Entities;
using Domain.Enums;
// TODO: Fix syllabus model dto
namespace Application.Syllabuses.DTO
{
    public class SyllabusDuplicate
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

        public ICollection<SyllabusUnit> Units { get; set; }
        public ICollection<ProgramSyllabus>? ProgramSyllabus { get; set; }
        public ICollection<TestAssessment>? TestAssessments { get; set; }
        public DateTime CreationDate { get; set; }

        public int? CreatedBy { get; set; }
        public User? CreateByUser { get; set; }

        public DateTime? ModificationDate { get; set; }

        public int? ModificationBy { get; set; }
        public User? ModificationByUser { get; set; }
    }
    public record SyllabusUnit
    {
        public string Name { get; init; }
        public int SyllabusSession { get; init; }
        public int UnitNumber { get; init; }
        public List<LessonUnit> UnitLessons { get; init; }
    }
    public class LessonUnit
    {
        public string Name { get; init; }
        public int Duration { get; init; }
        public LessonType LessonType { get; init; }
        public DeliveryType DeliveryType { get; init; }
        public List<LessonTrainingMaterial> TrainingMaterials { get; init; }
    }
    public class LessonTrainingMaterial
    {
        public string FileName { get; init; }
        public string FilePath { get; init; }
        public long FileSize { get; init; }

    }
}