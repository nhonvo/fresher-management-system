using FluentValidation;

namespace Application.FeedBacks.Commands.CreateFeedBack
{
    public class CreateFeedBackCommandValidator : AbstractValidator<CreateFeedBackCommand>
    {
        public CreateFeedBackCommandValidator()
        {
            RuleFor(x => x.Rating).InclusiveBetween(1, 5);
            RuleFor(x => x.Comment).MaximumLength(500);
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }

}
