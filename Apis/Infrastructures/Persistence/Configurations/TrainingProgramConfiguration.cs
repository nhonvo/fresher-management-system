using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class TrainingProgramConfiguration : IEntityTypeConfiguration<TrainingProgram>
    {
        public void Configure(EntityTypeBuilder<TrainingProgram> builder)
        {
            builder.ToTable("TrainingPrograms");

            //Id
            builder.HasKey(x => x.Id);

            //Name
            builder.Property(x => x.Name).HasMaxLength(500);

            //CreatedBy
            builder.Property(x => x.CreatedBy).IsRequired(false);

            //ModifiedBy
            builder.Property(x => x.LastModifyBy).IsRequired(false);


            //Modified
            builder.HasOne(x => x.ModifiedAdmin)
                .WithMany(x => x.ModifyTrainingProgram)
                .HasForeignKey(x => x.LastModifyBy);

            //Created
            builder.HasOne(x => x.CreatedAdmin)
                .WithMany(x => x.CreatedTrainingProgram)
                .HasForeignKey(x => x.CreatedBy);
        }
    }
}