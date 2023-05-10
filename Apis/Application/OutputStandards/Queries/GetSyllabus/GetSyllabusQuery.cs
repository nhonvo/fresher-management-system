// using Application.Commons;
// using Application.OutputStandards.DTO;
// using AutoMapper;
// using Domain.Aggregate.AppResult;
// using MediatR;


// namespace Application.OutputStandardes.Queries.GetOutputStandard;

// public record GetOutputStandardQuery(
//     int PageIndex = 0,
//     int PageSize = 10) : IRequest<ApiResult<Pagination<OutputStandardDTO>>>;

// public class GetOutputStandardHandler : IRequestHandler<GetOutputStandardQuery, ApiResult<Pagination<OutputStandardDTO>>>
// {
//     private readonly IUnitOfWork _unitOfWork;
//     private readonly IMapper _mapper;

//     public GetOutputStandardHandler(IUnitOfWork unitOfWork, IMapper mapper)
//     {
//         _unitOfWork = unitOfWork;
//         _mapper = mapper;
//     }

//     public async Task<ApiResult<Pagination<OutputStandardDTO>>> Handle(GetOutputStandardQuery request, CancellationToken cancellationToken)
//     {
//         var syllabus = await _unitOfWork.OutputStandardRepository.ToPagination(request.PageIndex, request.PageSize);
//         var result = _mapper.Map<Pagination<OutputStandardDTO>>(syllabus);
//         return new ApiSuccessResult<Pagination<OutputStandardDTO>>(result);
//     }
// }
