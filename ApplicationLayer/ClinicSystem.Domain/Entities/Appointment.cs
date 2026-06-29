using ClinicSystem.Domain.Enums;
using System;

namespace ClinicSystem.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public string? Reason { get; set; }
        public string? Notes { get; set; }
        public Doctor Doctor { get; set; } = null!;
        public Patient Patient { get; set; } = null!;
        public virtual Invoice? Invoice { get; set; }
    }
}