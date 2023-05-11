using Application.Class.Commands.AddTrainer;
using FluentValidation;

namespace Application.Lectures.Commands
{
    public class AddTrainerCommandValidator : AbstractValidator<AddTrainerCommand>
    {
        public AddTrainerCommandValidator()
        {
          
        }
    }
}