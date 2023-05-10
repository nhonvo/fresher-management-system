using Domain.Enums;

namespace Domain.Entities
{
    public class TrainingProgram : BaseEntity
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public TrainingProgramStatus Status { get; set; }

        //Navigation properties
        public int? ParentId { get; set; }
        public TrainingProgram? Parent { get; set; }
        public int TrainingClassId { get; set; }
        public TrainingClass TrainingClass { get; set; }
        public ICollection<ProgramSyllabus> ProgramSyllabus { get; set; }
    }
}
