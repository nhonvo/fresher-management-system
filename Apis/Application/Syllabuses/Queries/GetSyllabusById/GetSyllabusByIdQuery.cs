using Application.Common.Exceptions;
using Application.Syllabuses.DTO;
using AutoMapper;
using MediatR;


namespace Application.Syllabuses.Queries.GetSyllabusById
{

    public record GetSyllabusByIdQuery(int id) : IRequest<SyllabusDTO>;

    public class GetSyllabusByIdHandler : IRequestHandler<GetSyllabusByIdQuery, SyllabusDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSyllabusByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<SyllabusDTO> Handle(GetSyllabusByIdQuery request, CancellationToken cancellationToken)
        {
            var isExist = await _unitOfWork.SyllabusRepository.AnyAsync(x => x.Id == request.id);
            if (isExist is false)
            {
                throw new NotFoundException("Syllabus is not exist");
            }
            var syllabus = await _unitOfWork.SyllabusRepository.GetByIdAsync(request.id);
            var result = _mapper.Map<SyllabusDTO>(syllabus);
            if (syllabus.Units is not null)
            {
                result.Duration = syllabus.Units.Sum(x => x.UnitLessons.Sum(ul => ul.Duration));
            }
            return result;
        }
    }
}