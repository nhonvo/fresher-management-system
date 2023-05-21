using Domain.Enums;

namespace Application.TrainingPrograms.DTOs
{
    public class TrainingProgramDTO
    {
        public string Name { get; set; }
        public TrainingProgramStatus Status { get; set; }
        //Navigation properties
        public int? ParentId { get; set; }
        public int? TrainingClassId { get; set; }
        public DateTime CreationDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? ModificationBy { get; set; }
        public int? DeleteBy { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}