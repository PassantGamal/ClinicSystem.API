using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Domain.Entities
{
    public class PrescriptionMedicine
    {
        public Guid PrescriptionId { get; set; }
        public Guid MedicineId { get; set; }
        public string Dosage { get; set; } = null!;
        public string Frequency { get; set; } = null!;
        public int DurationDays { get; set; }
        public string? Instructions { get; set; }
        public int Quantity { get; set; }
        public Prescription Prescription { get; set; } = null!;
        public Medicine Medicine { get; set; } = null!;


    }
}
