using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class TrainingMaterialConfiguration : IEntityTypeConfiguration<TrainingMaterial>
    {
        public void Configure(EntityTypeBuilder<TrainingMaterial> builder)
        {
            //builder.HasOne(o => o.ModifiedByUser)
            //       .WithMany(u => u.ModifiedTrainingMaterial)
            //       .HasForeignKey(o => o.ModifiedByUserId);
            builder.HasOne(o => o.UnitLesson)
                   .WithMany(u => u.TrainingMaterials)
                   .HasForeignKey(o => o.UnitLessonId);
        }
    }
}
