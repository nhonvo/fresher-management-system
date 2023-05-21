using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class ClassAdminConfiguration : IEntityTypeConfiguration<ClassAdmin>
    {
        public void Configure(EntityTypeBuilder<ClassAdmin> builder)
        {
            builder.ToTable("ClassAdmin");
            builder.HasKey(x => x.Id);
            builder.HasOne(ca => ca.TrainingClass)
                   .WithMany(tc => tc.ClassAdmins)
                   .HasForeignKey(ca => ca.TrainingClassId);
            builder.HasOne(ca => ca.Admin)
                   .WithMany(tc => tc.ClassAdmins)
                   .HasForeignKey(ca => ca.AdminId);
            builder.HasOne(ca => ca.CreateByUser)
                   .WithMany(tc => tc.CreatedClassAdmin)
                   .HasForeignKey(ca => ca.CreatedBy);
        }
    }
}
