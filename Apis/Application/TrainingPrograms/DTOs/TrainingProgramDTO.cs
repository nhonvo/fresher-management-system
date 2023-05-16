using Domain.Enums;

namespace Application.TrainingPrograms.DTOs
{
    public class TrainingProgramDTO
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public TrainingProgramStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? ModificationBy { get; set; }
    }
}