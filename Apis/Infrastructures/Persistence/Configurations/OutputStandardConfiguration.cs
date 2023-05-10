using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class OutputStandardConfiguration : IEntityTypeConfiguration<OutputStandard>
    {
        public void Configure(EntityTypeBuilder<OutputStandard> builder)
        {
        }
    }
}
