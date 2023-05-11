using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TestAssessmentViewModels
{
    public class GetStudentGPAScoreOfClassViewModel
    {
        public int ClassId { get; set; }
        public float GPA { get; set; }
        public List<GetListSyllabusSocreOfStudentViewModel> ListSyllabus { get; set; }
    }
}
