using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? BloodType { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? EmergencyContact { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public MedicalHistory? MedicalHistory { get; set; }
    }
}