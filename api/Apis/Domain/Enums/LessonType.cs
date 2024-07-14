using System.Text.Json.Serialization;

namespace Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LessonType
    {
        Online = 0,
        Offline = 1
    }
}
