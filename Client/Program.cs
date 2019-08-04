﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
			// discover endpoints from metadata
			var client = new HttpClient();
			var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
			if (disco.IsError)
			{
				Console.WriteLine(disco.Error);
				return;
			}

			// request token
			var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
			{
				Address = disco.TokenEndpoint,

				ClientId = "client",
				ClientSecret = "secret",
				Scope = "securedWebApi"
			});

			if (tokenResponse.IsError)
			{
				Console.WriteLine(tokenResponse.Error);
				return;
			}

			Console.WriteLine(tokenResponse.Json);

			client.SetBearerToken(tokenResponse.AccessToken);

			var response = await client.GetAsync("http://localhost:18080/job");
			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine(response.StatusCode);
			}
			else
			{
				var content = await response.Content.ReadAsStringAsync();
				Console.WriteLine(JArray.Parse(content));
			}
		}
    }
}
