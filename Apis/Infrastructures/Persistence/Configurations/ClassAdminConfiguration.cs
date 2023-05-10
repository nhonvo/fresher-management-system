﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Persistence.Configurations
{
    public class ClassAdminConfiguration : IEntityTypeConfiguration<ClassAdmin>
    {
        public void Configure(EntityTypeBuilder<ClassAdmin> builder)
        {
            builder.HasKey(x => new { x.TrainingClassId, x.UserId });
            builder.HasOne(ca => ca.TrainingClass)
                   .WithMany(tc => tc.Admin)
                   .HasForeignKey(ca => ca.TrainingClassId);
            builder.HasOne(ca => ca.User)
                   .WithMany(tc => tc.ClassAdmins)
                   .HasForeignKey(ca => ca.UserId);
            builder.HasOne(ca => ca.CreateByUser)
                   .WithMany(tc => tc.CreatedClassAdmin)
                   .HasForeignKey(ca => ca.CreatedBy);
        }
    }
}