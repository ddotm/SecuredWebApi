using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public IHostingEnvironment HostingEnvironment { get; }

		public Startup(IHostingEnvironment environment, IConfiguration configuration)
		{
			HostingEnvironment = environment;

			var builder = new ConfigurationBuilder()
				.SetBasePath(HostingEnvironment.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{HostingEnvironment.EnvironmentName}.json", optional: true, true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			// uncomment, if you want to add an MVC-based UI
			//services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

			var builder = services.AddIdentityServer()
				.AddInMemoryIdentityResources(Config.GetIdentityResources())
				.AddInMemoryApiResources(Config.GetApis())
				.AddInMemoryClients(Config.GetClients());

			// Dev RSA key provided by IdentityServer4
			// if (HostingEnvironment.IsDevelopment() || HostingEnvironment.IsEnvironment("Local"))
			// {
			// 	
			// 	builder.AddDeveloperSigningCredential();
			// }
			// else
			// {
			// 	throw new Exception("need to configure key material");
			// }

			// In-memory RSA key creation and assignment to IdentityServer
			var rsaKey = Crypto.GenerateRsaKey();
			builder.AddSigningCredential(rsaKey);

			// DI
			// Application
			services.AddSingleton(HostingEnvironment);
			services.AddSingleton(Configuration);
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		}

		public void Configure(IApplicationBuilder app)
		{
			if (HostingEnvironment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			// uncomment if you want to support static files
			//app.UseStaticFiles();

			app.UseIdentityServer();

			// uncomment, if you want to add an MVC-based UI
			//app.UseMvcWithDefaultRoute();
		}
	}
}
