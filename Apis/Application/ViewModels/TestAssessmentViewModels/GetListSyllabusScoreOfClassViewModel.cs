namespace Application.ViewModels.TestAssessmentViewModels
{
    public class GetListSyllabusScoreOfClassViewModel
    {
        public int AttendeeId { get; set; }
        public string SyllabusName { get; set; }
        public float FinalScheme { get; set; }
        public float FinalSyllabusScore { get; set; }
        public int SyllabusId { get; set; }
        public List<GetStudentTestScoreViewModel> ListAssessment { get; set; }
    }
}
