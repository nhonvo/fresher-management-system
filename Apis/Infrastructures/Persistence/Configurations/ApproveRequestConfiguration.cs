using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class ApproveRequestConfiguration : IEntityTypeConfiguration<ApproveRequest>
    {
        public void Configure(EntityTypeBuilder<ApproveRequest> builder)
        {
            builder.ToTable("ApproveRequests");

            //Id
            builder.HasKey(x => x.Id);

            // student
            builder.HasOne(x => x.Student)
                   .WithMany(x => x.ApproveRequests)
                   .HasForeignKey(x => x.StudentId);
            // admin
            builder.HasOne(x => x.Admin)
                   .WithMany(x => x.AdminApproveRequests)
                   .HasForeignKey(x => x.ApproveBy);
            // class
            builder.HasOne(x => x.TrainingClass)
                   .WithMany(x => x.ApproveRequests)
                   .HasForeignKey(x => x.ClassId);
        }
    }
}
