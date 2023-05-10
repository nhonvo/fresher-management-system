using Domain.Entities;
using MediatR;
using Application.Class.DTO;
using AutoMapper;
using Application.Common.Exceptions;
using Domain.Enums;

namespace Application.Class.Commands.UpdateClass
{
    public record UpdateClassCommand : IRequest<ClassDTO>
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
        public DateTime ClassTimeStart { get; set; }
        public DateTime ClassTimeEnd { get; set; }
        public DateTime ReviewOn { get; set; }
        public DateTime ApproveOn { get; set; }
        public int NumberAttendeePlanned { get; set; }
        public int NumberAttendeeAccepted { get; set; }
        public int NumberAttendeeActual { get; set; }
        public ClassLocation ClassLocation { get; set; }
        public ClassStatus Status { get; set; }
    }
    public class UpdateClassHandler : IRequestHandler<UpdateClassCommand, ClassDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateClassHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ClassDTO> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
        {
            var classes = await _unitOfWork.ClassRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (classes == null)
                throw new NotFoundException("Class not found");
            classes = _mapper.Map<TrainingClass>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ClassRepository.Update(classes);
            });
            var result = _mapper.Map<ClassDTO>(classes);
            return result ?? throw new NotFoundException("Can not update class");
        }
    }
}
