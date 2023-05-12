using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FeedBacks.Commands.CreateFeedBack
{
    public class CreateFeedBackCommandValidator : AbstractValidator<CreateFeedBackCommand>
    {
        public CreateFeedBackCommandValidator()
        {
            RuleFor(x => x.Rating).InclusiveBetween(1, 5);
            RuleFor(x => x.Comment).MaximumLength(500);
            RuleFor(x => x.StudentId).GreaterThan(0);
        }
    }

}
