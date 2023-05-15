using Application.Users.Queries.ExportUsers;
using CsvHelper.Configuration;
using System.Globalization;

namespace Infrastructure.Files.Maps;

public class UserRecordMap : ClassMap<UserCSV>
{
    public UserRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.DateOfBirth).Convert(c => c.Value.DateOfBirth.ToString("yyyy/MM/dd"));
    }
}
