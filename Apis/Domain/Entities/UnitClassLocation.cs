using Domain.Enums;

namespace Domain.Entities;

#pragma warning disable
public class UnitClassLocation : BaseEntity
{
    public string BuildingLocation { get; set; }
    public City City { get; set; }
    public ICollection<UnitClassDetail> UnitClassDetails { get; set; }
}
