using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable("Unit");
            builder.HasKey(x => x.Id);

            builder.HasOne(o => o.CreateByUser)
                  .WithMany(u => u.CreatedUnits)
                  .HasForeignKey(o => o.CreatedBy)
                  .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.ModificationByUser)
                  .WithMany(u => u.ModifiedUnits)
                  .HasForeignKey(o => o.ModificationBy)
                  .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Syllabus)
                   .WithMany(u => u.Units)
                   .HasForeignKey(o => o.SyllabusId);
        }
    }
}
