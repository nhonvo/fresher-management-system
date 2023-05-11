namespace Domain.Entities;

#pragma warning disable
public abstract class BaseEntity
{
    public int Id { get; set; }
    //public DateTime CreationDate { get; set; }
    //public int? CreatedBy { get; set; }
    //public User? CreateByUser { get; set; }
    //public DateTime? ModificationDate { get; set; }
    //public int? ModificationBy { get; set; }
    //public User? ModificationByUser { get; set; }
    public int? DeleteBy { get; set; }
    //public User DeleteByUser { get; set; }
    public DateTime? DeletionDate { get; set; }
    public bool IsDeleted { get; set; }

}
