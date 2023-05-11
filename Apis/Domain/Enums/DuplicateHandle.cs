using System.Text.Json.Serialization;

namespace Apis.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DuplicateHandle
{
    Ignore,
    Replace,
    Throw
}