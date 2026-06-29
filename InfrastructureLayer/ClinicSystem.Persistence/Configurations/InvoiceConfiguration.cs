using ClinicSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Persistence.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(i => i.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(i => i.PaidAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(i => i.PaymentStatus)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(i => i.InvoiceDate)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(i => i.PaymentDate)
                .IsRequired(false);

            builder.Property(i => i.PaymentMethod)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(i => i.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(i => i.UpdatedAt)
                .IsRequired(false);

            builder.Property(i => i.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasQueryFilter(i => !i.IsDeleted);

            builder.HasOne(i => i.Appointment)
                .WithOne(a => a.Invoice)
                .HasForeignKey<Invoice>(i => i.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Patient)
                .WithMany(p => p.Invoices)
                .HasForeignKey(i => i.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
