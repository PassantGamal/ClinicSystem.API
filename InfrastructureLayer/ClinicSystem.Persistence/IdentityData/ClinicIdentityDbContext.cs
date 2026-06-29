using ClinicSystem.Domain.IdentityModules;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Persistence.IdentityData
{
    public class ClinicIdentityDbContext:IdentityDbContext<ApplicationUser>
    {
        public ClinicIdentityDbContext(DbContextOptions<ClinicIdentityDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");

            builder.Entity<IdentityRole>().HasData(
               new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
               new IdentityRole { Id = "2", Name = "Doctor", NormalizedName = "DOCTOR" },
               new IdentityRole { Id = "3", Name = "Receptionist", NormalizedName = "RECEPTIONIST" },
               new IdentityRole { Id = "4", Name = "Patient", NormalizedName = "PATIENT" }
           );

        }
    }
}
