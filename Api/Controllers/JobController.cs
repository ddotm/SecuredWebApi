using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
	[Route("job")]
	[Authorize]
	public class JobController : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult> Get()
		{
			return new JsonResult(from c in User.Claims select new {c.Type, c.Value});
		}
	}
}
