using Application.Account.Commands.CreateAccount;
using FluentValidation;

namespace Application.Account.Commands.ChangPassword;

public class CreateClassAdminCommandValidator : AbstractValidator<CreateClassAdminCommand>
{
    public CreateClassAdminCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
