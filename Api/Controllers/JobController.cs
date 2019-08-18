using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers
{
	[Produces("application/json")]
	[Route("api/job")]
	[Authorize]
	public class JobController : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult> Get()
		{
			return new JsonResult(from c in User.Claims select new {c.Type, c.Value});
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> Get([FromRoute] int id)
		{
			var randomizer = new Random();
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			var runFor = TimeSpan.FromMinutes(10);
			while (stopwatch.Elapsed < runFor)
			{
				var random = randomizer.Next();
				Thread.Sleep(1000);
			}
			
			return new JsonResult(from c in User.Claims select new {c.Type, c.Value});
		}
	}
}
