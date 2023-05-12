using System.Globalization;
using Application.Users.Queries.ExportUsers;
using CsvHelper.Configuration;

namespace Infrastructure.Files.Maps;

public class UserRecordMap : ClassMap<UserRecord>
{
    public UserRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.DateOfBirth).Convert(c => c.Value.DateOfBirth.ToString("yyyy/MM/dd"));
    }
}
