using Application.Attendances.DTOs;
using Application.Attendances.Queries.GetAttendanceByClass;
using Application.Commons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AttendanceController : BasesController
    {
        private readonly IMediator _mediator;
        public AttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}/class")]
        public async Task<Pagination<AttendanceDTO>> GetAsync(int id)
           => await _mediator.Send(new GetAttendanceByClassQuery(id));
    }
}

