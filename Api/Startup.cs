using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Api
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public IHostingEnvironment HostingEnvironment { get; }

		public Startup(IHostingEnvironment env, IConfiguration configuration)
		{
			HostingEnvironment = env;

			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
			Config.SetConfig(Configuration);
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvcCore()
				.AddAuthorization()
				.AddJsonFormatters()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddAuthentication("Bearer")
				.AddJwtBearer("Bearer", options =>
				{
					options.Authority = Config.IdentityServerUri;
					options.RequireHttpsMetadata = false;
					options.Audience = Config.Audience;
				});

			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddMvc();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Lilipudra API",
					Version = "v1",
					Description = "",
					TermsOfService = new Uri("https://example.com/terms"),
					Contact = new OpenApiContact
					{
						Name = "Dmitri Mogilevski",
						Email = string.Empty,
						Url = new Uri("https://twitter.com/ddotm"),
					},
					License = new OpenApiLicense
					{
						Name = "Use under MIT",
						Url = new Uri("https://opensource.org/licenses/MIT"),
					}
				});
				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});


			// DI
			// Application
			services.AddSingleton(HostingEnvironment);
			services.AddSingleton(Configuration);
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseAuthentication();

			app.UseMvc();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lilipudra API");
			});
		}
	}
}
