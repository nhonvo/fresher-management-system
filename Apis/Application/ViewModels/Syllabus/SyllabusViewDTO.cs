using Application.ViewModels.OutputStandards;
using Application.ViewModels.Units;
using Domain.Entities;
using Domain.Enums;

namespace Application.ViewModels.Syllabus
{
    public class SyllabusViewDTO
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedByUserId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Duration { get; set; }
        public TimeType TimeType { get; set; }
        public string TrainingDeliveryPrinciple { get; set; }
        public SyllabusLevel SyllabusLevel { get; set; }
        public int AttendeeNumber { get; set; }
        public string TechnicalRequirement { get; set; }
        public string CourseObjective { get; set; }
        public float Version { get; set; }
        public int QuizSchema { get; set; }
        public int AsignmentSchema { get; set; }
        public int FinalSchema { get; set; }
        public int FinalTheorySchema { get; set; }
        public int FinalPraticeSchema { get; set; }
        public int GPASchema { get; set; }
        public List<UnitViewDTO> Units { get; set; }
        public List<Allocate> Allocate { get; set; }
        public List<OutputStandardViewDTO> OutputStandards { get; set; }
    }
}
