﻿using Domain.Entities.Users;
using Domain.Entities;
using Domain.Enums.ClassEnums;
using MediatR;
using Application.Class.DTO;
using AutoMapper;
using Application.Common.Exceptions;

namespace Application.Class.Commands.DeleteClass
{
    public record DeleteClassCommand : IRequest<ClassDTO>
    {
        public int Id { get; set; }
    }
    public class DeleteClassHandler : IRequestHandler<DeleteClassCommand, ClassDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteClassHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ClassDTO> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
        {
            var classes = _unitOfWork.ClassRepository.GetByIdAsync(request.Id);
            if (classes == null)
                throw new NotFoundException("Class not found");

            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ClassRepository.Delete(classes);
            });
            var result = _mapper.Map<ClassDTO>(classes);
            return result ?? throw new NotFoundException("Can not delete class");
        }
    }
}