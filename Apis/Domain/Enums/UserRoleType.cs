using System.Text.Json.Serialization;

namespace Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRoleType
    {
        SuperAdmin = 0,
        ClassAdmin = 1,
        Trainer = 2,
        Trainee = 3,
        Dev = 4
    }
}
