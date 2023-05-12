using Application.Account.Commands.CreateAccount;
using Application.Account.Commands.CreateAccountTrainer;
using FluentValidation;

namespace Application.Account.Commands.ChangPassword;

public class CreateAccountTrainerValidator : AbstractValidator<CreateAccountTrainerCommand>
{
    public CreateAccountTrainerValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
