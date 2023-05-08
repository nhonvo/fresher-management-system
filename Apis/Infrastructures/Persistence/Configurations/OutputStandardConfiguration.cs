using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class OutputStandardConfiguration : IEntityTypeConfiguration<OutputStandard>
    {
        public void Configure(EntityTypeBuilder<OutputStandard> builder)
        {
            builder.ToTable("OutputStandards");

            //Id
            builder.HasKey(x => x.Id);

            //Code
            builder.Property(x => x.Code)
                .IsRequired();

            //Description
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            //Lecture
            builder.HasMany(x => x.Lectures)
                .WithOne(x => x.OutputStandard)
                .HasForeignKey(x => x.OutputStandardId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}