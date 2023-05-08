#nullable disable warnings

using Domain.Entities.Common;
using Domain.Entities.Users;

namespace Domain.Entities
{
    public class TrainingMaterial : BaseModel
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public int LectureId { get; set; }
        public Lecture? Lecture { get; set; }
        public User? User { get; set; }
    }
}