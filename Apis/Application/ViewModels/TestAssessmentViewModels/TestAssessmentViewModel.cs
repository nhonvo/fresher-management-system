using Domain.Enums;

namespace Application.ViewModels.TestAssessmentViewModels
{
    public class TestAssessmentViewModel
    {
        public float Score { get; set; }
        public TestAssessmentType TestAssessmentType { get; set; }
        public int AttendeeId { get; set; }
        public int SyllabusId { get; set; }
    }
}
