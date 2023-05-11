using Application.FeedBacks.Commands.CreateFeedBack;
using Application.FeedBacks.Commands.UpdateFeedBack;
using Application.FeedBacks.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.FeedBacks
{
    public class FeedBacksMappingProfile : Profile
    {
        public FeedBacksMappingProfile()
        {
            CreateMap<FeedBack, FeedBackDTO>().ReverseMap();
            CreateMap<FeedBack, CreateFeedBackCommand>().ReverseMap();
            CreateMap<FeedBack, UpdateFeedBackCommand>().ReverseMap();
        }
    }
}
