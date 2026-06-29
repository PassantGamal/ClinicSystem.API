using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Shared.DataTransferObject.AppointmentDTOS
{
    public class AppointmentDTO
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; } = null!;
        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = null!;
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Status { get; set; } = null!;
        public string? Reason { get; set; }
        public string? Notes { get; set; }
    }
}
