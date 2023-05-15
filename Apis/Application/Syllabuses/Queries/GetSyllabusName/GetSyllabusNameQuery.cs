using Application.Commons;
using Application.Syllabuses.DTO;
using AutoMapper;
using Domain.Aggregate.AppResult;
using MediatR;
namespace Application.Syllabuses.Queries.GetSyllabusName
{
    public record GetSyllabusNameQuery(string? name, int pageIndex = 0, int pageSize = 10) : IRequest<Pagination<SyllabusDTO>>;
    public class GetSyllabusNameHandler : IRequestHandler<GetSyllabusNameQuery, Pagination<SyllabusDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSyllabusNameHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<SyllabusDTO>> Handle(GetSyllabusNameQuery request, CancellationToken cancellationToken)
        {
            var syllabus = await _unitOfWork.SyllabusRepository.GetAsync(
                filter: x => x.Name == request.name,
                // && x.IsActive == true,
                pageIndex: request.pageIndex,
                pageSize: request.pageSize);

            var result = _mapper.Map<Pagination<SyllabusDTO>>(syllabus);
           
            return result;
        }
    }
}