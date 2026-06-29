using ClinicSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Persistence.Configurations
{
    public class PrescriptionMedicineConfiguration : IEntityTypeConfiguration<PrescriptionMedicine>
    {
        public void Configure(EntityTypeBuilder<PrescriptionMedicine> builder)
        {
            builder.ToTable("PrescriptionMedicines")
                .HasKey(pm => new { pm.PrescriptionId, pm.MedicineId });
            builder.Property(pm=> pm.Dosage)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(pm => pm.Frequency)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(pm => pm.DurationDays)
                .IsRequired();
            builder.HasOne(pm => pm.Prescription)
                .WithMany(p => p.PrescriptionMedicines)
                .HasForeignKey(pm => pm.PrescriptionId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(pm => pm.Medicine)
                .WithMany(m => m.PrescriptionMedicines)
                .HasForeignKey(pm => pm.MedicineId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
    }
