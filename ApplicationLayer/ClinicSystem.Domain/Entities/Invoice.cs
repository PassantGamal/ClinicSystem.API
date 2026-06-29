using ClinicSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public Guid AppointmentId { get; set; }
        public Guid PatientId { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public virtual Appointment Appointment { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
    }
}
