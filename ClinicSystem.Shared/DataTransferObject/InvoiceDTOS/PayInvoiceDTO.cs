using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Shared.DataTransferObject.InvoiceDTOS
{
    public class PayInvoiceDTO
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = null!;
    }
}
