#nullable disable warnings


using Domain.Entities.Syllabuses;
using Domain.Entities.Users;
using Domain.Enums.TestAssessmentEnums;

namespace Domain.Entities
{
    public class TestAssessment
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public TestAssessmentType TestAssessmentType { get; set; }
        public int AttendeeId { get; set; }
        public User Attendee { get; set; }
        public int SyllabusId { get; set; }
        public Syllabus Syllabus { get; set; }
    }
}