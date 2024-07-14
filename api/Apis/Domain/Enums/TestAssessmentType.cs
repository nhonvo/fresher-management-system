using System.Text.Json.Serialization;

namespace Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TestAssessmentType
    {
        Quiz = 0,
        Assignment = 1,
        FinalTheory = 2,
        FinalPractice = 3
    }
}
