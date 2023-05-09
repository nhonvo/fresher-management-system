using Domain.Entities.Users;
using Domain.Entities;
using Domain.Enums.ClassEnums;
using MediatR;
using Application.Class.DTO;
using AutoMapper;
using Application.Common.Exceptions;

namespace Application.Class.Commands.UpdateClass
{
    public record UpdateClassCommand : IRequest<ClassDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ClassLocation? Location { get; set; }
        public ClassAttendeeType? AttendeeType { get; set; }
        public ClassFSU? FSU { get; set; }
        public ClassTime ClassTime { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime FinishedOn { get; set; }
        public ClassStatus Status { get; set; }
        public DateTime ApprovedOn { get; set; }
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
