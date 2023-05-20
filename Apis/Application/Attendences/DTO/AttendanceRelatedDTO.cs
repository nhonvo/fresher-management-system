using Domain.Enums;

namespace Application.Attendances.DTO
{
    public class AttendanceRelatedDTO
    {
        public int Id { get; set; }
        public string? Reason { get; set; }
        public StatusAttendance? AttendanceStatus { get; set; }
        public DateTime Day { get; set; }
        public string AdminName { get; set; }
        public StatusAttendanceApprove ApproveStatus { get; set; }
        public AttendanceRelatedClassStudentDTO ClassStudent { get; set; }
    }
    public class AttendanceRelatedClassStudentDTO
    {
        public AttendanceRelatedStudent Student { get; set; }
        public AttendanceRelatedTrainingClassDTO TrainingClass { get; set; }
    }
    public class AttendanceRelatedStudent
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Gender? Gender { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }

    }
    public class AttendanceRelatedTrainingClassDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<AttendanceRelatedClassTrainers> ClassTrainers { get; set; }
        public List<AttendanceRelatedUserAdmin> ClassAdmins { get; set; }
    }
    public class AttendanceRelatedClassTrainers
    {
        public int TrainerId { get; set; }
        public AttendanceRelatedClassTrainerUser Trainer { get; set; }
        public AttendanceRelatedClassAdminUser Admin { get; set; }
    }
    public class AttendanceRelatedUserAdmin
    {
        public int AdminId { get; set; }
        public AttendanceRelatedClassAdminUser Admin { get; set; }
    }
    public class AttendanceRelatedClassAdminUser
    {
        public string Email { get; set; }
        public Gender? Gender { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public UserRoleType Role { get; set; }
    }
    public class AttendanceRelatedClassTrainerUser
    {
        public string Email { get; set; }
        public Gender? Gender { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public UserRoleType Role { get; set; }
    }
}
// FIXME: DON'T even know why can get trainer data some just send mail for admin
///add class trainer for each class to send mail daily for trainer
