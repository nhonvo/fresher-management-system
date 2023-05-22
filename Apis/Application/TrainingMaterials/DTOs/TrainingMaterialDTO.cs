namespace Application.TrainingMaterials.DTOs;

#pragma warning disable 
public class TrainingMaterialDTO
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public long FileSize { get; set; }
    public int SyllabusId { get; set; }
    public int TrainingClassId { get; set; }
    public int? DeleteBy { get; set; }
    public DateTime? DeletionDate { get; set; }
    public bool IsDeleted { get; set; }
    // ref
    // public User DeleteByUser { get; set; }
}
