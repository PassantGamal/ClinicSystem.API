using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Domain.Entities
{
    public class DoctorSchedule : BaseEntity
    {
        public Guid DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int SlotDuration { get; set; }
        public bool IsAvailable { get; set; } = true;
        public virtual Doctor Doctor { get; set; } = null!;
    }
}
