using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Shared.DataTransferObject.MedicineDTOS
{
    public class MedicineDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Manufacturer { get; set; }
        public string? DosageForm { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
