using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Shared.DataTransferObject.PrescriptionDTOS
{
    public class AddPrescriptionMedicineDTO
    {
        public Guid MedicineId { get; set; }
        public string Dosage { get; set; } = null!;
        public string Frequency { get; set; } = null!;
        public int DurationDays { get; set; }
        public int Quantity { get; set; }
        public string? Instructions { get; set; }
    }
}
