using Domain.Entities.Users;
using Domain.Enums.ClassEnums;

namespace Domain.Entities
{
    public class ClassUnitDetail
    {
        public int ClassId { get; set; }
        public int UnitId { get; set; }

        public ClassLocation? Location { get; set; }
        public int? TrainerId { get; set; }
        public int? DayNo { get; set; }
        public DateTime? OperationDate { get; set; }

        public TrainingClass Class { get; set; }
        public Unit Unit { get; set; }
        public User? Trainer { get; set; }
    }
}
