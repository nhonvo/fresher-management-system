using Domain.Enums;

namespace Domain.Entities;

#pragma warning disable
public class UnitLesson : BaseEntity
{
    public string Name { get; set; }
    public int Duration { get; set; }
    public LessonType LessonType { get; set; }
    public DeliveryType DeliveryType { get; set; }

    //Navigation Property
    public int? OutputStandardId { get; set; }
    public OutputStandard? OutputStandard { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
    public ICollection<TrainingMaterial> TrainingMaterials { get; set; }
}
