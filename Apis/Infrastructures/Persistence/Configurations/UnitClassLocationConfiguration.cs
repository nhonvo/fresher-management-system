using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class UnitClassLocationConfiguration : IEntityTypeConfiguration<UnitClassLocation>
    {
        public void Configure(EntityTypeBuilder<UnitClassLocation> builder)
        {
        }
    }
}
