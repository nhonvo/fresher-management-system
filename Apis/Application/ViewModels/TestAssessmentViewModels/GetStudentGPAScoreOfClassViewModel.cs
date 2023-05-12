namespace Application.ViewModels.TestAssessmentViewModels
{
    public class GetStudentGPAScoreOfClassViewModel
    {
        public int AttendeeId { get; set; }
        public int ClassId { get; set; }
        public float? GPA { get; set; }
        public List<GetListSyllabusScoreOfClassViewModel> ListSyllabus { get; set; }
    }
}
