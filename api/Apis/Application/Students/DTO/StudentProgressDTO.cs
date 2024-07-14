namespace Application.Students.DTO;

public class StudentProgressDTO
{
    public int ClassId { get; set; }
    public string ClassName { get; set; } = null!;
    public float StudentQuizAvg { get; set; }
    public float StudentAssignmentAvg { get; set; }
    public float StudentFinalTheoryAvg { get; set; }
    public float StudentFinalPracticeAvg { get; set; }
    public float StudentGPA { get; set; }
    public float ClassGPA { get; set; }
}
