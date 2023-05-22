namespace Application.Syllabuses.DTOs
{
    public class AddOneMaterialToLessonResponse
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public int? UnitLessonId { get; set; }
    }
}