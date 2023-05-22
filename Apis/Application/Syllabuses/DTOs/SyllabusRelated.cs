using Application.Users.DTO;
using Domain.Entities;
using Domain.Enums;
namespace Application.Syllabuses.DTOs
{
    public class SyllabusRelated
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int AttendeeNumber { get; set; }
        public string CourseObjective { get; set; }
        public int Duration { get; set; }

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
        public string Name { get; set; }
        public int SyllabusSession { get; set; }
        public int UnitNumber { get; set; }
        public List<SyllabusLessonRelated> Lessons { get; set; }
    }
    public class SyllabusLessonRelated
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public LessonType LessonType { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public List<TrainingMaterialRelated> TrainingMaterials { get; set; }
    }
    public class TrainingMaterialRelated
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
    }
}