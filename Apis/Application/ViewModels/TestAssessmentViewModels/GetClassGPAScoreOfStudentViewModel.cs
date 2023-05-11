using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TestAssessmentViewModels
{
    public class GetClassGPAScoreOfStudentViewModel
    {
        public int AttendeeId { get; set; }
        public float GPA { get; set; }
        public List<GetListSyllabusScoreOfClassViewModel> ListSyllabus { get; set; }
    }
}
