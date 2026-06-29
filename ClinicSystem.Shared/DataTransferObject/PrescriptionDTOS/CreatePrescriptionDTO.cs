using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Shared.DataTransferObject.PrescriptionDTOS
{
    public class CreatePrescriptionDTO
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public string Diagnosis { get; set; } = null!;
        public string? Notes { get; set; }
        public List<AddPrescriptionMedicineDTO> Medicines { get; set; } = new();
    }
}
