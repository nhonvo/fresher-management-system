using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class UnitLessonConfiguration : IEntityTypeConfiguration<UnitLesson>
    {
        public void Configure(EntityTypeBuilder<UnitLesson> builder)
        {
            builder.HasOne(o => o.OutputStandard)
                   .WithMany(u => u.UnitLessons)
                   .HasForeignKey(o => o.OutputStandardId);
            builder.HasOne(o => o.Unit)
                   .WithMany(u => u.UnitLessons)
                   .HasForeignKey(o => o.UnitId);
        }
    }
}
