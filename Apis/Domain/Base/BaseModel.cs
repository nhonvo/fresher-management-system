namespace Domain.Entities.Common
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public bool isDeleted { get; set; } = false;
    }
}