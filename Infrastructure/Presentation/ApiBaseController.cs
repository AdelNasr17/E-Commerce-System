

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation
{
    [ApiController]
    [Route("api/[Controller]")]
    public abstract class ApiBaseController:ControllerBase
    {
        protected  string GetEmailFromToken() =>   User.FindFirstValue(ClaimTypes.Email)!;

    }
}
