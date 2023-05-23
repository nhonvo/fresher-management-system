using Application.Commons;
using Application.Syllabuses.DTOs;
using AutoMapper;
using MediatR;

namespace Application.TrainingPrograms.Queries.GetListSyllabusesNotExistInTrainingProgram;

public record GetListSyllabusesNotExistInTrainingProgramQuery(int TrainingProgramId, int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<SyllabusDTO>>;

public class GetListSyllabusesNotExistInTrainingProgramHandler : IRequestHandler<GetListSyllabusesNotExistInTrainingProgramQuery, Pagination<SyllabusDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetListSyllabusesNotExistInTrainingProgramHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<SyllabusDTO>> Handle(GetListSyllabusesNotExistInTrainingProgramQuery request, CancellationToken cancellationToken)
    {
        var syllabuses = await _unitOfWork.SyllabusRepository.GetAsync(
           filter: s => !s.ProgramSyllabus.Any(x => x.TrainingProgramId == request.TrainingProgramId),
           pageIndex: request.PageIndex,
           pageSize: request.PageSize
       );

        var result = _mapper.Map<Pagination<SyllabusDTO>>(syllabuses);
        return result;
    }
}
