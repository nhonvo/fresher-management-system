﻿using Application.Class.DTO;
using Application.Common.Exceptions;
using Application.Interfaces;
using Application.TrainingPrograms.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Class.Commands.AddTrainProgramToClass
{
    public record AddTrainProgramToClassCommand : IRequest<ClassDTO>
    {
        public int ClassId { get; init; }
        public int TrainProgramId { get; init; }
    }
    /// <summary>
    /// create add new training program to class 
    /// when add we duplicate the training program
    /// </summary>
    public class AddTrainProgramToClassHandler : IRequestHandler<AddTrainProgramToClassCommand, ClassDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public AddTrainProgramToClassHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IClaimService claimService,
            ICurrentTime currentTime)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _claimService = claimService;
            _currentTime = currentTime;
        }
        public async Task<ClassDTO> Handle(AddTrainProgramToClassCommand request, CancellationToken cancellationToken)
        {
            var isExistClass = await _unitOfWork.ClassRepository.AnyAsync(x => x.Id == request.ClassId);
            if (isExistClass is false)
            {
                throw new NotFoundException("Training class not found");
            }
            var isExistTrainProgram = await _unitOfWork.TrainingProgramRepository.AnyAsync(x => x.Id == request.TrainProgramId);
            if (isExistTrainProgram is false)
            {
                throw new NotFoundException("Training program not found");
            }
            var trainingClass = await _unitOfWork.ClassRepository.FirstOrdDefaultAsync(x => x.Id == request.ClassId);
            trainingClass.TrainingProgramId = request.TrainProgramId;
            await _unitOfWork.CommitAsync();
            var result = _mapper.Map<ClassDTO>(trainingClass);
            return result;
        }

    }
}