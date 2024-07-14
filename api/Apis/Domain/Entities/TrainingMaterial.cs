namespace Domain.Entities;

#pragma warning disable
public class TrainingMaterial : BaseEntity
{
    public string FileName { get; set; }
    public string FilePath { get; set; }

    public long FileSize { get; set; }

    //Navigation Property
    public int? UnitLessonId { get; set; }
    public Lesson? UnitLesson { get; set; }
    public int? TestAssessmentId { get; set; }
    public TestAssessment? TestAssessment { get; set; }

    //public DateTime? CreationDate { get; set; }
    //public int? CreatedBy { get; set; }
    //public DateTime? ModificationDate { get; set; }
    //public int? ModificationBy { get; set; }
    //public User? ModifiedByUser { get; set; }
}
