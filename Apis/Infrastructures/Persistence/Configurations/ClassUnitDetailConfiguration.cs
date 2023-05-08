using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class ClassUnitDetailConfiguration : IEntityTypeConfiguration<ClassUnitDetail>
    {
        public void Configure(EntityTypeBuilder<ClassUnitDetail> builder)
        {
            builder.ToTable("ClassUnitDetail");

            builder.HasKey(x => new
            {
                x.ClassId,
                x.UnitId
            });

            builder.HasOne(x => x.Class)
                .WithMany(x => x.ClassUnitDetails)
                .HasForeignKey(x => x.ClassId);

            builder.HasOne(x => x.Unit)
                .WithMany(x => x.ClassUnitDetails)
                .HasForeignKey(x => x.UnitId);

            builder.HasOne(x => x.Trainer)
                .WithMany(x => x.ClassUnitDetails)
                .HasForeignKey(x => x.TrainerId);
        }
    }
}
