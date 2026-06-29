using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Shared.DataTransferObject.IdentityDTOS
{
    public record UserDTO(string Email, string DisplayName, string Token);
   
}
