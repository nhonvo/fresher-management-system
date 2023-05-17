using Application.Class.DTO;
using Application.Class.DTOs;
using Application.Commons;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Class.Queries.GetClassDetail
{
    public record GetClassDetailQuery(int id) : IRequest<ClassDTO>;

    public class GetClassDetailHandler : IRequestHandler<GetClassDetailQuery, ClassDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClassDetailHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ClassDTO> Handle(GetClassDetailQuery request, CancellationToken cancellationToken)
        {
            var syllabus = await _unitOfWork.ClassRepository.FirstOrdDefaultAsync(
                filter: x => x.Id == request.id,
                include: x => x.Include(x => x.TrainingProgram)
                               .ThenInclude(x => x.ProgramSyllabus)
                               .ThenInclude(x => x.Syllabus)
                               .ThenInclude(x => x.Units)
                               .ThenInclude(x => x.UnitLessons)
                               .ThenInclude(x => x.TrainingMaterials));

            var result = _mapper.Map<ClassDTO>(syllabus);

            return result;
        }
    }
}
// TODO: Get all class details

