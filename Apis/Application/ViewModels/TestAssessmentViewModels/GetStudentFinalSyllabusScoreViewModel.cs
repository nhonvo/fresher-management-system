using Domain.Enums;

namespace Application.ViewModels.TestAssessmentViewModels
{
    public class GetStudentFinalSyllabusScoreViewModel
    {
        public float FinalSyllabusScore { get; set; }
        public int SyllabusId { get; set; }
        public int TrainingClassId { get; set; }
        public List<GetStudentTestScoreViewModel> ListAssessment { get; set; }
    }
}
