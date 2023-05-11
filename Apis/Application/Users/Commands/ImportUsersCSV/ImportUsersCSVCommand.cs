using Apis.Domain.Enums;
using Application.Users.Queries.ExportUsers;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Users.Commands.ImportUsersCSV;

public record ImportUsersCSVCommand : IRequest<List<UserRecord>>
{
#pragma warning disable
    public IFormFile FormFile { get; set; }
    public bool IsScanEmail { get; set; }
    public DuplicateHandle DuplicateHandle { get; set; }
}

public class ImportUsersCSVHandler : IRequestHandler<ImportUsersCSVCommand, List<UserRecord>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ImportUsersCSVHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<UserRecord>> Handle(ImportUsersCSVCommand request, CancellationToken cancellationToken)
    {
        var newItems = await ConvertToUserRecordList(request.FormFile);
        var addedItems = new List<UserRecord>();
        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            foreach (var item in newItems)
            {
                var newItem = _mapper.Map<User>(item);
                newItem.Password = "12345678"; // default password
                if (request.IsScanEmail)
                {
                    var oldItem = await _unitOfWork.UserRepository.FirstOrdDefaultAsync(s => s.Email == item.Email);
                    if (oldItem != null)
                    {
                        if (request.DuplicateHandle == DuplicateHandle.Ignore)
                            continue; // skip the item if it already exists
                        else if (request.DuplicateHandle == DuplicateHandle.Replace)
                            _unitOfWork.SyllabusRepository.Delete(oldItem.Id); // delete the old item
                        else if (request.DuplicateHandle == DuplicateHandle.Throw)
                            throw new DuplicateWaitObjectException("Duplicate code found in CSV file"); // throw exception
                    }
                }
                await _unitOfWork.UserRepository.AddAsync(newItem);
                var addedItem = _mapper.Map<UserRecord>(newItem);
                addedItems.Add(addedItem);
            }
        });
        // addedItems = addedItems.Count > 0 ? addedItems : newItems;
        return addedItems;
    }

    private async Task<List<UserRecord>> ConvertToUserRecordList(
        IFormFile formFile)
    {
        var items = new List<UserRecord>();
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
                var item = new UserRecord
                {
                    Email = values[0] ?? "",
                    Gender = Enum.TryParse(typeof(Gender), values[4], out var gender) ? (Gender)gender : Gender.Male,
                    Name = values[2] ?? "",
                    Phone = values[3] ?? "",
                    Role = Enum.TryParse(typeof(UserRoleType), values[4], out var role) ? (UserRoleType)role : UserRoleType.Trainee,
                    Status = Enum.TryParse(typeof(UserStatus), values[5], out var status) ? (UserStatus)status : UserStatus.Active,
                    DateOfBirth = DateTime.TryParse(values[6], out var lastModifiedOn) ? lastModifiedOn : DateTime.Now,
                };
                items.Add(item);
            }
        }

        return items;
    }
}
