using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TestAssessmentViewModels
{
    public class GetClassGPAScoreOfStudentViewModel
    {
        public int ClassId { get; set; }
        public int AttendeeId { get; set; }
        public float? GPA { get; set; }
        public List<GetListSyllabusScoreOfStudentViewModel> ListSyllabus { get; set; }
    }
}
