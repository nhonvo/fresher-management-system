namespace Domain.Entities;

#pragma warning disable
public abstract class BaseEntity
{
    public int Id { get; set; }
  
    public int? DeleteBy { get; set; }
    // public User DeleteByUser { get; set; }
    public DateTime? DeletionDate { get; set; }
    public bool? IsDeleted { get; set; }

}
