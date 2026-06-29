using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string FullName { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public string? LicenseNumber { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Bio { get; set; }
        public decimal ConsultationFee { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new List<DoctorSchedule>();
    }
}