using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
    }
}
