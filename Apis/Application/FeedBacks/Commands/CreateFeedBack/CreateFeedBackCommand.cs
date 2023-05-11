﻿using Application.Common.Exceptions;
using Application.FeedBacks.DTO;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FeedBacks.Commands.CreateFeedBack
{
    public record CreateFeedBackCommand : IRequest<FeedBackDTO>
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int StudentId { get; set; }
    }

    public class CreateFeedBackHandler : IRequestHandler<CreateFeedBackCommand, FeedBackDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFeedBackHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FeedBackDTO> Handle(CreateFeedBackCommand request, CancellationToken cancellationToken)
        {
            var feedback = _mapper.Map<FeedBack>(request);
            
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.FeedBackRepository.AddAsync(feedback);
            });
            var result = _mapper.Map<FeedBackDTO>(feedback);

            return result ?? throw new NotFoundException("Feed back can not create");
        }
    }
}