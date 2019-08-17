using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("health")]
    [AllowAnonymous]
    public class HealthController : ControllerBase
    {
        [HttpGet("status")]
        public async Task<ActionResult<string>> HealthAsync()
        {
            return Ok("API is OK");
        }

    }
}