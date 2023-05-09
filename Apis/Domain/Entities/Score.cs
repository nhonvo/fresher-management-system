#nullable disable warnings


using Domain.Entities.Syllabuses;
using Domain.Entities.Users;

namespace Domain.Entities
{
    public class Score
    {
        public int Id { get; set; }
        public int AttendeeId { get; set; }
        public User Attendee { get; set; }
        public int SyllabusId { get; set; }
        public Syllabus Syllabus { get; set; }
    }
}