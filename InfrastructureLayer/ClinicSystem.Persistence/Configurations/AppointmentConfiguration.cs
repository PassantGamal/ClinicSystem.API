using ClinicSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Persistence.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(a => a.AppointmentDate)
                .IsRequired();

            builder.Property(a => a.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(a => a.Reason)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(a => a.Notes)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(a => a.UpdatedAt);
                

            builder.Property(a => a.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasQueryFilter(a => !a.IsDeleted);

            builder.HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.Invoice)
                   .WithOne(i => i.Appointment)
                   .HasForeignKey<Invoice>(i => i.AppointmentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}