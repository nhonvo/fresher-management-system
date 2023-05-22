using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TestAssessmentViewModels
{
    public class GetStudentTestScore_TestAssessmentViewModel
    {
        public int Id { get; set; }
        public float? Score { get; set; }
        public ICollection<GetStudentTestScore_TestAssessment_TrainingMaterialViewModel> TrainingMaterials { get; set; }

    }
}
