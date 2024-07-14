﻿namespace Application.ViewModels.TestAssessmentViewModels
{
    public class GetListSyllabusScoreOfStudentViewModel
    {
        public float FinalSyllabusScore { get; set; }
        public int SyllabusId { get; set; }
        public string SyllabusName { get; set; }
        public int TrainingClassId { get; set; }
        public float FinalScheme { get; set; }
        public List<GetStudentTestScoreViewModel> ListAssessment { get; set; }
    }
}
