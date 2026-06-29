using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Shared.DataTransferObject.MedicalHistoryDTOS
{
    public class MedicalHistoryDTO
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string? Allergies { get; set; }
        public string? ChronicConditions { get; set; }
        public string? PreviousSurgeries { get; set; }
        public string? CurrentMedications { get; set; }
        public string? FamilyHistory { get; set; }
        public string? Notes { get; set; }
    }
}
