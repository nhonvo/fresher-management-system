using Domain.Enums;

namespace Application.Attendances.DTO
{
    public class AttendanceRelatedFilterDTO
    {
        public int Id { get; set; }
        public string? Reason { get; set; }
        public StatusAttendance? AttendanceStatus { get; set; }
        public DateTime Day { get; set; }
        public string AdminName { get; set; }
        public StatusAttendanceApprove ApproveStatus { get; set; }
        public AttendanceRelatedClassStudentFilterDTO ClassStudent { get; set; }
    }
    public class AttendanceRelatedClassStudentFilterDTO
    {
        // public List<AttendanceRelatedDTO> Attendances { get; set; }
        public AttendanceRelatedStudent Student { get; set; }
        public AttendanceRelatedTrainingClassDTO TrainingClass { get; set; }
    }
}