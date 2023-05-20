using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.TestAssessmentViewModels
{
    public class CreateTestAssessmentViewModel
    {
        public float Score { get; set; }
        public TestAssessmentType TestAssessmentType { get; set; }
        public int AttendeeId { get; set; }
        public int SyllabusId { get; set; }
        public int TrainingClassId { get; set; }
        public List<IFormFile> FileMaterials { get; set; }

    }
}
