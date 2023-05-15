using Application.Account.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Aggregate.AppResult;
using MediatR;
using Microsoft.AspNetCore.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account.Queries.GetUserById
{
    public record GetTokenByIdQuery : IRequest<AccountDTO>;

    public class GetTokenByIdHandler : IRequestHandler<GetTokenByIdQuery, AccountDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;
        private readonly IMapper _mapper;

        public GetTokenByIdHandler(
            IUnitOfWork unitOfWork,
            IClaimService claimService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _claimService = claimService;
            _mapper = mapper;
        }

        public async Task<AccountDTO> Handle(GetTokenByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _claimService.CurrentUserId;
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            var result = _mapper.Map<AccountDTO>(user);
            return result;
        }
    }
}
