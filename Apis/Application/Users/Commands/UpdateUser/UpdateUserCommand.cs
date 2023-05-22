using Application.Common.Exceptions;
using Application.Students.DTO;
using Application.Users.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Student.Commands.UpdateUser
{
    public record UpdateUserCommand : IRequest<UserDTO>
    {
        public int Id { get; set; }
        public UserRoleType Role { get; set; }
        public UserStatus Status { get; set; }
    }
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UserDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            user.Role = request.Role;
            user.Status = request.Status;

            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.UserRepository.Update(user);
            });
            var result = _mapper.Map<UserDTO>(user);
            return result;
        }
    }
}
