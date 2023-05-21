using Domain.Enums;

namespace Application.ViewModels.TestAssessmentViewModels
{
    public class TestAssessmentViewModel
    {
        public float Score { get; set; }
        public TestAssessmentType TestAssessmentType { get; set; }
        public int AttendeeId { get; set; }
        public int SyllabusId { get; set; }
        public int TrainingCLassId { get; set; }
        public ICollection<TestAssessment_TrainingMaterialsViewModel> TrainingMaterials { get; set; }
        public TestAssessment_AttendeeViewModel Attendee { get; set; }
    }
}
