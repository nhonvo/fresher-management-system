using Application.ViewModels;
using AutoMapper;
using MediatR;

namespace Application.Class.Queries.GetClassDuration;

public record GetClassDurationQuery(int id) : IRequest<ClassDuration>;

public class GetClassDurationQueryHandler : IRequestHandler<GetClassDurationQuery, ClassDuration>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetClassDurationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ClassDuration> Handle(GetClassDurationQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.ClassRepository.GetClassDurationAsync(request.id);
        return result;
    }
}
