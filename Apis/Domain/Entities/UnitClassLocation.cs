using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UnitClassLocation : BaseEntity
    {
        public string BuildingLocation { get; set; }
        public City City { get; set; }
        public ICollection<UnitClassDetail> UnitClassDetails { get; set; }
    }
}
