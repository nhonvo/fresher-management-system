using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class FSUConfiguration : IEntityTypeConfiguration<FSU>
    {
        public void Configure(EntityTypeBuilder<FSU> builder)
        {
        }
    }
}
