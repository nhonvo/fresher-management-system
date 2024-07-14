namespace Application.ViewModels.TestAssessmentViewModels
{
    public class GetStudentGPAScoreOfClassViewModel
    {
        public int AttendeeId { get; set; }
        public string AttendeeName { get; set; }
        public int ClassId { get; set; }
        public float? GPA { get; set; }
        public float? classAverage { get; set; }
        public float? diffFromClassAverage { get; set; }
        public List<GetListSyllabusScoreOfClassViewModel> ListSyllabus { get; set; }
    }
}
