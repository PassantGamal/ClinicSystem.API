using ClinicSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Persistence.Data
{
    public class ClinicSystemDBContext:DbContext
    {
        public ClinicSystemDBContext(DbContextOptions<ClinicSystemDBContext> options) : base(options) { }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<PrescriptionMedicine> PrescriptionMedicines { get; set;  }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicSystemDBContext).Assembly);
           
        }

    }
}
