using ClinicSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Persistence.Configurations
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.ToTable("Medicines");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(m => m.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(m => m.Name)
                .IsUnique();

            builder.Property(m => m.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(m => m.Category)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(m => m.Manufacturer)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(m => m.DosageForm)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(m => m.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(m => m.UpdatedAt)
                .IsRequired(false);

            builder.Property(m => m.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasQueryFilter(m => !m.IsDeleted);
        }
    }
}