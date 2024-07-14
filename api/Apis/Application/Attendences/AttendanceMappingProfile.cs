using Application.Attendances.Commands.CreateAttendances;
using Application.Attendances.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Attendances
{
    public class AttendanceMappingProfile : Profile
    {
        public AttendanceMappingProfile()
        {
            CreateMap<Attendance, AttendanceDTO>().ReverseMap();
            CreateMap<Attendance, AttendanceRelatedDTO>().ReverseMap();
            CreateMap<Attendance, CreateAttendancesCommand>().ReverseMap();
            // related
            CreateMap<Attendance, AttendanceRelatedFilterDTO>().ReverseMap();
            CreateMap<AttendanceRelatedClassStudentFilterDTO, ClassStudent>().ReverseMap();
            
            CreateMap<AttendanceRelatedClassStudentDTO, ClassStudent>().ReverseMap();
            CreateMap<AttendanceRelatedTrainingClassDTO, TrainingClass>().ReverseMap();
            CreateMap<AttendanceRelatedStudent, User>().ReverseMap();
            CreateMap<AttendanceRelatedStudent, User>().ReverseMap();
            CreateMap<AttendanceRelatedClassTrainers, ClassTrainer>().ReverseMap();
            CreateMap<AttendanceRelatedUserAdmin, ClassAdmin>().ReverseMap();
            CreateMap<AttendanceRelatedClassTrainerUser, User>().ReverseMap();
            CreateMap<AttendanceRelatedClassAdminUser, User>().ReverseMap();
        }
    }
}
