using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.
Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class TrainingProgramConfiguration : IEntityTypeConfiguration<TrainingProgram>
    {
        public void Configure(EntityTypeBuilder<TrainingProgram> builder)
        {
            // builder.HasKey(x => x.Id);
            
            builder.HasOne(o => o.CreateByUser)
                  .WithMany(u => u.CreatedTrainingPrograms)
                  .HasForeignKey(o => o.CreatedBy)
                  .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(o => o.ModificationByUser)
                  .WithMany(u => u.ModifiedTrainingPrograms)
                  .HasForeignKey(o => o.ModificationBy)
                  .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(o => o.TrainingClass)
                   .WithOne(u => u.TrainingProgram)
                   .HasForeignKey<TrainingClass>(o => o.TrainingProgramId);
            
            builder.HasOne(e => e.Parent)
                    .WithMany()
                    .HasForeignKey(m => m.ParentId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
