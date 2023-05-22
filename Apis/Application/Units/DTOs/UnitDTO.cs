namespace Application.Units.DTOs
{
    public class UnitDTO
    {
        public string Name { get; set; }
        public int SyllabusSession { get; set; }
        public int UnitNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int SyllabusId { get; set; }
    }
}
