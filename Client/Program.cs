using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client
{
	internal class Program
	{
		private static readonly string IdentityServerUri = "https://localhost:44372";
		private static readonly string ApiUri = "https://localhost:44349";

		private static async Task Main(string[] args)
		{
			using (var client = await CreateAuthenticatedClient())
			{
				if (client == null)
				{
					return;
				}

				await CallSecureApi(client);
			}
		}

		private static async Task<HttpClient> CreateAuthenticatedClient()
		{
			// discover endpoints from metadata
			var client = new HttpClient();
			var disco = await client.GetDiscoveryDocumentAsync(IdentityServerUri);
			if (disco.IsError)
			{
				Console.WriteLine(disco.Error);
				return null;
			}

			// request token
			var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
			{
				Address = disco.TokenEndpoint,
				ClientId = "securedWebApiClientId",
				ClientSecret = "super-secret-password",
				Scope = "securedWebApiScope"
			});

			if (tokenResponse.IsError)
			{
				Console.WriteLine(tokenResponse.Error);
				return null;
			}

			Console.WriteLine(tokenResponse.Json);

			client.SetBearerToken(tokenResponse.AccessToken);
			return client;
		}

		private static async Task CallSecureApi(HttpClient client)
		{
			var response = await client.GetAsync($"{ApiUri}/job");
			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine(response.StatusCode);
			}
			else
			{
				var content = await response.Content.ReadAsStringAsync();
				Console.WriteLine(JArray.Parse(content));
			}

			response.Dispose();
		}
	}
}
