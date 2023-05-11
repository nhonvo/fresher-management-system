using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Units.Commands.CreateUnit
{
    public class CreateUnitCommandValidator : AbstractValidator<CreateUnitCommand>
    {
        public CreateUnitCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.SyllabusSession).GreaterThan(0);
            RuleFor(x => x.UnitNumber).GreaterThan(0);
            RuleFor(x => x.CreationDate).NotEmpty();
        }
    }

}
