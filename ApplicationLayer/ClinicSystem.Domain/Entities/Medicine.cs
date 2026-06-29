using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Domain.Entities
{
    public class Medicine:BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Manufacturer { get; set; }
        public string? DosageForm { get; set; }
        public ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; } = new List<PrescriptionMedicine>();
    }
}
