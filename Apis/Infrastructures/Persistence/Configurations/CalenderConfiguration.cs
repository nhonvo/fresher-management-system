
using System.Globalization;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations;

public class CalenderConfiguration : IEntityTypeConfiguration<Calender>
{
    public void Configure(EntityTypeBuilder<Calender> builder)
    {
        builder.ToTable("Calender", x =>
        {
            x.HasCheckConstraint("CK_Calender_Order", "[Order] > 0");
            x.HasCheckConstraint("CK_Calender_Count", "[Count] >= 0 AND [Count] >= [Order]");
        });
        builder
            .HasKey(x => x.Id)
            .HasName("PK_Calender_Id");
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder
            .HasIndex(x => x.TrainingClassId, "IX_Calender_TrainingClassId");
        builder
            .HasIndex(x => x.Date, "IX_Calender_Date");
        builder
            .Property(x => x.Date)
            .HasColumnType("date")
            .HasDefaultValueSql("GETDATE()");
        builder
            .Property(x => x.Order)
            .HasDefaultValue(1);
        builder
            .Property(x => x.Count)
            .HasDefaultValue(1);
        builder
            .Property(x => x.IsDeleted)
            .HasDefaultValue(false);
        // ref
        builder
            .HasOne(x => x.TrainingClass)
            .WithMany(x => x.Calenders)
            .HasForeignKey(x => x.TrainingClassId)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_Calender_TrainingClassId");
        builder
            .HasOne(x => x.DeletedByUser)
            .WithMany(x => x.DeletedCalenders)
            .HasForeignKey(x => x.DeleteBy)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_Calender_DeleteBy");
    }
}
