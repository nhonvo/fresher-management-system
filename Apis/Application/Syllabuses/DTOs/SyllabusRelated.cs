using Application.Users.DTO;
using Domain.Entities;
using Domain.Enums;
namespace Application.Syllabuses.DTO
{
    public class SyllabusRelated
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

        public List<SyllabusUnitRelated> Units { get; set; }
        public DateTime CreationDate { get; set; }

        public int? CreatedBy { get; set; }
        public UserContainIdDTO? CreateByUser { get; set; }

        public DateTime? ModificationDate { get; set; }

        public int? ModificationBy { get; set; }
        public UserContainIdDTO? ModificationByUser { get; set; }
    }
    public record SyllabusUnitRelated
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public int SyllabusSession { get; init; }
        public int UnitNumber { get; init; }
        public List<SyllabusLessonRelated> Lessons { get; init; }
    }
    public class SyllabusLessonRelated
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public int Duration { get; init; }
        public LessonType LessonType { get; init; }
        public DeliveryType DeliveryType { get; init; }
        public List<TrainingMaterialRelated> TrainingMaterials { get; init; }
    }
    public class TrainingMaterialRelated
    {
        public int Id { get; set; }
        public string FileName { get; init; }
        public string FilePath { get; init; }
        public long FileSize { get; init; }
    }
}