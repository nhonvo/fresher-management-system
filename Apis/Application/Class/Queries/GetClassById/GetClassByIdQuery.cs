using Application.Class.DTOs;
using Application.Common.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Class.Queries.GetClassById;

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
        var item = await _unitOfWork.ClassRepository.FirstOrDefaultAsync(
            filter: x => x.Id == request.id);

        var result = _mapper.Map<ClassDTO>(item);

        return result ?? throw new NotFoundException("Could not find the class");
    }
}
