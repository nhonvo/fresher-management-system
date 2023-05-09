using Application.Services;
using Application.Syllabuses.DTO;
using AutoMapper;
using Domain.Entities.Syllabuses;
using Domain.Enums.SyllabusEnums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Syllabuses.Commands.ImportSyllabusesCSV;

public enum DuplicateHandle
{
    Ignore,
    Replace,
    Throw
}

public record ImportSyllabusesCSVCommand : IRequest<List<SyllabusDTO>>
{
    public IFormFile FormFile { get; set; }
    public bool IsScanCode { get; set; }
    public bool IsScanName { get; set; }
    public DuplicateHandle DuplicateHandle { get; set; }
}

public class ImportSyllabusesCSVHandler : IRequestHandler<ImportSyllabusesCSVCommand, List<SyllabusDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ImportSyllabusesCSVHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SyllabusDTO>> Handle(ImportSyllabusesCSVCommand request, CancellationToken cancellationToken)
    {
        var newSyllabuses = await ConvertToSyllabusDTOList(
            request.FormFile,
            request.IsScanCode,
            request.IsScanName);
        var items = new List<SyllabusDTO>();
        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            foreach (var item in newSyllabuses)
            {
                var syllabus = _mapper.Map<Syllabus>(item);
                var oldSyllabus = await _unitOfWork.SyllabusRepository.FirstOrdDefaultAsync(s => s.Code == item.Code);
                if (oldSyllabus != null)
                {
                    if (request.DuplicateHandle == DuplicateHandle.Ignore)
                        continue; // skip the syllabus if it already exists
                    else if (request.DuplicateHandle == DuplicateHandle.Replace)
                        _unitOfWork.SyllabusRepository.Delete(oldSyllabus);
                    else if (request.DuplicateHandle == DuplicateHandle.Throw)
                        throw new DuplicateWaitObjectException("Duplicate code found in CSV file");
                }
                await _unitOfWork.SyllabusRepository.AddAsync(syllabus);
                var syllabusDTO = _mapper.Map<SyllabusDTO>(syllabus);
                newSyllabuses.Add(syllabusDTO);
            }
        });
        items = items.Count > 0 ? items : newSyllabuses;
        return items;
    }

    private async Task<List<SyllabusDTO>> ConvertToSyllabusDTOList(
        IFormFile formFile,
        bool isScanCode,
        bool isScanName)
    {
        var newSyllabuses = new List<SyllabusDTO>();
        using (var reader = new StreamReader(formFile.OpenReadStream()))
        {
            var header = await reader.ReadLineAsync(); // skip the header line
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (line == null) continue; // skip empty lines (if any
                var values = line.Split(',');
                if (values.Length < 3)
                    throw new InvalidDataException("Invalid CSV format");
                var syllabusDTO = new SyllabusDTO
                {
                    Code = isScanCode ? values[1] : "",
                    Version = isScanName ? float.TryParse(values[2], out var version) ? version : 0 : 0,
                    Level = (int.TryParse(values[3], out var level) ? level : 0) switch
                    {
                        1 => SyllabusLevel.Beginner,
                        2 => SyllabusLevel.Intermediate,
                        3 => SyllabusLevel.Advance,
                        0 => SyllabusLevel.AllLevel,
                        _ => SyllabusLevel.AllLevel,
                    },
                    AttendeeNumber = int.TryParse(values[4], out var attendeeNumber) ? attendeeNumber : 0,
                    CourseObjectives = values[5] ?? "",
                    TechnicalRequirements = values[6] ?? "",
                    TrainingDeliveryPrinciple = values[7] ?? "",
                    QuizCriteria = float.TryParse(values[8], out var quizCriteria) ? quizCriteria : 0,
                    AssignmentCriteria = float.TryParse(values[9], out var assignmentCriteria) ? assignmentCriteria : 0,
                    FinalCriteria = float.TryParse(values[10], out var finalCriteria) ? finalCriteria : 0,
                    FinalTheoryCriteria = float.TryParse(values[11], out var finalTheoryCriteria) ? finalTheoryCriteria : 0,
                    FinalPracticalCriteria = float.TryParse(values[12], out var finalPracticalCriteria) ? finalPracticalCriteria : 0,
                    PassingGPA = float.TryParse(values[13], out var passingGPA) ? passingGPA : 0,
                    IsActive = bool.TryParse(values[14], out var isActive) && isActive,
                    Duration = int.TryParse(values[15], out var duration) ? duration : 0,
                    Name = values[16] ?? "",
                    LastModifiedOn = DateTime.TryParse(values[17], out var lastModifiedOn) ? lastModifiedOn : DateTime.Now,
                    LastModifiedBy = int.TryParse(values[18], out var lastModifiedBy) ? lastModifiedBy : null,

                };
                newSyllabuses.Add(syllabusDTO);
            }
        }

        return newSyllabuses;
    }
}
