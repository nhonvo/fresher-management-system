using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Syllabus : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int AttendeeNumber { get; set; }
        public string TechnicalRequrement { get; set; }
        public string CourseObjective { get; set; }
        public string TrainingDeliveryPrinciple { get; set; }

        public SyllabusLevel SyllabusLevel { get; set; }
        public string Version { get; set; }

        public float QuizScheme { get; set; }
        public float AsignmentScheme { get; set; }
        public float FinalScheme { get; set; }
        public float FinalTheoryScheme { get; set; }
        public float FinalPraticeScheme { get; set; }
        public float GPAScheme { get; set; }

        // Navigation Property
 
        public ICollection<Unit> Units { get; set; }
        public ICollection<ProgramSyllabus> ProgramSyllabus { get; set; }
        public ICollection<TestAssessment> TestAssessments { get; set; }

    }
}
