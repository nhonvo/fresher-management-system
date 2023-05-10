using FluentValidation;

namespace Application.Account.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email is required.")
           .EmailAddress().WithMessage("Email is not in the correct format.");

        RuleFor(x => x.Password)
                   .NotEmpty().WithMessage("Password is requied.")
                   .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

        RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("FirstName is required.");

        RuleFor(request => request.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^[0-9]+$").WithMessage("Phone must contain only digits.")
                .Length(10).WithMessage("Phone must be exactly 10 digits.");
    }
}
