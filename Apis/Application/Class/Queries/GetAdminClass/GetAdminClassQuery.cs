using Application.Class.DTOs;
using Application.Commons;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Class.Queries.GetAdminClass
{
    public record GetAdminClassQuery(int id, int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<AdminClass>>;
    public class GetAdminClassHandler : IRequestHandler<GetAdminClassQuery, Pagination<AdminClass>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;


        public GetAdminClassHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
        }
        public async Task<Pagination<AdminClass>> Handle(GetAdminClassQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.ClassRepository.GetAsync(
                filter: x => x.Id == request.id,
                include: x => x.Include(x => x.ClassAdmins),
                pageIndex: request.PageIndex,
                pageSize: request.PageSize);
            var result = _mapper.Map<Pagination<AdminClass>>(user);
            return result;
        }
    }
}