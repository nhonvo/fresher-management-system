using Domain.Enums;

namespace Domain.Entities
{
    public class Allocate
    {
        public DeliveryType DeliveryType { get; set; }
        public int Time { get; set; }
        public int Percent { get; set; }
        public int Count { get; set; }
        public int Duration { get; set; }
    }
}
