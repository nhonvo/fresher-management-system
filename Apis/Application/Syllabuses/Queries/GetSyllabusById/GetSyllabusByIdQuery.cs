using Application.Common.Exceptions;
using Application.Syllabuses.DTOs;
using Application.Syllabuses.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Syllabuses.Queries.GetSyllabusById
{

    public record GetSyllabusByIdQuery(int id) : IRequest<SyllabusRelated>;

    public class GetSyllabusByIdHandler : IRequestHandler<GetSyllabusByIdQuery, SyllabusRelated>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSyllabusByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<SyllabusRelated> Handle(GetSyllabusByIdQuery request, CancellationToken cancellationToken)
        {
            var syllabus = await _unitOfWork.SyllabusRepository.FirstOrDefaultAsync(
               filter: x => x.Id == request.id);
            if (syllabus is null)
            {
                throw new NotFoundException("Syllabus is not exist");
            }
            var result = _mapper.Map<SyllabusRelated>(syllabus);
            return result;
        }
    }
}