using Apis.Domain.Enums;
using Application.Commons;
using Application.Student.Commands.EditProfile;
using Application.Student.Commands.UpdateUser;
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
    [Authorize]
    public class UserController : CustomBaseController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        // [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAsync(
            string? keyword,
            SortType sortType = SortType.Ascending,
            int pageIndex = 0,
            int pageSize = 10)
            => Ok(await _mediator.Send(new GetUserQuery(keyword, pageIndex, pageSize, sortType)));

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put(UpdateUserCommand request)
            => Ok(await _mediator.Send(request));

        [HttpGet("Profile")]
        [Authorize]
        public async Task<IActionResult> GetAsync()
            => Ok(await _mediator.Send(new GetProfileQuery()));
        [HttpPut("Profile")]
        [Authorize]
        public async Task<IActionResult> PutAsync(EditProfileCommand request)
            => Ok(await _mediator.Send(request));

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

        [HttpPost("import-users-csv-v2")]
        public async Task<List<UserCSV>> ImportUsersCSVV2([FromForm] ImportUsersCSVCommand command)
        => await _mediator.Send(command);

        #endregion CSV

        [HttpGet("{id}/tips")]
        public async Task<List<Tip>> GetTipsByUserId(int id)
        => await _mediator.Send(new GetTipsByUserIdQuery(id));
    }
}
