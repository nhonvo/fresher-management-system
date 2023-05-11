using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class ApproveRequestConfiguration : IEntityTypeConfiguration<ApproveRequest>
    {
        public void Configure(EntityTypeBuilder<ApproveRequest> builder)
        {
            builder.ToTable("ApproveRequests");

            //Id
            builder.HasKey(x => x.Id);

            // student
            builder.HasOne(x => x.Student)
                   .WithMany(x => x.ApproveRequests)
                   .HasForeignKey(x => x.StudentId);
            // class
            builder.HasOne(x => x.TrainingClass)
                   .WithMany(x => x.ApproveRequests)
                   .HasForeignKey(x => x.ClassId);
        }
    }
    public class FeedBackConfiguration : IEntityTypeConfiguration<FeedBack>
    {
        public void Configure(EntityTypeBuilder<FeedBack> builder)
        {
            builder.ToTable("FeedBack");

            //Id
            builder.HasKey(x => x.Id);

            // student
            //Trainee
            builder.HasOne(x => x.Trainee)
                .WithMany(x => x.FeedbackTrainee)
                .HasForeignKey(x => x.TraineeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //Trainer
            builder.HasOne(x => x.Trainer)
                .WithMany(x => x.FeedbackTrainer)
                .HasForeignKey(x => x.TrainerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
