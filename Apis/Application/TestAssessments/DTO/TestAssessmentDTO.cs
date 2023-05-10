using Domain.Enums;

namespace Application.TestAssessments.DTO;

public class TestAssessmentDTO
{
    public int Id { get; set; }
    public float Score { get; set; }
    public TestAssessmentType TestAssessmentType { get; set; }
    public int AttendeeId { get; set; }
    public int SyllabusId { get; set; }
}
