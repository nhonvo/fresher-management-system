using Domain.Enums;

namespace Domain.Entities;

#pragma warning disable
public class User : BaseEntity
{
    public string Email { get; set; }
    public Gender Gender { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public UserRoleType Role { get; set; }
    public UserStatus Status { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<ClassAdmin> CreatedClassAdmin { get; set; }
    public ICollection<ClassAdmin> ClassAdmins { get; set; }
    public ICollection<ClassStudent> CreatedClassStudents { get; set; }
    public ICollection<ClassStudent> ClassStudents { get; set; }
    public ICollection<Syllabus> CreatedSyllabuses { get; set; }
    public ICollection<Syllabus> ModifiedSyllabuses { get; set; }
    public ICollection<TestAssessment> TestAssessments { get; set; }
    public ICollection<TrainingClass> CreatedTrainingClasses { get; set; }
    public ICollection<TrainingClass> ReviewTrainingClasses { get; set; }
    public ICollection<TrainingClass> ApprovedTrainingClasses { get; set; }
    public ICollection<TrainingMaterial> ModifiedTrainingMaterial { get; set; }
    public ICollection<TrainingProgram> CreatedTrainingPrograms { get; set; }
    public ICollection<TrainingProgram> ModifiedTrainingPrograms { get; set; }
    public ICollection<Unit> CreatedUnits { get; set; }
    public ICollection<Unit> ModifiedUnits { get; set; }
    public ICollection<UnitClassDetail> UnitTrainers { get; set; }
    public ICollection<ReportAttendence> ReportAttendence { get; set; }
}
