using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Shared.DataTransferObject.AppointmentDTOS
{
    public class BookAppointmentDTO
    {
        
            public string DoctorEmail { get; set; }   
            public string PatientEmail { get; set; }   
            public DateTime AppointmentDate { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public string? Reason { get; set; }
            public string? Notes { get; set; }
        
    }
}
