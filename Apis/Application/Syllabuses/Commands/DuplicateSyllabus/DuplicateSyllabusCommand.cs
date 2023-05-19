using System.Security.Cryptography.X509Certificates;
using Application.Common.Exceptions;
using Application.Syllabuses.DTO;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Syllabuses.Commands.DuplicateSyllabus
{
    public record DuplicateSyllabusCommand(int id) : IRequest<SyllabusDTO>;
    public class DuplicateSyllabusHandler : IRequestHandler<DuplicateSyllabusCommand, SyllabusDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DuplicateSyllabusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SyllabusDTO> Handle(DuplicateSyllabusCommand request, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.SyllabusRepository.AnyAsync(x => x.Id == request.id);
            if (exist is false)
                throw new NotFoundException("Syllabus not found");
            var syllabus = await _unitOfWork.SyllabusRepository.FirstOrdDefaultAsync(
                filter: x => x.Id == request.id,
                include: x => x.Include(x => x.Units)
                               .ThenInclude(x => x.UnitLessons)
                               .ThenInclude(x => x.TrainingMaterials));
            var removeIdSyllabus = _mapper.Map<SyllabusDuplicate>(syllabus);
            var duplicateSyllabus = _mapper.Map<Syllabus>(removeIdSyllabus);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.SyllabusRepository.AddAsync(duplicateSyllabus);
            });
            var result = _mapper.Map<SyllabusDTO>(duplicateSyllabus);
            return result;
        }
    }
}