using Domain.Enums;

namespace Application.ViewModels.TestAssessmentViewModels
{
    public class UpdateTestAssessmentViewModel
    {
        public float Score { get; set; }
        public TestAssessmentType TestAssessmentType { get; set; }
        public int AttendeeId { get; set; }
        public int TraningProgramId { get; set; }
    }
}
