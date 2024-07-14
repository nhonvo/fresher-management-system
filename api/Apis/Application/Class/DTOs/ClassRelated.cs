using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Class.DTOs;

#pragma warning disable 
public class ClassRelated
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int StartMinute { get; set; }
    public int EndMinute { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }
    public DateTime ReviewOn { get; set; }
    public DateTime ApproveOn { get; set; }
    public AttendeeType AttendeeType { get; set; }
    public int NumberAttendeePlanned { get; set; }
    public int NumberAttendeeAccepted { get; set; }
    public int NumberAttendeeActual { get; set; }
    public ClassLocation Location { get; set; }
    public ClassStatus Status { get; set; }
    // ref
    public UserDto? CreateBy { get; set; }
    public UserDto? ReviewBy { get; set; }
    public UserDto? ApproveBy { get; set; }
    public ICollection<CalenderDto> Calenders { get; set; }
    public ICollection<ClassAdminDto> ClassAdmins { get; set; }
    public ICollection<ClassTrainerDto> ClassTrainers { get; set; }
    public ICollection<ClassStudentDto> Students { get; set; }
    public TrainingProgramDto TrainingProgram { get; set; }
}

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public UserRoleType Role { get; set; }
}

public class CalenderDto
{
    public int TrainingClassId { get; set; }
    public DateTime Date { get; set; }
    public int Order { get; set; }
    public int Count { get; set; }
}

public class ClassAdminDto
{
    public int AdminId { get; set; }
    public UserDto Admin { get; set; }
}

public class ClassTrainerDto
{
    public int TrainerId { get; set; }
    public UserDto Trainer { get; set; }
}

public class ClassStudentDto
{
    public int StudentId { get; set; }
    public UserDto Student { get; set; }
}

public class TrainingProgramDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public DateTime LastModify { get; set; }
    public bool IsActive { get; set; } = true;
    public int DaysDuration { get; set; }
    public int TimeDuration { get; set; }
    public List<ProgramSyllabusDto> ProgramSyllabus { get; set; }
}

public class ProgramSyllabusDto
{
    public int TrainingProgramId { get; set; }
    public int SyllabusId { get; set; }
    public SyllabusDto Syllabus { get; set; }
}

public class SyllabusDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int AttendeeNumber { get; set; }
    public string CourseObjective { get; set; }
    public SyllabusLevel SyllabusLevel { get; set; }
    public float QuizScheme { get; set; }
    public float AssignmentScheme { get; set; }
    public float FinalScheme { get; set; }
    public float FinalTheoryScheme { get; set; }
    public float FinalPracticeScheme { get; set; }
    public float GPAScheme { get; set; }
    public int? CreatedBy { get; set; }
    public int? ModificationBy { get; set; }
    public DateTime? ModificationDate { get; set; }
    // ref
    public DateTime CreationDate { get; set; }
    public List<UnitDto> Units { get; set; }
}

public class UnitDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SyllabusSession { get; set; }
    public int UnitNumber { get; set; }
    public List<LessonDto> Lessons { get; set; }
}

public class LessonDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public LessonType LessonType { get; set; }
    public DeliveryType DeliveryType { get; set; }
    public List<TrainingMaterialDto> TrainingMaterials { get; set; }
}

public class TrainingMaterialDto
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public long FileSize { get; set; }
}
