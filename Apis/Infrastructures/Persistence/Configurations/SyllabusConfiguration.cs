using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class SyllabusConfiguration : IEntityTypeConfiguration<Syllabus>
    {
        public void Configure(EntityTypeBuilder<Syllabus> builder)
        {
            builder.ToTable("Syllabuses");
            builder.HasKey(t => t.Id);
            builder.HasOne(o => o.CreateByUser)
                  .WithMany(u => u.CreatedSyllabuses)
                  .HasForeignKey(o => o.CreatedBy).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.ModificationByUser)
                  .WithMany(u => u.ModifiedSyllabuses)
                  .HasForeignKey(o => o.ModificationBy).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
