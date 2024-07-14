using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class TestAssessmentConfiguration : IEntityTypeConfiguration<TestAssessment>
    {
        public void Configure(EntityTypeBuilder<TestAssessment> builder)
        {
            builder.ToTable("TestAssessments");

            //Id
            builder.HasKey(x => x.Id);

            // Syllabus
            builder.HasOne(x => x.Syllabus)
                   .WithMany(x => x.TestAssessments)
                   .HasForeignKey(x => x.SyllabusId);

            // TrainingClass
            builder.HasOne(x => x.TrainingClass)
                   .WithMany(x => x.TestAssessments)
                   .HasForeignKey(x => x.TrainingClassId);

            // Attendee
            builder.HasOne(x => x.Attendee)
                   .WithMany(x => x.TestAssessments)
                   .HasForeignKey(x => x.AttendeeId);

        }
    }
}
