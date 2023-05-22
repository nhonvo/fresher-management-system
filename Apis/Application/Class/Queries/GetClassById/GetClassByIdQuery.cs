using Application.Class.DTOs;
using Application.Common.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Class.Queries.GetClassById;

public record GetClassByIdQuery(int id) : IRequest<ClassRelated>;

public class GetClassByIdHandler : IRequestHandler<GetClassByIdQuery, ClassRelated>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetClassByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ClassRelated> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _unitOfWork.ClassRepository.FirstOrDefaultAsync(
            filter: x => x.Id == request.id);

        var result = _mapper.Map<ClassRelated>(item);

        return result ?? throw new NotFoundException("Could not find the class");
    }
}
