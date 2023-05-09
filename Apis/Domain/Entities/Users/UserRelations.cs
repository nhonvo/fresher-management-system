#nullable disable warnings

using Domain.Entities.Syllabuses;

namespace Domain.Entities.Users
{
    public partial class User
    {
        // public Role Role { get; set; }   
        public ICollection<Score> Scores { get; set; }
        public ICollection<TMS> TimeMngSystem { get; set; }
        public ICollection<TrainingProgram> ModifyTrainingProgram { get; set; }
        public ICollection<TrainingProgram> CreatedTrainingProgram { get; set; }
        public ICollection<Syllabus> CreatedSyllabus { get; set; }
        public ICollection<Syllabus> ModifiedSyllabus { get; set; }
        public ICollection<TrainingClass> ApprovedClass { get; set; }
        public ICollection<TrainingClass> CreatedClass { get; set; }
        public ICollection<FeedbackForm> FeedbackTrainee { get; set; }
        public ICollection<FeedbackForm> FeedbackTrainer { get; set; }
        public ICollection<GradeReport> GradeReports { get; set; }
        public ICollection<ClassUsers> ClassUsers { get; set; }
        public ICollection<TMS> TimeMngSystemList { get; set; }
        public ICollection<TrainingMaterial> TrainingMaterials { get; set; }
        public ICollection<ClassUnitDetail> ClassUnitDetails { get; set; }
    }
}