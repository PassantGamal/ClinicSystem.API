using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Domain.Enums
{
    public enum PaymentStatus
    {
        Pending = 1,
        Paid = 2,
        Partial = 3,
        Overdue = 4
    }
}
