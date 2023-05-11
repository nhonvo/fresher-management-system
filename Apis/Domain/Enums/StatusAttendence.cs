using System.Text.Json.Serialization;

namespace Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusAttendance
    {
        Approve = 0,
        Reject = 1,
        Waiting = 2
    }
}
