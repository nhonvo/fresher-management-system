using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            //builder.HasOne(o => o.CreatedByUser)
            //       .WithMany(u => u.CreatedUnits)
            //       .HasForeignKey(o => o.CreatedByUserId)
            //       .OnDelete(DeleteBehavior.NoAction);
            //builder.HasOne(o => o.ModifiedByUser)
            //       .WithMany(u => u.ModifiedUnits)
            //       .HasForeignKey(o => o.ModifiedByUserId)
            //       .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Syllabus)
                   .WithMany(u => u.Units)
                   .HasForeignKey(o => o.SyllabusId);
        }
    }
}
