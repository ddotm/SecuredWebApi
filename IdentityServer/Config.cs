using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
	public static class Config
	{
		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new IdentityResource[]
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResources.Email(),
				new IdentityResources.Address(),
				new IdentityResources.Phone()
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
						new Scope("securedWebApiScope")
					},
					Description = "This is a super-secure API resource",
					Enabled = true,
					UserClaims = new List<string>
					{
						"Role"
					},
					Properties = new Dictionary<string, string>
					{
						{"prop1", "prop1Value"}
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
					ClientId = "securedWebApiClientId",
					// secret for authentication
					ClientSecrets =
					{
						new Secret("super-secret-password".Sha256())
					},
					// no interactive user, use the clientid/secret for authentication
					AllowedGrantTypes = GrantTypes.ClientCredentials,
					Enabled = true,

					// scopes that client has access to
					AllowedScopes = {"securedWebApiScope"},
					Claims = new List<Claim>
					{
						new Claim("role", "secure_api"),
						new Claim("version", "1.2.3")
					},
					// If any prefix is specified, it is prepended to each claim name. Defaults to client_
					ClientClaimsPrefix = "",
					AlwaysSendClientClaims = true,

					// lifetime in seconds
					AccessTokenLifetime = 3600,
					AccessTokenType = AccessTokenType.Jwt,
					AllowAccessTokensViaBrowser = false,

					ClientName = "Super-secure API (name)",
					Description = "Super-secure API client"
				}
			};
		}
	}
}
