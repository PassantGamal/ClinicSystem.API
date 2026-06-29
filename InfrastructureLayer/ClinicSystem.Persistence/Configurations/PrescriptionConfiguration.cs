using ClinicSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Persistence.Configurations
{
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("Prescriptions");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(p => p.Diagnosis)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(p => p.PrescriptionDate)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(p => p.Notes)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(p => p.UpdatedAt)
                .IsRequired(false);

            builder.Property(p => p.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasOne(p => p.Doctor)
           .WithMany(d => d.Prescriptions)
           .HasForeignKey(p => p.DoctorId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Patient)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}