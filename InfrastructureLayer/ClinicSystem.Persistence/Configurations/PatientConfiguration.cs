using ClinicSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Persistence.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(p => p.FullName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.DateOfBirth)
                .IsRequired(false);

            builder.Property(p => p.Gender)
                .HasMaxLength(10)
                .IsRequired(false);

            builder.Property(p => p.BloodType)
                .HasMaxLength(5)
                .IsRequired(false);

            builder.Property(p => p.Phone)
                .HasMaxLength(15)
                .IsRequired(false);

            builder.Property(p => p.Email)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.HasIndex(p => p.Email);

            builder.Property(p => p.Address)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(p => p.EmergencyContact)
                .HasMaxLength(15)
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

            
        }
    }
}