using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class ProgramSyllabusConfiguration : IEntityTypeConfiguration<ProgramSyllabus>
    {
        public void Configure(EntityTypeBuilder<ProgramSyllabus> builder)
        {
            builder.ToTable("ProgramSyllabus");
            builder.HasKey(x => x.Id);

            builder.HasOne(ps => ps.TrainingProgram)
                   .WithMany(tp => tp.ProgramSyllabus)
                   .HasForeignKey(ps => ps.TrainingProgramId)
                   .HasPrincipalKey(tp => tp.Id);

            builder.HasOne(ps => ps.Syllabus)
                   .WithMany(s => s.ProgramSyllabus)
                   .HasForeignKey(ps => ps.SyllabusId)
                   .HasPrincipalKey(s => s.Id);
        }
    }
}
