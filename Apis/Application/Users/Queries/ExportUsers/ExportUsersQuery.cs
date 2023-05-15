using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Users.Queries.ExportUsers;

public record ExportUsersQuery : IRequest<ExportUsersVm>;

public class ExportUsersQueryHandler : IRequestHandler<ExportUsersQuery, ExportUsersVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICsvFileBuilder _fileBuilder;

    public ExportUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICsvFileBuilder fileBuilder)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileBuilder = fileBuilder;
    }

    public async Task<ExportUsersVm> Handle(ExportUsersQuery request, CancellationToken cancellationToken)
    {
        var items = await _unitOfWork.UserRepository.GetAsync();
        var records = _mapper.Map<List<UserCSV>>(items.Items);

        var vm = new ExportUsersVm(
            "Users.csv",
            "text/csv",
            _fileBuilder.BuildTUsersFile(records));

        return vm;
    }
}
