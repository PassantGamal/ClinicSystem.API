using ClinicSystem.Shared.CommonResult;
using ClinicSystem.Shared.DataTransferObject.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.ServicesAbstration.Contracts.Services
{
    public interface IAuthenticationService
    {
        //login
        //Email ,Password =>token , DisplayName , Email
      
        Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO);

        //Regisrer
        //Email ,Password ,DisplayName ,PhoneNumber =>token , DisplayName , Email
        Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO);
    }
}
