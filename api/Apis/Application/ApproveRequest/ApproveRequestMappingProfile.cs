using Application.ApproveRequests.Commands.CreateRequest;
using Application.ApproveRequests.Commands.CreateRequestCurrentUser;
using Application.ApproveRequests.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.ApproveRequests;

public partial class ApproveRequestMappingProfile : Profile
{
    public ApproveRequestMappingProfile()
    {
        CreateMap<ApproveRequestRelatedDTO, ApproveRequest>().ReverseMap();
        CreateMap<CreateRequestCommand, ApproveRequest>().ReverseMap();
        CreateMap<ApproveRequestTrainingClass, TrainingClass>().ReverseMap();
        CreateMap<CreateRequestCurrentUserCommand, ApproveRequest>().ReverseMap();
    }

}
