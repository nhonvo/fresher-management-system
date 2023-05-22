using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Class.Queries.GetClassById;

#pragma warning disable 
public class ClassDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Code { get; init; }
    public DateTime TimeStart { get; init; }
    public DateTime TimeEnd { get; init; }
    public DateTime ReviewOn { get; init; }
    public DateTime ApproveOn { get; init; }
    public AttendeeType AttendeeType { get; init; }
    public int NumberAttendeePlanned { get; init; }
    public int NumberAttendeeAccepted { get; init; }
    public int NumberAttendeeActual { get; init; }
    public ClassLocation Location { get; init; }
    public ClassStatus Status { get; init; }
    // ref
    public UserDto? CreateBy { get; init; }
    public UserDto? ReviewBy { get; init; }
    public UserDto? ApproveBy { get; init; }
    public ICollection<CalenderDto> Calenders { get; init; }
    public TrainingProgramDto TrainingProgram { get; init; }
}

public class UserDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public UserRoleType Role { get; init; }
}

public class CalenderDto
{
    public int TrainingClassId { get; init; }
    public DateTime Date { get; init; }
    public int Order { get; init; }
    public int Count { get; init; }
}

public class TrainingProgramDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int Duration { get; init; }
    public DateTime LastModify { get; init; }
    public bool IsActive { get; init; } = true;
    public int DaysDuration { get; init; }
    public int TimeDuration { get; init; }
    public List<ProgramSyllabusDto> ProgramSyllabus { get; init; }
}

public class ProgramSyllabusDto
{
    public int TrainingProgramId { get; init; }
    public int SyllabusId { get; init; }
    public SyllabusDto Syllabus { get; init; }
}

public class SyllabusDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Code { get; init; }
    public int AttendeeNumber { get; init; }
    public string CourseObjective { get; init; }
    public SyllabusLevel SyllabusLevel { get; init; }
    public float QuizScheme { get; init; }
    public float AssignmentScheme { get; init; }
    public float FinalScheme { get; init; }
    public float FinalTheoryScheme { get; init; }
    public float FinalPracticeScheme { get; init; }
    public float GPAScheme { get; init; }
    public int? CreatedBy { get; init; }
    public int? ModificationBy { get; init; }
    public DateTime? ModificationDate { get; init; }
    // ref
    public DateTime CreationDate { get; init; }
    public List<UnitDto> Units { get; init; }
}

public class UnitDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int SyllabusSession { get; init; }
    public int UnitNumber { get; init; }
    public List<LessonDto> UnitLessons { get; init; }
}

public class LessonDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int Duration { get; init; }
    public LessonType LessonType { get; init; }
    public DeliveryType DeliveryType { get; init; }
    public List<TrainingMaterialDto> TrainingMaterials { get; init; }
}

public class TrainingMaterialDto
{
    public int Id { get; init; }
    public string FileName { get; init; }
    public string FilePath { get; init; }
    public long FileSize { get; init; }
}

public class ClassMappingProfile : Profile
{
    public ClassMappingProfile()
    {
        CreateMap<TrainingClass, ClassDto>();
        CreateMap<User, UserDto>();
        CreateMap<Calender, CalenderDto>();
        CreateMap<TrainingProgram, TrainingProgramDto>();
        CreateMap<ProgramSyllabus, ProgramSyllabusDto>();
        CreateMap<Syllabus, SyllabusDto>();
        CreateMap<Unit, UnitDto>();
        CreateMap<Lesson, LessonDto>();
        CreateMap<TrainingMaterial, TrainingMaterialDto>();
    }
}
