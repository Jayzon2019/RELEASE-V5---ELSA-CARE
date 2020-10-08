using System.Security.Claims;
using System.Collections.Generic;

using IdentityServer4;
using IdentityServer4.Models;
using IdentityModel;
using System;
using System.Security.Cryptography;
using System.Text;

namespace InLife.Store.Identity
{
	public class Config
	{
		public static Dictionary<string, object> GetCustomEndpoints()
		{
			return new Dictionary<string, object>
			{
				{"registration_endpoint",   "~/connect/register"},
				{"changepassword_endpoint", "~/connect/change-password"},
				{"resetpassword_endpoint",  "~/connect/reset-password"}
			};
		}



		// Identity resources (used by UserInfo endpoint)
		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResources.Email(),
				new IdentityResource("role", new List<string> { "role" })
			};
		}



		// Api resources
		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
				//new ApiResource
				//{
				//	Name = IdentityServerConstants.LocalApi.ScopeName,
				//	Scopes =
				//	{
				//		IdentityServerConstants.LocalApi.ScopeName
				//	}
				//}
			};
		}



		// Clients want to access resources (aka scopes)
		public static IEnumerable<Client> GetClients()
		{
			// Lazy tool to convert secrets to Sha256 Base64 string
			var secret = "InLife.Store-AC9A8C5D-15A6-4813-8D3D-A89FAC591DEC".Sha256();

			// client credentials client
			return new List<Client>
			{
				new Client
				{
					ClientId = "inlife.store.cms",
					ClientName = "InLife Store CMS",
					ClientSecrets = { new Secret("InLife.Store-AC9A8C5D-15A6-4813-8D3D-A89FAC591DEC".Sha256()) },

					AllowedGrantTypes = GrantTypes.Code,
					AllowedScopes = { "openid", "profile" },

					RequirePkce = true,
					AllowPlainTextPkce = false,

					RedirectUris = { "https://localhost:8100/signin-oidc" },
					PostLogoutRedirectUris = { "https://localhost:8100/signout-callback-oidc" },
					FrontChannelLogoutUri = "https://localhost:8100/signout-oidc"
				}
			};
		}
	}
}
