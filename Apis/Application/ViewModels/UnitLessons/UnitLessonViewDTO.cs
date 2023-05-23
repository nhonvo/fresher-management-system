using Application.ViewModels.OutputStandards;
using Application.ViewModels.TrainingMaterials;
using Domain.Enums;

namespace Application.ViewModels.UnitLessons
{
    public class UnitLessonViewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public LessonType LessonType { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public OutputStandardViewDTO OutputStandard { get; set; }
        public ICollection<TrainingMaterialViewDTO> TrainingMaterials { get; set; }
    }
}
