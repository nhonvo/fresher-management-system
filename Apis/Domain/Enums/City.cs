using System.Text.Json.Serialization;

namespace Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum City
    {
        HCM = 0,
        HN = 1,
        DN = 2
    }
}
