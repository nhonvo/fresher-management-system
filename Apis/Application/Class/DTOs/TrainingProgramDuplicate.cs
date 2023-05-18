using Application.Syllabuses.DTO;
using Application.TrainingPrograms.DTOs;
using Domain.Enums;

namespace Application.Class.DTO
{
    public class TrainingProgramDuplicate
    {
        public string Name { get; set; }
        public TrainingProgramStatus Status { get; set; }
        public ICollection<ProgramSyllabusDuplicate> ProgramSyllabus { get; set; }
    }
    public class ProgramSyllabusDuplicate
    {
        public int SyllabusId { get; set; }
        public SyllabusDuplicate Syllabus { get; set; }
        // public int TrainingProgramId { get; set; }
        public TrainingProgramDTO TrainingProgram { get; set; }
    }
}
