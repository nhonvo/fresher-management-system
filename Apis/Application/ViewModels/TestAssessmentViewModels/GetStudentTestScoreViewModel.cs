using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TestAssessmentViewModels
{
    public class GetStudentTestScoreViewModel
    {
        public float? AverageScore { get; set; }
        public string TestAssessmentType { get; set; }
        public int AttendeeId { get; set; }
        public int SyllabusId { get; set; }
        public int TrainingClassId { get; set; }
        public float? SyllabusScheme { get; set; }
    }
}
