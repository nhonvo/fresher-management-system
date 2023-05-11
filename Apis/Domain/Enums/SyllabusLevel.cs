using System.Text.Json.Serialization;

namespace Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SyllabusLevel
    {
        AllLevel = 0,
        Beginner = 1,
        Intermediate = 2,
        Advance = 3,
    }
}
