using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            builder.HasOne(x => x.TrainingProgram)
                   .WithMany(x => x.TestAssessments)
                   .HasForeignKey(x => x.TraningProgramId);

            // Attendee
            builder.HasOne(x => x.Attendee)
                   .WithMany(x => x.TestAssessments)
                   .HasForeignKey(x => x.AttendeeId);

        }
    }
}
