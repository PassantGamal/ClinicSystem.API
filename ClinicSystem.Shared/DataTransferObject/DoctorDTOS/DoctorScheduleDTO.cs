using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Shared.DataTransferObject.DoctorDTOS
{
    public class DoctorScheduleDTO
    {
        public Guid Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int SlotDuration { get; set; }
        public bool IsAvailable { get; set; }
    }
}
