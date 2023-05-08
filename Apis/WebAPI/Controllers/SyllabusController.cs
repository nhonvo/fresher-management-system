using Application.Commons;
using Application.Syllabuses.Commands.CreateSyllabus;
using Application.Syllabuses.Commands.ImportSyllabusesCSV;
using Application.Syllabuses.DTO;
using Application.Syllabuses.Queries.ExportSyllabusesCSV;
using Application.Syllabuses.Queries.GetSyllabus;
using Application.Syllabuses.Queries.GetSyllabusById;
using Domain.Aggregate.AppResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class SyllabusController : BaseController
    {
        private readonly IMediator _mediator;
        public SyllabusController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // TODO: FIXE pagination user params or move the another layer
        [HttpGet]
        public async Task<ApiResult<Pagination<SyllabusDTO>>> Get(int pageIndex = 0, int pageSize = 10)
         => await _mediator.Send(new GetSyllabusQuery(pageIndex, pageSize));
        [HttpGet("{id}")]
        public async Task<SyllabusDTO> Get(int id)
         => await _mediator.Send(new GetSyllabusByIdQuery(id));
        [HttpPost]
        public async Task<SyllabusDTO> Post([FromBody] CreateSyllabusCommand request)
        => await _mediator.Send(request);

        #region CSV
        [HttpGet("export-syllabuses-csv")]
        public async Task<FileStreamResult> ExportSyllabusesCSV(string columnSeparator)
            => await _mediator.Send(new ExportSyllabusesCSVQuery(columnSeparator));

        [HttpPost("import-syllabuses-csv")]
        public async Task<List<SyllabusDTO>> ImportSyllabusesCSV(
            [FromQuery] bool IsScanCode,
            [FromQuery] bool IsScanName,
            [FromQuery] DuplicateHandle DuplicateHandle,
            [FromForm] IFormFile formFile
            )
            => await _mediator.Send(new ImportSyllabusesCSVCommand()
            {
                FormFile = formFile,
                IsScanCode = IsScanCode,
                IsScanName = IsScanName,
                DuplicateHandle = DuplicateHandle
            });
        #endregion CSV
    }
}