using System.Text;
using Application.Syllabuses.DTO;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Syllabuses.Queries.ExportSyllabusesCSV;

public record ExportSyllabusesCSVQuery(string columnSeparator) : IRequest<FileStreamResult>;

public class ExportSyllabusesCSVHandler : IRequestHandler<ExportSyllabusesCSVQuery, FileStreamResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ExportSyllabusesCSVHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<FileStreamResult> Handle(ExportSyllabusesCSVQuery request, CancellationToken cancellationToken)
    {
        var pagedItems = await _unitOfWork.SyllabusRepository.GetAsync();
        var syllabuses = _mapper.Map<List<SyllabusDTO>>(pagedItems.Items);
        var csv = ConvertToCSV(syllabuses, request.columnSeparator);
        return csv;
    }

    private FileStreamResult ConvertToCSV(List<SyllabusDTO> syllabuses, string columnSeparator)
    {
        var csv = new StringBuilder();
        csv.AppendLine(
            "id" + columnSeparator +
            "Code" + columnSeparator +
            "Version" + columnSeparator +
            "Level" + columnSeparator +
            "AttendeeNumber" + columnSeparator +
            "CourseObjectives" + columnSeparator +
            "TechnicalRequirements" + columnSeparator +
            "TrainingDeliveryPrinciple" + columnSeparator +
            "QuizCriteria" + columnSeparator +
            "AssignmentCriteria" + columnSeparator +
            "FinalCriteria" + columnSeparator +
            "FinalTheoryCriteria" + columnSeparator +
            "FinalPracticalCriteria" + columnSeparator +
            "PassingGPA" + columnSeparator +
            "IsActive" + columnSeparator +
            "Duration" + columnSeparator +
            "Name" + columnSeparator +
            "LastModifiedOn" + columnSeparator +
            "LastModifiedBy");

        foreach (var syllabus in syllabuses)
        {
            csv.AppendLine(
                "id" + columnSeparator + //0
                 syllabus.Code + columnSeparator + //1
                 syllabus.Version + columnSeparator + //2
                 syllabus.Level + columnSeparator + //3
                 syllabus.AttendeeNumber + columnSeparator + //4
                 syllabus.CourseObjectives + columnSeparator + //5
                 syllabus.TechnicalRequirements + columnSeparator + //6
                 syllabus.TrainingDeliveryPrinciple + columnSeparator + //7
                 syllabus.QuizCriteria + columnSeparator + //8
                 syllabus.AssignmentCriteria + columnSeparator + //9
                 syllabus.FinalCriteria + columnSeparator + //10
                 syllabus.FinalTheoryCriteria + columnSeparator + //11
                 syllabus.FinalPracticalCriteria + columnSeparator + //12
                 syllabus.PassingGPA + columnSeparator + //13
                 syllabus.IsActive + columnSeparator + //14
                 syllabus.Duration + columnSeparator + //15
                 syllabus.Name + columnSeparator + //16
                 syllabus.LastModifiedOn + columnSeparator + //17
                 syllabus.LastModifiedBy + "" //18
            );
        }

        var csvBytes = Encoding.UTF8.GetBytes(csv.ToString());
        var csvStream = new MemoryStream(csvBytes);
        var now = DateTime.Now.ToLongDateString();
        // return new File(csvStream.ToArray(), "text/csv", $"syllabuses-{now}.csv");
        return new FileStreamResult(csvStream, "application/octet-stream")
        {
            FileDownloadName = $"syllabuses-{now}.csv"
        };
    }
}
