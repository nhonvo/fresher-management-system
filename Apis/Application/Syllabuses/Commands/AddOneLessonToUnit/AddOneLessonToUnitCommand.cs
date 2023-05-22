using Application.Interfaces;
using Application.Syllabuses.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System.Transactions;

namespace Application.Syllabuses.Commands.AddOneLessonToUnit
{
    public record AddOneLessonToUnitCommand : IRequest<AddOneLessonToUnitResponse>
    {
        public string Name { get; init; }
        public int Duration { get; init; }
        public LessonType LessonType { get; init; }
        public DeliveryType DeliveryType { get; init; }
        public int UnitId { get; init; }
    }

    public class AddOneLessonToUnitHandler : IRequestHandler<AddOneLessonToUnitCommand, AddOneLessonToUnitResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public AddOneLessonToUnitHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentTime = currentTime;
            _claimService = claimService;
        }

        public async Task<AddOneLessonToUnitResponse> Handle(AddOneLessonToUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = await _unitOfWork.UnitRepository.AnyAsync(x => x.Id == request.UnitId);
            if (unit is false)
                throw new TransactionException("Unit not exist");
            var lesson = _mapper.Map<Lesson>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.LessonRepository.AddAsync(lesson);
            });
            var result = _mapper.Map<AddOneLessonToUnitResponse>(lesson);
            return result;
        }
    }
}