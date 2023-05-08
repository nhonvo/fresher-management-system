#nullable disable warnings

using Domain.Entities.Users;
using Domain.Enums.GradeReportEnums;

namespace Domain.Entities
{
    public class GradeReport
    {
        public int Id { get; set; }
        public GradeReportType Type { get; set; }
        public DateTime GradedOn { get; set; }
        public float Grade { get; set; }

        public int TraineeId { get; set; }
        public User? User { get; set; }
        public int LectureId { get; set; }
        public Lecture? Lecture { get; set; }
    }
}