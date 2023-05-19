using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class TrainingClassConfiguration : IEntityTypeConfiguration<TrainingClass>
    {
        public void Configure(EntityTypeBuilder<TrainingClass> builder)
        {
            builder.HasOne(tc => tc.ReviewBy)
                   .WithMany(u => u.ReviewTrainingClasses)
                   .HasForeignKey(tc => tc.ReviewByUserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(tc => tc.ApproveBy)
                   .WithMany(u => u.ApprovedTrainingClasses)
                   .HasForeignKey(tc => tc.ApproveByUserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(tc => tc.TrainingProgram)
                   .WithOne(tp => tp.TrainingClass)
                   .HasForeignKey<TrainingClass>(tc => tc.TrainingProgramId);
            builder.HasMany(x => x.Calenders)
                   .WithOne(x => x.TrainingClass)
                   .HasForeignKey(x => x.TrainingClassId)
                   .HasPrincipalKey(x => x.Id)
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}