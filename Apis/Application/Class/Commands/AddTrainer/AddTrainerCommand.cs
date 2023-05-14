using Application.Class.DTO;
using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Class.Commands.AddTrainer
{
    public record AddTrainerCommand : IRequest<TrainerClassDTO>
    {
        public int TrainingClassId { get; set; }
        public int TrainerId { get; set; }
    }
    public class AddTrainerHandler : IRequestHandler<AddTrainerCommand, TrainerClassDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddTrainerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TrainerClassDTO> Handle(AddTrainerCommand request, CancellationToken cancellationToken)
        {
            var trainerExist = await CheckTrainerExitsAsync(request.TrainingClassId);
            if (trainerExist)
                throw new NotFoundException("Can not found trainer!!");

            var classExist = await CheckClassExitsAsync(request.TrainerId);
            if (classExist)
                throw new NotFoundException("Can not found class!!");
            var trainerClass = _mapper.Map<ClassTrainer>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ClassTrainerRepository.AddAsync(trainerClass);
            });
            var result = _mapper.Map<TrainerClassDTO>(request);
            return result;
        }
        private async Task<bool> CheckTrainerExitsAsync(int id)
           => await _unitOfWork.UserRepository.GetByIdAsyncAsNoTracking(id) == null ? true : false;
        private async Task<bool> CheckClassExitsAsync(int id)
            => await _unitOfWork.ClassRepository.GetByIdAsyncAsNoTracking(id) == null ? true : false;
    }
}
