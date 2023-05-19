using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations;

public class ClassStudentConfiguration : IEntityTypeConfiguration<ClassStudent>
{
    public void Configure(EntityTypeBuilder<ClassStudent> builder)
    {
        builder.ToTable("ClassStudent");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.TrainingClass)
               .WithMany(tc => tc.Students)
               .HasForeignKey(ca => ca.TrainingClassId);
        builder.HasOne(ca => ca.Student)
               .WithMany(tc => tc.ClassStudents)
               .HasForeignKey(ca => ca.StudentId);
        builder.HasOne(ca => ca.CreateByUser)
               .WithMany(tc => tc.CreatedClassStudents)
               .HasForeignKey(ca => ca.CreatedBy);
        builder.HasMany(st => st.Attendances)
               .WithOne(rp => rp.ClassStudent)
               .HasForeignKey(ps => ps.StudentId);
    }
}
