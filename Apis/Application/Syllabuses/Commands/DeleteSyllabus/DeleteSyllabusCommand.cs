using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Syllabuses.DTO;
using AutoMapper;
using MediatR;

namespace Application.Syllabuses.Commands.DeleteSyllabus
{
    public record DeleteSyllabusCommand(int id) : IRequest<SyllabusDTO>;
    public class DeleteSyllabusHandler : IRequestHandler<DeleteSyllabusCommand, SyllabusDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteSyllabusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SyllabusDTO> Handle(DeleteSyllabusCommand request, CancellationToken cancellationToken)
        {
            var syllabus = await _unitOfWork.SyllabusRepository.GetByIdAsync(request.id);
            if (syllabus == null)
                throw new NotFoundException("Syllabus not found");
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() => { _unitOfWork.SyllabusRepository.Delete(syllabus); });
                var result = _mapper.Map<SyllabusDTO>(syllabus);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}