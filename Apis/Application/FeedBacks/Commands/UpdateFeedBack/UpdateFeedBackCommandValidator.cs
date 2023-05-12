using FluentValidation;

namespace Application.FeedBacks.Commands.UpdateFeedBack
{
    public class UpdateFeedBackCommandValidator : AbstractValidator<UpdateFeedBackCommand>
    {
        public UpdateFeedBackCommandValidator()
        {
            RuleFor(x => x.Rating).InclusiveBetween(1, 5);
            RuleFor(x => x.Comment).MaximumLength(500);
            RuleFor(x => x.StudentId).GreaterThan(0);
        }
    }
}
