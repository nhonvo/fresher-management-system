using Application.Account.DTOs;
using Application.Account.Queries.GetAttendanceByClass;
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
        {
            return await _mediator.Send(new GetAttendanceByClassQuery(id));
        }
    }
}
