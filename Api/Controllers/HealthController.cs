using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Produces("application/json")]
	[Route("api/health")]
    [AllowAnonymous]
    public class HealthController : ControllerBase
    {
	    [ProducesResponseType(200)]
		[HttpGet("status")]
        public async Task<ActionResult<string>> HealthAsync()
        {
            return Ok("API is OK");
        }

    }
}