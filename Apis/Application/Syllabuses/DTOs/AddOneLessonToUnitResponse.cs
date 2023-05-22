using Domain.Enums;

namespace Application.Syllabuses.DTOs
{
    public class AddOneLessonToUnitResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public LessonType LessonType { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public int UnitId { get; set; }
    }
}