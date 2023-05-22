using Application.Class.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Class.Queries.GetClassDetail
{
    public record GetClassDetailQuery(int id) : IRequest<ClassDetail>;

    public class GetClassDetailHandler : IRequestHandler<GetClassDetailQuery, ClassDetail>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClassDetailHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ClassDetail> Handle(GetClassDetailQuery request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWork.ClassRepository.FirstOrDefaultAsync(
                filter: x => x.Id == request.id,
                include: x => x
                    .Include(x => x.CreateBy)
                    .Include(x => x.ReviewBy)
                    .Include(x => x.ApproveBy)
                    .Include(x => x.TrainingProgram)
                        .ThenInclude(x => x.ProgramSyllabus)
                        .ThenInclude(x => x.Syllabus)
                        .ThenInclude(x => x.Units)
                        .ThenInclude(x => x.Lessons)
                        .ThenInclude(x => x.TrainingMaterials)
                    .Include(x => x.Calenders));

            var result = _mapper.Map<ClassDetail>(item);

            return result;
        }
    }
}