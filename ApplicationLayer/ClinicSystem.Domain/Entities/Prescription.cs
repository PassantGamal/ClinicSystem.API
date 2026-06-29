using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Domain.Entities
{
    public class Prescription:BaseEntity
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public string Diagnosis { get; set; } = null!;
        public DateTime PrescriptionDate { get; set; }
        public string? Notes { get; set; }
        public Doctor Doctor { get; set; } = null!;
        public Patient Patient { get; set; } = null!;
        public ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; } = new List<PrescriptionMedicine>();
        
        
    }
}
