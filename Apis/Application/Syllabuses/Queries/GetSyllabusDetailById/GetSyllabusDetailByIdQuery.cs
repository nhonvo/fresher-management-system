using Application.Common.Exceptions;
using Application.Syllabuses.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Syllabuses.Queries.GetSyllabusDetailById
{

    public record GetSyllabusDetailByIdQuery(int id) : IRequest<SyllabusDTO>;

    public class GetSyllabusDetailByIdHandler : IRequestHandler<GetSyllabusDetailByIdQuery, SyllabusDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSyllabusDetailByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<SyllabusDTO> Handle(GetSyllabusDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var isExist = await _unitOfWork.SyllabusRepository.AnyAsync(x => x.Id == request.id);
            if (isExist is false)
            {
                throw new NotFoundException("Syllabus is not exist");
            }
            var syllabus = await _unitOfWork.SyllabusRepository.FirstOrdDefaultAsync(
                filter: x => x.Id == request.id,
                include: x => x.Include(x => x.Units)
                               .ThenInclude(x => x.Lessons)
                               .ThenInclude(x => x.TrainingMaterials));
            var result = _mapper.Map<SyllabusDTO>(syllabus);
            return result;
        }
    }
}