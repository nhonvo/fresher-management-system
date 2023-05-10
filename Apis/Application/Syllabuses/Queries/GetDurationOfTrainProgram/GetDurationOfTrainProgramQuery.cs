using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Syllabuses.Queries.GetDurationOfTrainProgram;

public record GetDurationOfTrainProgramQuery(string id) : IRequest<FileStreamResult>;

public class GetDurationOfTrainProgramHandler : IRequestHandler<GetDurationOfTrainProgramQuery, FileStreamResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDurationOfTrainProgramHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<FileStreamResult> Handle(GetDurationOfTrainProgramQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}