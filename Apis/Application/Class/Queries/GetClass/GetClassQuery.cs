using Application.Class.DTO;
using Application.Commons;
using AutoMapper;
using MediatR;

namespace Application.Class.Queries.GetClass
{
    public record GetClassQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<ClassDTO>>;

    public class GetClassHandler : IRequestHandler<GetClassQuery, Pagination<ClassDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClassHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<ClassDTO>> Handle(GetClassQuery request, CancellationToken cancellationToken)
        {
            var syllabus = await _unitOfWork.ClassRepository.ToPagination(request.PageIndex, request.PageSize);

            var result = _mapper.Map<Pagination<ClassDTO>>(syllabus);

            return result;
        }
    }
}


