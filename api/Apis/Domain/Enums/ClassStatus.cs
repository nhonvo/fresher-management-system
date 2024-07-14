using System.Text.Json.Serialization;

namespace Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ClassStatus
    {
        Planning = 0,
        Opening = 1,
        Closed = 2
    }
}
