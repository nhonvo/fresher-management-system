using System.Text.Json.Serialization;

namespace Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AttendeeType
    {
        Intern = 0,
        Fresher = 1,
        OnlineFeeFresher = 2,
        OfflineFeeFresher = 3
    }
}
