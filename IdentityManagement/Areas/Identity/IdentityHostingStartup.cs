//using IdentityManagement.Areas.Identity.Data;
//using IdentityManagement.Data;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//
//[assembly: HostingStartup(typeof(IdentityManagement.Areas.Identity.IdentityHostingStartup))]
//namespace IdentityManagement.Areas.Identity
//{
//	public class IdentityHostingStartup : IHostingStartup
//    {
//        public void Configure(IWebHostBuilder builder)
//        {
//            builder.ConfigureServices((context, services) => {
//                services.AddDbContext<ApplicationDbContext>(options =>
//                    options.UseSqlServer(
//                        context.Configuration.GetConnectionString("DefaultConnection")));
//
//                services.AddDefaultIdentity<ApplicationUser>()
//	                .AddDefaultUI(UIFramework.Bootstrap4)
//					.AddEntityFrameworkStores<ApplicationDbContext>();
//            });
//        }
//    }
//}