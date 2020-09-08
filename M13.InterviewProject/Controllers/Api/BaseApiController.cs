using System.Threading;
using M13.Domain.Constants;
using M13.Domain.Conventions;
using Microsoft.AspNetCore.Mvc;

namespace M13.InterviewProject.Controllers.Api
{
    [ApiController]
    [Route(ApiRouteConstants.DefaultApiController)]
    public abstract class BaseApiController : ControllerBase
    {
        protected CancellationToken _cancellationToken => HttpContext.RequestAborted;

        protected IActionResult ResultFromService(ServiceResult serviceResult)
        {
            if (serviceResult.IsOk)
            {
                return Ok();
            }

            return BadRequest(serviceResult.ErrorMessage);
        }
    }
}