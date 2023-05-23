namespace Application.ViewModels.TestAssessmentViewModels
{
    public class GetClassGPAScoreOfStudentViewModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int AttendeeId { get; set; }
        public float? GPA { get; set; }
        public float? studentAverage { get; set; }
        public float? diffFromStudentAverage { get; set; }
        public List<GetListSyllabusScoreOfStudentViewModel> ListSyllabus { get; set; }
    }
}
