namespace Application.Units.DTOs
{
    public class UnitHasIdDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SyllabusSession { get; set; }
        public int UnitNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int SyllabusId { get; set; }
    }
}
