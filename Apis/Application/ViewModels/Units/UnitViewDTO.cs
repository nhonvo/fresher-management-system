using Application.ViewModels.UnitLessons;

namespace Application.ViewModels.Units
{
    public class UnitViewDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Day { get; set; }
        public int SortOrder { get; set; }
        public ICollection<UnitLessonViewDTO> Lessons { get; set; }
    }
}
