using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClinicSystem.Shared.DataTransferObject.IdentityDTOS
{
    public record LoginDTO([EmailAddress]string Email, string Password);
}
