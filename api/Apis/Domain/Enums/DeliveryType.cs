using System.Text.Json.Serialization;

namespace Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DeliveryType
    {
        AssignmentLab = 0,

        ConceptLecture = 1,

        GuideReview = 2,

        TestQuiz = 3,

        Exam = 4,

        SeminarWorkshop = 5
    }
}
