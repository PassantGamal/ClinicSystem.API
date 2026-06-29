using ClinicSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Domain.IdentityModules
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = default!;
        public Guid? DoctorId { get; set; } 
        public Guid? PatientId { get; set; }
       
    }
}
