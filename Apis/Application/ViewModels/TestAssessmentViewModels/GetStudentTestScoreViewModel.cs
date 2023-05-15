namespace Application.ViewModels.TestAssessmentViewModels
{
    public class GetStudentTestScoreViewModel
    {
        public int AttendeeId { get; set; }
        public int SyllabusId { get; set; }
        public int TrainingClassId { get; set; }
        public float? AverageScore { get; set; }
        public int NumberOfTests { get; set; }
        public string TestAssessmentType { get; set; }
        public float? SyllabusScheme { get; set; }
    }
}
