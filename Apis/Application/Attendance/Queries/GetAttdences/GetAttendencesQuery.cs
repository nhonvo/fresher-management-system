//using Application.Attendences.DTO;
//using Application.Commons;
//using AutoMapper;
//using Domain.Aggregate.AppResult;
//using MediatR;

//namespace Application.Attendences.Queries.GetAttdences
//{

//    public record AttendencesQuery(int PageIndex = 0, int PageSize = 10) : IRequest<ApiResult<Pagination<AttendanceDTO>>>;

//    public class AttdendenceHandler : IRequestHandler<AttendencesQuery, ApiResult<Pagination<AttendanceDTO>>>
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;

//        public AttdendenceHandler(IUnitOfWork unitOfWork, IMapper mapper)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//        }

//        public async Task<ApiResult<Pagination<AttendanceDTO>>> Handle(AttendencesQuery request, CancellationToken cancellationToken)
//        {
//            var attendences = await _unitOfWork.AttendanceRepository.ToPagination(request.PageIndex, request.PageSize);
//            var result = _mapper.Map<Pagination<AttendanceDTO>>(attendences);
//            return new ApiSuccessResult<Pagination<AttendanceDTO>>(result);
//        }
//    }
    
//}



