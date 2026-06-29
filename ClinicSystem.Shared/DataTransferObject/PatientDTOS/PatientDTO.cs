using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Shared.DataTransferObject.PatientDTOS
{
    public class PatientDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? BloodType { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? EmergencyContact { get; set; }
    }
}
