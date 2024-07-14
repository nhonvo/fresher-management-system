using Application.Interfaces;
using Application.Syllabuses.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Transactions;

namespace Application.Syllabuses.Commands.AddOneMaterialToLesson
{

    public record AddOneMaterialToLessonCommand : IRequest<AddOneMaterialToLessonResponse>
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public int? UnitLessonId { get; set; }
    }

    public class AddOneMaterialToLessonHandler : IRequestHandler<AddOneMaterialToLessonCommand, AddOneMaterialToLessonResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public AddOneMaterialToLessonHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentTime = currentTime;
            _claimService = claimService;
        }

        public async Task<AddOneMaterialToLessonResponse> Handle(AddOneMaterialToLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = await _unitOfWork.LessonRepository.AnyAsync(x => x.Id == request.UnitLessonId);
            if (lesson is false)
                throw new TransactionException("Syllabus not exist");
            var trainingMaterial = _mapper.Map<TrainingMaterial>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.TrainingMaterialRepository.AddAsync(trainingMaterial);
            });
            var result = _mapper.Map<AddOneMaterialToLessonResponse>(trainingMaterial);
            return result;
        }
    }
}