using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Unit : BaseEntity
    {
        public string Name { get; set; }
        public int SyllabusSession { get; set; }
        public int UnitNumber { get; set; }

        //Navigation Property
        public int SyllabusId { get; set; }
        public Syllabus Syllabus { get; set; }
        public ICollection<UnitLesson> UnitLessons { get; set; }
        public ICollection<UnitClassDetail> UnitClassDetails { get; set; }


    }
}
