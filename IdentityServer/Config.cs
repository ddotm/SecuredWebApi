using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
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
						"Role",
						JwtClaimTypes.Email
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
					// If any prefix is specified, it is prepended to each claim name. Defaults to "client_"
					ClientClaimsPrefix = "",
					AlwaysSendClientClaims = true,

					// lifetime in seconds
					AccessTokenLifetime = 3600,
					AccessTokenType = AccessTokenType.Jwt,
					AllowAccessTokensViaBrowser = false,

					ClientName = "Super-secure API (name)",
					Description = "Super-secure API client"
				},
				// resource owner password grant client
				new Client
				{
					ClientId = "ResourceOwnerClientId",
					ClientName = "Resource Owner Password",
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

					ClientSecrets =
					{
						new Secret("super-secret-password".Sha256())
					},
					AllowedScopes =
					{
						"securedWebApiScope",
						OidcConstants.StandardScopes.OpenId,
						OidcConstants.StandardScopes.Profile
					},
					Claims = new List<Claim>
					{
						new Claim("role", "user"),
						new Claim("version", "1.2.3")
					},
					ClientClaimsPrefix = "",
					AlwaysSendClientClaims = true,
					AccessTokenType = AccessTokenType.Jwt,
					AlwaysIncludeUserClaimsInIdToken = true
				}
			};
		}

		public static List<TestUser> GetUsers()
		{
			return new List<TestUser>
			{
				new TestUser
				{
					SubjectId = "1",
					Username = "dmitri",
					Password = "password",
					Claims = new List<Claim>
					{
						new Claim(JwtClaimTypes.Email, "dmitri@someemail.com"),
						new Claim(JwtClaimTypes.EmailVerified, "true")
					}
				},
				new TestUser
				{
					SubjectId = "2",
					Username = "pete",
					Password = "password",
					Claims = new List<Claim>
					{
						new Claim(JwtClaimTypes.Email, "pete@someemail.com"),
						new Claim(JwtClaimTypes.EmailVerified, "true")
					}
				}
			};
		}
	}
}
