using Apis.Domain.Enums;
using Application.Commons;
using Application.Emails.Commands.SendMail;
using Application.Emails.Queries;
using Application.Users.Commands.ImportUsersCSV;
using Application.Users.DTO;
using Application.Users.GetProfile.Queries;
using Application.Users.Queries.ExportUsers;
using Application.Users.Queries.GetTipsByUserId;
using Application.Users.Queries.GetUser;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class UserController : CustomBaseController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<Pagination<UserDTO>> GetAsync(
            string? keyword,
            SortType sortType = SortType.Ascending,
            int pageIndex = 0,
            int pageSize = 10)
            => await _mediator.Send(new GetUserQuery(keyword, pageIndex, pageSize, sortType));

        [HttpGet("Profile")]
        [Authorize]
        public async Task<UserDTO> GetAsync()
            => await _mediator.Send(new GetProfileQuery());

        #region CSV
        [HttpGet("export-users-csv")]
        public async Task<FileResult> Get()
        {
            var vm = await _mediator.Send(new ExportUsersQuery());

            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost("import-users-csv")]
        public async Task<List<UserCSV>> ImportUsersCSV(
        [FromQuery] bool? isScanEmail,
        [FromQuery] DuplicateHandle? duplicateHandle,
        [FromForm] IFormFile formFile)
        => await _mediator.Send(new ImportUsersCSVCommand()
        {
            FormFile = formFile,
            IsScanEmail = isScanEmail ?? false,
            DuplicateHandle = duplicateHandle ?? DuplicateHandle.Ignore,
        });
        #endregion CSV

        [HttpGet("{id}/tips")]
        public async Task<IActionResult> GetTipsByUserId(int id)
        => Ok(await _mediator.Send(new GetTipsByUserIdQuery(id)));
    }
}
