using Domain.Entities;
using Domain.Enums;

namespace Application.Class.DTOs
{
    public class ClassDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime ReviewOn { get; set; }
        public DateTime ApproveOn { get; set; }
        public AttendeeType AttendeeType { get; set; }
        public int NumberAttendeePlanned { get; set; }
        public int NumberAttendeeAccepted { get; set; }
        public int NumberAttendeeActual { get; set; }
        public ClassLocation Location { get; set; }
        public ClassStatus Status { get; set; }
        public TrainingProgramDetail trainingProgram { get; set; }
    }
    public class TrainingProgramDetail
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public DateTime LastModify { get; set; }
        public bool IsActive { get; set; } = true;
        public int DaysDuration { get; set; }
        public int TimeDuration { get; set; }
        public List<ProgramSyllabusDetail> ProgramSyllabus { get; set; }
    }
    public class ProgramSyllabusDetail
    {
        public int SyllabusId { get; set; }
        public SyllabusDetail Syllabus { get; set; }
    }

    public class SyllabusDetail
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

        public List<UnitDetail> Units { get; set; }
        public List<ProgramSyllabusDetail>? ProgramSyllabus { get; set; }
        public List<TestAssessment>? TestAssessments { get; set; }
        public DateTime CreationDate { get; set; }

        public int? CreatedBy { get; set; }
        public User? CreateByUser { get; set; }

        public DateTime? ModificationDate { get; set; }

        public int? ModificationBy { get; set; }
        public User? ModificationByUser { get; set; }
    }
    public class UnitDetail
    {
        public string Name { get; init; }
        public int SyllabusSession { get; init; }
        public int UnitNumber { get; init; }
        public List<LessonDetail> UnitLessons { get; init; }
    }
    public class LessonDetail
    {
        public string Name { get; init; }
        public int Duration { get; init; }
        public LessonType LessonType { get; init; }
        public DeliveryType DeliveryType { get; init; }
        public List<TrainingMaterialDetail> TrainingMaterials { get; init; }
    }
    public class TrainingMaterialDetail
    {
        public string FileName { get; init; }
        public string FilePath { get; init; }
        public long FileSize { get; init; }
    }
}
