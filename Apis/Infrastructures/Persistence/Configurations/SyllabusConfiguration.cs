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
            builder.Property(x => x.isDeleted).IsRequired();

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
            /*
                        builder.HasData(
                            new Syllabus
                            {
                                Id = 1,
                                Code = "Code",
                                Version = 1,
                                Name = "Name",
                                LastModifiedOn = new DateTime(2023, 1, 2),
                                LastModifiedBy = 1,
                                Level = SyllabusLevel.AllLevel,
                                AttendeeNumber = 1,
                                CourseObjectives = "CourseObjectives",
                                TechnicalRequirements = "TechnicalRequirements",
                                TrainingDeliveryPrinciple = "TrainingDeliveryPrinciple",
                                QuizCriteria = 1,
                                AssignmentCriteria = 1,
                                FinalCriteria = 1,
                                FinalTheoryCriteria = 1,
                                FinalPracticalCriteria = 1,
                                PassingGPA = 1,
                                IsActive = true,
                                Duration = 10,
                            },
                            new Syllabus
                            {
                                Id = 2,
                                Code = "abc",
                                Version = 1,
                                Name = "nhon",
                                LastModifiedOn = new DateTime(2023, 1, 2),
                                LastModifiedBy = 1,
                                Level = SyllabusLevel.AllLevel,
                                AttendeeNumber = 1,
                                CourseObjectives = "CourseObjectives 1",
                                TechnicalRequirements = "TechnicalRequirements 1",
                                TrainingDeliveryPrinciple = "TrainingDeliveryPrinciple 1",
                                QuizCriteria = 10,
                                AssignmentCriteria = 10,
                                FinalCriteria = 10,
                                FinalTheoryCriteria = 10,
                                FinalPracticalCriteria = 10,
                                PassingGPA = 10,
                                IsActive = true,
                                Duration = 10,
                            }
                        );*/
        }
    }
}