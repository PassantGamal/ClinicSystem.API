using ClinicSystem.Domain.IdentityModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.ServicesAbstration.Contracts.Services
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}
