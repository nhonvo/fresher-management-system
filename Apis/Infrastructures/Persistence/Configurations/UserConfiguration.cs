using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Email).HasMaxLength(100);
            
            builder.Property(x => x.IsShowTipCreatingClass)
                   .HasDefaultValue(true);
            
            builder.HasMany(x => x.DeletedCalenders)
                   .WithOne(x => x.DeletedByUser)
                   .HasForeignKey(x => x.DeleteBy)
                   .HasPrincipalKey(x => x.Id)
                   .OnDelete(DeleteBehavior.ClientCascade);
            
            builder.HasMany(x => x.Attendances)
                   .WithOne(x => x.Admin)
                   .HasForeignKey(x => x.AdminId);
        }
    }
}
