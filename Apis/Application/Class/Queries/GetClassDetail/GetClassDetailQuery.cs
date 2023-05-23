using Application.Class.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Class.Queries.GetClassDetail
{
    public record GetClassDetailQuery(int id) : IRequest<ClassRelated>;

    public class GetClassDetailHandler : IRequestHandler<GetClassDetailQuery, ClassRelated>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClassDetailHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ClassRelated> Handle(GetClassDetailQuery request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWork.ClassRepository.FirstOrDefaultAsync(
                filter: x => x.Id == request.id,
                include: x => x
                    .Include(x => x.CreateBy)
                    .Include(x => x.ReviewBy)
                    .Include(x => x.ApproveBy)
                    .Include(x => x.ClassAdmins)
                        .ThenInclude(x => x.Admin)
                    .Include(x => x.ClassTrainers)
                        .ThenInclude(x => x.Trainer)
                    .Include(x => x.Students)
                        .ThenInclude(x => x.Student)
                    .Include(x => x.TrainingProgram)
                        .ThenInclude(x => x.ProgramSyllabus)
                        .ThenInclude(x => x.Syllabus)
                        .ThenInclude(x => x.Units)
                        .ThenInclude(x => x.Lessons)
                        .ThenInclude(x => x.TrainingMaterials)
                    .Include(x => x.Calenders));

            var result = _mapper.Map<ClassRelated>(item);

            return result;
        }
    }
}