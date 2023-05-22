using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Users.DTO;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.Student.Commands.EditProfile
{
    public record EditProfileCommand : IRequest<UserDTO>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
    }
    public class EditProfileHandler : IRequestHandler<EditProfileCommand, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;

        public EditProfileHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentTime currentTime, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
            _currentTime = currentTime;
        }
        public async Task<UserDTO> Handle(EditProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Id == _claimService.CurrentUserId);
            if (user is null)
            {
                throw new NotFoundException("Student not found");
            }
            // user = _mapper.Map<User>(request);
            user.Name = request.Name;
            user.Email = request.Email;
            user.Phone = request.Phone;
            user.DateOfBirth = request.DateOfBirth;
            user.Gender = request.Gender;
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.UserRepository.Update(user);
            });
            var result = _mapper.Map<UserDTO>(user);
            return result;
        }
    }
}
