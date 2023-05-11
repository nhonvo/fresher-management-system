using Application.Users.Queries.ExportUsers;

namespace Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTUsersFile(IEnumerable<UserRecord> records);
}
