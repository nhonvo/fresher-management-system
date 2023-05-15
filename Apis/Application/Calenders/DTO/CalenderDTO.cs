namespace Application.Calenders.DTO;

public class CalenderDTO
{
    public int Id { get; init; }
    public int TrainingClassId { get; init; }
    public DateTime Date { get; init; }
    public int Order { get; init; }
    public int Count { get; init; }
    public int? DeleteBy { get; init; }
    public DateTime? DeletionDate { get; init; }
    public bool IsDeleted { get; init; }
}
