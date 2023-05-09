using Domain.Entities.Syllabuses;
using Domain.Enums.SyllabusEnums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class SyllabusConfiguration : IEntityTypeConfiguration<Syllabus>
    {
        public void Configure(EntityTypeBuilder<Syllabus> builder)
        {
            builder.ToTable("Syllabus");

            //Id
            builder.HasKey(x => x.Id);

            //Name
            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);

            //Code
            builder.Property(x => x.Code).IsRequired().HasMaxLength(10);

            //isActive
            builder.Property(x => x.IsActive).IsRequired();

            //isDeleted
            builder.Property(x => x.IsDeleted).IsRequired();

            //CreatedBy
            builder.Property(x => x.CreatedBy).IsRequired(false);

            //Modified
            builder.Property(x => x.LastModifiedBy).IsRequired(false);

            //TrainingDeliveryPrincipleId
            builder.Property(x => x.TrainingDeliveryPrinciple)
                .IsRequired(false);

            //Units
            builder.HasMany(x => x.Units)
                .WithMany(x => x.Syllabuses);

            //TrainingPrograms
            builder.HasMany(x => x.TrainingPrograms)
                .WithMany(x => x.Syllabuses);



            //Created
            builder.HasOne(x => x.CreatedAdmin)
                .WithMany(x => x.CreatedSyllabus)
                .HasForeignKey(x => x.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //Modified
            builder.HasOne(x => x.ModifiedAdmin)
                .WithMany(x => x.ModifiedSyllabus)
                .HasForeignKey(x => x.LastModifiedBy)
                .OnDelete(DeleteBehavior.ClientSetNull);
            // score

            builder.HasMany(x => x.TestAssessments)
                   .WithOne(x => x.Syllabus)
                   .HasForeignKey(x=>x.SyllabusId);

        }
    }
}