using Domain.Enums;

namespace Domain.Entities
{
    public class Attendance : BaseEntity
    {
        public int AttendanceId { get; set; }
        public int ClassUserId { get; set; }
        public DateTime Day { get; set; }
        public AttendanceStatus? AttendanceStatus { get; set; }
        public string Reason { get; set; }

        public TrainingClass? ClassUser { get; set; }
    }
}
