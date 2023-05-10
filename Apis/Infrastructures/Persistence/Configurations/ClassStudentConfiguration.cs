using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations;

public class ClassStudentConfiguration : IEntityTypeConfiguration<ClassStudent>
{
    public void Configure(EntityTypeBuilder<ClassStudent> builder)
    {
        builder.HasKey(x => new { x.TrainingClassId, x.UserId });
        builder.HasOne(x => x.TrainingClass)
               .WithMany(tc => tc.Students)
               .HasForeignKey(ca => ca.TrainingClassId);
        builder.HasOne(ca => ca.User)
               .WithMany(tc => tc.ClassStudents)
               .HasForeignKey(ca => ca.UserId);
        builder.HasOne(ca => ca.CreateByUser)
            .WithMany(tc => tc.CreatedClassStudents)
            .HasForeignKey(ca => ca.CreatedBy);
    }
}
