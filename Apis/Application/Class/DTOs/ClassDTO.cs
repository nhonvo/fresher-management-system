using Domain.Enums;

namespace Application.Class.DTO
{
    public class ClassDTO
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
        public DateTime ClassTimeStart { get; set; }
        public DateTime ClassTimeEnd { get; set; }
        public DateTime ReviewOn { get; set; }
        public DateTime ApproveOn { get; set; }
        public AttendeeType AttendeeType { get; set; }
        public int NumberAttendeePlanned { get; set; }
        public int NumberAttendeeAccepted { get; set; }
        public int NumberAttendeeActual { get; set; }
        public ClassLocation ClassLocation { get; set; }
        public ClassStatus Status { get; set; }
    }
    public class ClassDetail
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
        public DateTime ClassTimeStart { get; set; }
        public DateTime ClassTimeEnd { get; set; }
        public DateTime ReviewOn { get; set; }
        public DateTime ApproveOn { get; set; }
        public AttendeeType AttendeeType { get; set; }
        public int NumberAttendeePlanned { get; set; }
        public int NumberAttendeeAccepted { get; set; }
        public int NumberAttendeeActual { get; set; }
        public ClassLocation ClassLocation { get; set; }
        public ClassStatus Status { get; set; }
        // public ClassAdminDetail ClassAdmin { get; set; }
        // public ClassTrainerDetail ClassTrainer { get; set; }
        // public ClassStudentDetail ClassStudent { get; set; }
        // public ApproveRequestDetail ApproveRequest { get; set; }
        // public TestAssessmentDetail TestAssessment { get; set; }
        // public CalenderDetail Calender { get; set; }
        public TrainingProgramss trainingProgram { get; set; }
    }


}
