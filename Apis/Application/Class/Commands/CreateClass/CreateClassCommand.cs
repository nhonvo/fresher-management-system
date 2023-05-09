using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Class.Commands.CreateClass
{
    public record CreateClassCommand : IRequest<ClassDTO>
    {

    }
    public class CreateClassCommand
    {
    }
}
