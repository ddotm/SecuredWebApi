using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
	public static class Config
	{
		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new IdentityResource[]
			{
				new IdentityResources.OpenId()
			};
		}

		public static IEnumerable<ApiResource> GetApis()
		{
			return new List<ApiResource>
			{
				new ApiResource
				{
					Name = "securedWebApi",
					DisplayName = "Secured Web API",
					Scopes = new List<Scope>
					{
						new Scope("securedWebApi")
					}
				}
			};
		}

		public static IEnumerable<Client> GetClients()
		{
			return new List<Client>
			{
				new Client
				{
					ClientId = "client",
					// secret for authentication
					ClientSecrets =
					{
						new Secret("secret".Sha256())
					},

					// no interactive user, use the clientid/secret for authentication
					AllowedGrantTypes = GrantTypes.ClientCredentials,

					// scopes that client has access to
					AllowedScopes = {"securedWebApi" }
				}
			};
		}
	}
}
