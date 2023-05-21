using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class TrainingMaterialConfiguration : IEntityTypeConfiguration<TrainingMaterial>
    {
        public void Configure(EntityTypeBuilder<TrainingMaterial> builder)
        {
            builder.ToTable("TrainingMaterial");

            builder.HasOne(o => o.UnitLesson)
                 .WithMany(u => u.TrainingMaterials)
                 .HasForeignKey(o => o.UnitLessonId);

            builder.HasOne(x => x.TestAssessment)
                   .WithMany(x => x.TrainingMaterials)
                   .HasForeignKey(x => x.TestAssessmentId);
        }
    }
}
