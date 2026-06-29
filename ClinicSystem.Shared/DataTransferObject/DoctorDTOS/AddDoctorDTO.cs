using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Shared.DataTransferObject.DoctorDTOS
{
    public class  AddDoctorDTO
    {
        public string FullName { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public string LicenseNumber { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Bio { get; set; }
        public decimal ConsultationFee { get; set; }
    }
}
