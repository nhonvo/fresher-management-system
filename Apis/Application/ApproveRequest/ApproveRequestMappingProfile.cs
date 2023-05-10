using Application.Account.DTOs;
using Application.ApproveRequests.Commands.CreateRequest;
using Application.ApproveRequests.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.ApproveRequests;

public class ApproveRequestMappingProfile : Profile
{
    public ApproveRequestMappingProfile()
    {
        CreateMap<ApproveRequestDTO, ApproveRequest>().ReverseMap();
        CreateMap<CreateRequestCommand, ApproveRequest>().ReverseMap();
    }
}
