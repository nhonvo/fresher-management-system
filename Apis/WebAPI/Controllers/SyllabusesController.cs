using Apis.Domain.Enums;
using Application.Commons;
using Application.Syllabuses.Commands.AddOneLessonToUnit;
using Application.Syllabuses.Commands.AddOneMaterialToLesson;
using Application.Syllabuses.Commands.AddOneUnitToSyllabus;
using Application.Syllabuses.Commands.CreateSyllabus;
using Application.Syllabuses.Commands.DeleteSyllabus;
using Application.Syllabuses.Commands.DuplicateSyllabus;
using Application.Syllabuses.Commands.ImportSyllabusesCSV;
using Application.Syllabuses.Commands.UpdateSyllabus;
using Application.Syllabuses.DTOs;
using Application.Syllabuses.Queries.ExportSyllabusesCSV;
using Application.Syllabuses.Queries.GetPagedSyllabusesByDateRange;
using Application.Syllabuses.Queries.GetSyllabus;
using Application.Syllabuses.Queries.GetSyllabusById;
using Application.Syllabuses.Queries.GetSyllabusDetailById;
using Domain.Enums;
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
    public async Task<IActionResult> GetAsync(
        string? keyword,
        SortType sortType = SortType.Ascending,
        int pageIndex = 0,
        int pageSize = 10)
     => Ok(await _mediator.Send(new GetSyllabusQuery(keyword, pageIndex, pageSize, sortType)));
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
     => Ok(await _mediator.Send(new GetSyllabusByIdQuery(id)));
    [HttpGet("Detail/{id}")]
    public async Task<IActionResult> GetDetailAsync(int id)
     => Ok(await _mediator.Send(new GetSyllabusDetailByIdQuery(id)));
    [HttpPost]
    [Authorize(Roles = "Trainer,SuperAdmin")]
    public async Task<IActionResult> Post([FromBody] CreateSyllabusCommand request)
    => Ok(await _mediator.Send(request));
    [HttpPost("{id}/Unit")]
    [Authorize(Roles = "Trainer")]
    public async Task<IActionResult> Post([FromBody] AddOneUnitToSyllabusCommand request)
    => Ok(await _mediator.Send(request));
    [HttpPost("{id}/Lesson")]
    [Authorize(Roles = "Trainer")]
    public async Task<IActionResult> Post([FromBody] AddOneLessonToUnitCommand request)
    => Ok(await _mediator.Send(request));
    [HttpPost("{id}/TrainingMaterials")]
    [Authorize(Roles = "Trainer")]
    public async Task<IActionResult> Post([FromBody] AddOneMaterialToLessonCommand request)
    => Ok(await _mediator.Send(request));
    // [HttpDelete("{id}/Unit")]
    // public async Task<UnitDTO> DeleteUnit(int id)
    // => await _mediator.Send(new DeleteUnitCommand(id));
    [HttpPut]
    [Authorize(Roles = "Trainer")]
    public async Task<SyllabusDTO> Put([FromBody] UpdateSyllabusCommand request)
    => await _mediator.Send(request);
    [HttpDelete("{id}")]
    public async Task<SyllabusDTO> Delete(int id)
    => await _mediator.Send(new DeleteSyllabusCommand(id));
    [HttpPost("Duplicate/{id}")]
    [Authorize(Roles = "Trainer, ClassAdmin")]
    public async Task<SyllabusDTO> Duplicate(int id)
    => await _mediator.Send(new DuplicateSyllabusCommand(id));

    #region CSV
    [HttpGet("export-syllabuses-csv")]
    public async Task<FileStreamResult> ExportSyllabusesCSV(string? columnSeparator)
    => await _mediator.Send(new ExportSyllabusesCSVQuery(columnSeparator ?? ","));

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

    [HttpPost("import-syllabuses-csv-v2")]
    public async Task<List<SyllabusDTO>> ImportSyllabusesCSVV2([FromForm] ImportSyllabusesCSVCommand command)
    => await _mediator.Send(command);

    #endregion CSV

    [HttpGet("get-paged-syllabuses-by-date-range")]
    public async Task<Pagination<SyllabusDTO>> GetAsyncPagedSyllabusesByDateRange(
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
