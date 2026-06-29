using ClinicSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Persistence.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(d => d.FullName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(d => d.Specialization)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(d => d.LicenseNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(d => d.LicenseNumber)
                .IsUnique();

            builder.Property(d => d.Phone)
                .HasMaxLength(15)
                .IsRequired(false);

            builder.Property(d => d.Email)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(d => d.ConsultationFee)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(d => d.Bio)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(d => d.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(d => d.UpdatedAt)
                .IsRequired(false);

            builder.Property(d => d.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasQueryFilter(d => !d.IsDeleted);
          
            builder.HasMany(d => d.DoctorSchedules)
           .WithOne(ds => ds.Doctor)
          .HasForeignKey(ds => ds.DoctorId)
          .OnDelete(DeleteBehavior.Restrict);


        }
    }
}