using FluentValidation;

namespace Application.Account.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email address is not in a valid format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is requied.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
    }
}
