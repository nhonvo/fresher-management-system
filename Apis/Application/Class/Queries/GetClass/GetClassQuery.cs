using Application.Class.DTO;
using Application.Class.DTOs;
using Application.Commons;
using AutoMapper;
using MediatR;

namespace Application.Class.Queries.GetClass
{
    public record GetClassQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<ClassRelated>>;

    public class GetClassHandler : IRequestHandler<GetClassQuery, Pagination<ClassRelated>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClassHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<ClassRelated>> Handle(GetClassQuery request, CancellationToken cancellationToken)
        {
            var syllabus = await _unitOfWork.ClassRepository.GetAsync(pageIndex: request.PageIndex, pageSize: request.PageSize);

            var result = _mapper.Map<Pagination<ClassRelated>>(syllabus);

            return result;
        }
    }
}


