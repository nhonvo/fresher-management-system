#nullable disable warnings

using Domain.Entities.Users;
using Domain.Enums.TMSEnums;

namespace Domain.Entities
{
    public class TMS
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Reason { get; set; }
        public int? CheckedBy { get; set; }
        public User? Admin { get; set; }
        public int TraineeId { get; set; }
        public TMSApproveStatus ApproveStatus { get; set; }
        public User? Trainee { get; set; }
    }
}