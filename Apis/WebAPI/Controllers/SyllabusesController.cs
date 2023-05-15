using Apis.Domain.Enums;
using Application.Commons;
using Application.Syllabuses.Commands.CreateSyllabus;
using Application.Syllabuses.Commands.DeleteSyllabus;
using Application.Syllabuses.Commands.ImportSyllabusesCSV;
using Application.Syllabuses.Commands.UpdateSyllabus;
using Application.Syllabuses.DTO;
using Application.Syllabuses.Queries.ExportSyllabusesCSV;
using Application.Syllabuses.Queries.GetPagedSyllabusesByDateRange;
using Application.Syllabuses.Queries.GetSyllabus;
using Application.Syllabuses.Queries.GetSyllabusById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class SyllabusesController : BasesController
{
    private readonly IMediator _mediator;
    public SyllabusesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<Pagination<SyllabusDTO>> Get(int pageIndex = 0, int pageSize = 10)
     => await _mediator.Send(new GetSyllabusQuery(pageIndex, pageSize));
    [HttpGet("{id}")]
    public async Task<SyllabusDTO> Get(int id)
     => await _mediator.Send(new GetSyllabusByIdQuery(id));
    [HttpPost]
    [Authorize(Roles = "Trainer")]
    public async Task<SyllabusDTO> Post([FromBody] CreateSyllabusCommand request)
    => await _mediator.Send(request);
    [HttpPut]
    [Authorize(Roles = "Trainer")]
    public async Task<SyllabusDTO> Put([FromBody] UpdateSyllabusCommand request)
    => await _mediator.Send(request);
    [HttpDelete("{id}")]
    public async Task<SyllabusDTO> Delete(int id)
    => await _mediator.Send(new DeleteSyllabusCommand(id));

    #region CSV
    [HttpGet("export-syllabuses-csv")]
    public async Task<FileStreamResult> ExportSyllabusesCSV(string columnSeparator)
        => await _mediator.Send(new ExportSyllabusesCSVQuery(columnSeparator));

    [HttpPost("import-syllabuses-csv")]
    public async Task<List<SyllabusDTO>> ImportSyllabusesCSV(
        [FromQuery] bool IsScanCode,
        [FromQuery] bool IsScanName,
        [FromQuery] DuplicateHandle DuplicateHandle,
        [FromForm] IFormFile formFile)
        => await _mediator.Send(new ImportSyllabusesCSVCommand()
        {
            FormFile = formFile,
            IsScanCode = IsScanCode,
            IsScanName = IsScanName,
            DuplicateHandle = DuplicateHandle
        });
    #endregion CSV

    [HttpGet("get-paged-syllabuses-by-date-range")]
    public async Task<Pagination<SyllabusDTO>> GetPagedSyllabusesByDateRange(
        [FromQuery] DateTime fromtDate,
        [FromQuery] DateTime toDate,
        [FromQuery] int pageIndex = 0,
        [FromQuery] int pageSize = 10)
        => await _mediator.Send(new GetPagedSyllabusesByDateRangeQuery()
        {
            FromDate = fromtDate,
            ToDate = toDate,
            PageIndex = pageIndex,
            PageSize = pageSize,
        });
}
