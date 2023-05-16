using Application.Calenders.DTO;
using Application.Common.Exceptions;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Class.Commands.SetCalenders;

#pragma warning disable 
public record SetCalendersOfTrainingClassCommand : IRequest<List<CalenderDTO>>
{
    public int? TrainingClassId { get; set; }
    public List<CalenderCreateDTO> Calenders { get; init; }
}
#pragma warning restore

public class SetCalendersOfTrainingClassHandler : IRequestHandler<SetCalendersOfTrainingClassCommand, List<CalenderDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SetCalendersOfTrainingClassHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<CalenderDTO>> Handle(SetCalendersOfTrainingClassCommand request, CancellationToken cancellationToken)
    {
        int classId = request.TrainingClassId ?? throw new NotFoundException("Training class not found");
        var trainingClass = await _unitOfWork.ClassRepository.GetByIdAsync(classId);
        if (trainingClass is null)
            throw new NotFoundException("Can not found class!!");
        var calenders = _mapper.Map<List<Calender>>(request.Calenders);
        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            var calendersToDelete = (await _unitOfWork.CalenderRepository.GetAsync(
                filter: x => x.TrainingClassId == classId,
                pageIndex: 0,
                pageSize: int.MaxValue)).Items;
            _unitOfWork.CalenderRepository.DeleteRange(calendersToDelete);
            _unitOfWork.SaveChanges();
            var x = (await _unitOfWork.CalenderRepository.GetAsync(
                filter: x => x.TrainingClassId == classId,
                pageIndex: 0,
                pageSize: int.MaxValue)).Items;
            trainingClass.Calenders = calenders;
            _unitOfWork.ClassRepository.Update(trainingClass);
        });
        var result = _mapper.Map<List<CalenderDTO>>(calenders);
        return result;
    }
}
