using FluentValidation;

namespace Application.Account.Commands.ChangPassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.NewPassword).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Password).Equal(x => x.NewPassword);

    }
}
