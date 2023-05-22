using Application.Users.DTO;
using Domain.Enums;

namespace Application.Class.DTOs
{
    public class ClassRelated
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime ReviewOn { get; set; }
        public DateTime ApproveOn { get; set; }
        public int NumberAttendeePlanned { get; set; }
        public int NumberAttendeeAccepted { get; set; }
        public int NumberAttendeeActual { get; set; }

        // Navigation Properties
        public ClassLocation Location { get; set; }
        public ClassStatus Status { get; set; }
        public List<ClassClassAdminRelated> ClassAdmins { get; set; }
    }
    public class ClassClassAdminRelated
    {
        public int AdminId { get; set; }
        public UserDTO Admin { get; set; }
    }
}