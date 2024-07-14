﻿using Application.ApproveRequests.DTOs;
using Application.Commons;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ApproveRequests.GetApproveFilter
{

    public record GetApproveFilterQuery(StatusApprove order = StatusApprove.Waiting, int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<ApproveRequestRelatedDTO>>;

    public class GetApproveFilterHandler : IRequestHandler<GetApproveFilterQuery, Pagination<ApproveRequestRelatedDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetApproveFilterHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<ApproveRequestRelatedDTO>> Handle(GetApproveFilterQuery request, CancellationToken cancellationToken)
        {
            var approveRequest = await _unitOfWork.ApproveRequestRepository.GetAsync(
                filter: x => x.Status == request.order,
                include: x => x.Include(x => x.Student)
                               .Include(x => x.Admin)
                               .Include(x => x.TrainingClass),
                pageIndex: request.PageIndex,
                pageSize: request.PageSize);

            var result = _mapper.Map<Pagination<ApproveRequestRelatedDTO>>(approveRequest);

            return result;
        }
    }
}


