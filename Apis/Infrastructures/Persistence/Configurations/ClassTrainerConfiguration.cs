using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class ClassTrainerConfiguration : IEntityTypeConfiguration<ClassTrainer>
    {
        public void Configure(EntityTypeBuilder<ClassTrainer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(ca => ca.Trainer)
                   .WithMany(tc => tc.ClassTrainers)
                   .HasForeignKey(ca => ca.TrainerId);
            builder.HasOne(ca => ca.TrainingClass)
                   .WithMany(tc => tc.ClassTrainers)
                   .HasForeignKey(ca => ca.TrainerId);
            builder.HasOne(ca => ca.CreateByUser)
                   .WithMany(tc => tc.CreatedClassTrainers)
                   .HasForeignKey(ca => ca.CreatedBy);
        }
    }
}
