using Application.Syllabuses.DTO;
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
    public class TrainingProgramDuplicate
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public TrainingProgramStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int? ModificationBy { get; set; }
        public ICollection<ProgramSyllabusDuplicate> ProgramSyllabus { get; set; }
    }
    public class ProgramSyllabusDuplicate
    {
        public int SyllabusId { get; set; }
        public SyllabusDuplicate Syllabus { get; set; }
        public int TrainingProgramId { get; set; }
        public TrainingProgramDTO TrainingProgram { get; set; }
    }
}