using Application.Common.Exceptions;
using Application.ViewModels.Syllabus;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.AllocateSyllabus.GetSyllabusAllocate
{
    public record GetSyllabusTimeAllocatePercentage(int id) : IRequest<SyllabusViewDTO>;

    public class GetSyllabusTimeAllocateHandler : IRequestHandler<GetSyllabusTimeAllocatePercentage, SyllabusViewDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSyllabusTimeAllocateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SyllabusViewDTO> Handle(GetSyllabusTimeAllocatePercentage request, CancellationToken cancellationToken)
        {
            var syllabus = await _unitOfWork.SyllabusRepository.GetSyllabusRelationUnitAsync(request.id);
            if (syllabus is null)
            {
                throw new NotFoundException("Syllabus not found");
            }
            var result = _mapper.Map<SyllabusViewDTO>(syllabus);
            //result.OutputStandards = result.Units.SelectMany(u => u.Lessons.Select(ul => ul.OutputStandard))
            //                                     .DistinctBy(x => x.Id)
            //                                     .OrderBy(x => x.Id)
            //                                     .ToList();

            //result.Duration = result.Units.SelectMany(u => u.Lessons).Sum(ul => ul.Duration);
            
            //if (time == 1)
            //{
            //    result.Duration /= 60;
            //    result.TimeType = TimeType.hour;
            //}

            result.Allocate = result.Units.SelectMany(u => u.Lessons)
                                          .GroupBy(ul => ul.DeliveryType)
                                          .Select(x => new Allocate
                                          {
                                              DeliveryType = x.Key,
                                              Count = x.Count(),
                                              Duration = x.Select(l => l.Duration).FirstOrDefault(),
                                              Time = x.Sum(x => x.Duration)
                                          })
                                          .ToList();
            foreach (var item in result.Allocate)
            {
                item.Percent = (int)Math.Round((decimal)(item.Time * 100) / (decimal)result.Duration);
            }
            var allValues = Enum.GetValues(typeof(DeliveryType)).Cast<DeliveryType>().ToList();
            var listDev = result.Allocate.Select(a => a.DeliveryType);
            //var str = string.Join("", Enum.GetValues(typeof(DeliveryType)));
            var listenum = allValues.Except(listDev);
            foreach (var item in listenum)
            {
                result.Allocate.Add(new Allocate()
                {
                    DeliveryType = item,
                    Percent = 0,
                    Time = 0,
                });
            }
            return result ?? throw new NotFoundException();
        }
    }
}
