using ClinicSystem.Shared.CommonResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClinicSystem.Presentation.Controllers
{
   
    [ApiController]
    [Route("api/[Controller]")]
    public abstract class ApiBaseController : ControllerBase
    {
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(result.Errors);
        }
        protected ActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
                return NoContent();

            return BadRequest(result.Errors);
        }
    }
}