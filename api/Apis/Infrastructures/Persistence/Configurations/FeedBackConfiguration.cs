using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
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
