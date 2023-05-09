using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            builder.ToTable("Score");

            //Id
            builder.HasKey(x => x.Id);

            // Syllabus
            builder.HasOne(x => x.Syllabus)
                   .WithMany(x => x.Scores)
                   .HasForeignKey(x=>x.SyllabusId);

            // Attendee
            builder.HasOne(x => x.Attendee)
                   .WithMany(x => x.Scores)
                   .HasForeignKey(x=>x.AttendeeId);

        }
    }
}