using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Persistence.Configurations
{
    public class ProgramSyllabusConfiguration : IEntityTypeConfiguration<ProgramSyllabus>
    {
        public void Configure(EntityTypeBuilder<ProgramSyllabus> builder)
        {
            builder.HasOne(x => x.TrainingProgram)
                .WithMany(x => x.ProgramSyllabuses)
                .HasForeignKey(x => x.TrainingProgramId);
            builder.HasOne(x => x.Syllabus)
                .WithMany(x => x.ProgramSyllabuses)
                .HasForeignKey(x => x.SyllabusId);
        }
    }
}
