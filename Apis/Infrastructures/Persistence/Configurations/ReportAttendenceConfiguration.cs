using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class ReportAttendenceConfiguration : IEntityTypeConfiguration<ReportAttendance>
    {
        public void Configure(EntityTypeBuilder<ReportAttendance> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(st => st.Student)
                   .WithMany(rp => rp.ReportAttendence)
                   .HasForeignKey(ps => ps.StudentId);

        }
    }
}
