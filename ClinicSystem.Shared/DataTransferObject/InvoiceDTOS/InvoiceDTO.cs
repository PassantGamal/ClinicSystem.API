using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Shared.DataTransferObject.InvoiceDTOS
{
    public class InvoiceDTO
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = null!;
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public string PaymentStatus { get; set; } = null!;
        public DateTime InvoiceDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
