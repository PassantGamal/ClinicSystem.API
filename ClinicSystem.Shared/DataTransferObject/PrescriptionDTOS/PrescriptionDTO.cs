using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Shared.DataTransferObject.PrescriptionDTOS
{
    public class PrescriptionDTO
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; } = null!;
        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = null!;
        public string Diagnosis { get; set; } = null!;
        public DateTime PrescriptionDate { get; set; }
        public string? Notes { get; set; }
        public List<PrescriptionMedicineDTO> Medicines { get; set; } = new();
    }
}
