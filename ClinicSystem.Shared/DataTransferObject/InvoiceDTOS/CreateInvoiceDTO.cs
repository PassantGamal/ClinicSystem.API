using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Shared.DataTransferObject.InvoiceDTOS
{
    public class CreateInvoiceDTO
    {
        public Guid AppointmentId { get; set; }
        public Guid PatientId { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
