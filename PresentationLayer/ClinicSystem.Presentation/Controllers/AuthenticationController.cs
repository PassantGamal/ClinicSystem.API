using ClinicSystem.ServicesAbstration.Contracts.Services;
using ClinicSystem.Shared.DataTransferObject.IdentityDTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSystem.Presentation.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var Result = await _authenticationService.LoginAsync(loginDTO);
            return HandleResult(Result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var Result = await _authenticationService.RegisterAsync(registerDTO);
            return HandleResult(Result);
        }
    }
}