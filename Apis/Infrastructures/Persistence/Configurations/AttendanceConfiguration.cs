using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("Attendance");

            builder.HasKey(x => x.Id);

            builder.HasOne(st => st.ClassStudent)
                   .WithMany(rp => rp.Attendances)
                   .HasForeignKey(ps => ps.StudentId);

            builder.HasOne(x => x.Admin)
                   .WithMany(x => x.Attendances)
                   .HasForeignKey(x => x.AdminId);
        }
    }
}
