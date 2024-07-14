using Application.Common.Exceptions;
using Application.Syllabuses.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Syllabuses.Queries.GetSyllabusDetailById
{

    public record GetSyllabusDetailByIdQuery(int id) : IRequest<SyllabusRelated>;

    public class GetSyllabusDetailByIdHandler : IRequestHandler<GetSyllabusDetailByIdQuery, SyllabusRelated>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSyllabusDetailByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<SyllabusRelated> Handle(GetSyllabusDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var isExist = await _unitOfWork.SyllabusRepository.AnyAsync(x => x.Id == request.id);
            if (isExist is false)
            {
                throw new NotFoundException("Syllabus is not exist");
            }
            var syllabus = await _unitOfWork.SyllabusRepository.FirstOrDefaultAsync(
                filter: x => x.Id == request.id,
                   include: x => x.Include(x => x.CreateByUser)
                               .Include(x => x.ModificationByUser)
                               .Include(x => x.Units)
                               .ThenInclude(x => x.Lessons)
                               .ThenInclude(x => x.TrainingMaterials));
            var result = _mapper.Map<SyllabusRelated>(syllabus);
            return result;
        }
    }
}