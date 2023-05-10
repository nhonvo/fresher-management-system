using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class UnitClassDetailConfiguration : IEntityTypeConfiguration<UnitClassDetail>
    {
        public void Configure(EntityTypeBuilder<UnitClassDetail> builder)
        {
            builder.HasKey(x => new { x.ClassId, x.UnitId, x.TrainerId });
            builder.HasOne(o => o.TrainingClass)
                   .WithMany(u => u.UnitClassDetail)
                   .HasForeignKey(o => o.ClassId);
            builder.HasOne(o => o.Unit)
                   .WithMany(u => u.UnitClassDetails)
                   .HasForeignKey(o => o.UnitId);
            builder.HasOne(o => o.Trainer)
                   .WithMany(u => u.UnitTrainers)
                   .HasForeignKey(o => o.TrainerId);
        }
    }
}
