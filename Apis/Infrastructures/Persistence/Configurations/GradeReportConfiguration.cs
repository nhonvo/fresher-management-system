using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class GradeReportConfiguration : IEntityTypeConfiguration<GradeReport>
    {
        public void Configure(EntityTypeBuilder<GradeReport> builder)
        {
            builder.ToTable("GradeReports");

            //Id
            builder.HasKey(x => x.Id);

            //Grade
            builder.Property(x => x.Grade).IsRequired();

            //Lecture
            builder.HasOne(x => x.Lecture)
                .WithMany(x => x.GradeReports)
                .HasForeignKey(x => x.LectureId);

            //Trainee
            builder.HasOne(x => x.User)
                .WithMany(x => x.GradeReports)
                .HasForeignKey(x => x.TraineeId);
        }
    }
}