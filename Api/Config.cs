using System;
using Microsoft.Extensions.Configuration;

namespace Api
{
    public static class Config
    {
        private static IConfiguration _config;

        public static string IdentityServerUri { get; set; }
        public static string Audience { get; set; }

		public static void SetConfig(IConfiguration config)
        {
            _config = config;
            IdentityServerUri = config.GetSection("auth")["IdentityServerUri"];
            Audience = config.GetSection("auth")["Audience"];

            LogConfigurations();
		}

        public static void LogConfigurations()
        {
            Console.WriteLine($"auth.IdentityServerUri: {IdentityServerUri}");
            Console.WriteLine($"auth.Audience: {Audience}");
        }
    }
}
