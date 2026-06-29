using ClinicSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Persistence.Configurations
{
    public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
    {
        public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
        {
            builder.ToTable("DoctorSchedules");
            builder.HasKey(ds => ds.Id);

            builder.Property(ds => ds.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(ds => ds.DayOfWeek)
                .IsRequired();

            builder.Property(ds => ds.StartTime)
                .IsRequired();

            builder.Property(ds => ds.EndTime)
                .IsRequired();

            builder.Property(ds => ds.SlotDuration)
                .IsRequired();

            builder.Property(ds => ds.IsAvailable)
                .HasDefaultValue(true);

            builder.Property(ds => ds.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(ds => ds.UpdatedAt)
                .IsRequired(false);

            builder.Property(ds => ds.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasQueryFilter(ds => !ds.IsDeleted);

            builder.HasOne(ds => ds.Doctor)
                .WithMany(d => d.DoctorSchedules)
                .HasForeignKey(ds => ds.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
