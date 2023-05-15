using Application.Class.DTO;
using Application.Commons;
using AutoMapper;
using Domain.Aggregate.AppResult;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Class.Queries.GetClassProgram
{
    public record GetClassProgramQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<ClassProgram>>;

    public class GetClassProgramHandler : IRequestHandler<GetClassProgramQuery, Pagination<ClassProgram>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClassProgramHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<ClassProgram>> Handle(GetClassProgramQuery request, CancellationToken cancellationToken)
        {
            var classes = await _unitOfWork.ClassRepository.GetAsync(
                filter: x => x.Id != null,
                include: x => x.Include(x => x.TrainingProgram),
                pageIndex: request.PageIndex,
                pageSize: request.PageSize);

            var result = _mapper.Map<Pagination<ClassProgram>>(classes);

            return result;
        }
    }
}


