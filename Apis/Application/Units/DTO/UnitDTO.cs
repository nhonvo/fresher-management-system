namespace Application.Units.DTO
{
    public class UnitDTO
    {
        public string Name { get; set; }
        public int SyllabusSession { get; set; }
        public int UnitNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }

    }
}
