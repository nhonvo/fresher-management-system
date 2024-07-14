﻿using Domain.Enums;

namespace Application.Lessons.DTO
{
    public class UnitLessonDTO
    {
        public string? Name { get; set; }
        public int? Duration { get; set; }
        public LessonType LessonType { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public int UnitId { get; set; }
    }
}
