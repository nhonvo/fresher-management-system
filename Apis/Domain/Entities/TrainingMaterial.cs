namespace Domain.Entities;

#pragma warning disable
public class TrainingMaterial : BaseEntity
{
    public string FileName { get; set; }
    public string FilePath { get; set; }

    public long FileSize { get; set; }


    //Navigation Property
    public int? UnitLessonId { get; set; }
    public UnitLesson? UnitLesson { get; set; }    
    public int? TestAssessmentId { get; set; }
    public TestAssessment? TestAssessment { get; set; }
}
