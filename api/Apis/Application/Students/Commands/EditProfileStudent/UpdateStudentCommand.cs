using Application.Common.Exceptions;
using Application.Students.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
namespace Application.Student.Commands.UpdateStudent
{
    public record UpdateStudentCommand : IRequest<StudentDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
    }
    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, StudentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateStudentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<StudentDTO> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (user == null)
                throw new NotFoundException("Student not found");
            user = _mapper.Map<User>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.UserRepository.Update(user);
            });
            var result = _mapper.Map<StudentDTO>(user);
            return result ?? throw new NotFoundException("Can not update student");
        }
    }
}
