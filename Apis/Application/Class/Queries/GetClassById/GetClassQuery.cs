using Application.Class.DTO;
using Application.Common.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Class.Queries.GetClass
{
    public record GetClassByIdQuery(int id) : IRequest<ClassDTO>;

    public class GetClassByIdHandler : IRequestHandler<GetClassByIdQuery, ClassDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClassByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ClassDTO> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
        {
            var syllabus = await _unitOfWork.ClassRepository.GetByIdAsync(request.id);

            var result = _mapper.Map<ClassDTO>(syllabus);

            return result ?? throw new NotFoundException("Class not found");
        }
    }
}


