using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class TrainingProgramConfiguration : IEntityTypeConfiguration<TrainingProgram>
    {
        public void Configure(EntityTypeBuilder<TrainingProgram> builder)
        {
            //builder.HasOne(o => o.CreatedByUser)
            //       .WithMany(u => u.CreatedTrainingPrograms)
            //       .HasForeignKey(o => o.CreatedByUserId)
            //       .OnDelete(DeleteBehavior.NoAction);
            //builder.HasOne(o => o.ModifiedByUser)
            //       .WithMany(u => u.ModifiedTrainingPrograms)
            //       .HasForeignKey(o => o.ModifiedByUserId)
            //       .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.TrainingClass)
                   .WithOne(u => u.TrainingProgram)
                   .HasForeignKey<TrainingProgram>(o => o.TrainingClassId);
            builder.HasOne(e => e.Parent)
                    .WithMany()
                    .HasForeignKey(m => m.ParentId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
