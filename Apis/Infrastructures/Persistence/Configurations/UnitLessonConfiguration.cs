using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class UnitLessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lesson");
            builder.HasKey(x => x.Id);

            builder.HasOne(o => o.OutputStandard)
                   .WithMany(u => u.UnitLessons)
                   .HasForeignKey(o => o.OutputStandardId)
                   .OnDelete(DeleteBehavior.Cascade);
                   
            builder.HasOne(o => o.Unit)
                   .WithMany(u => u.Lessons)
                   .HasForeignKey(o => o.UnitId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
