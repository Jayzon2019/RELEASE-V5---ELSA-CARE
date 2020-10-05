using System.Security.Claims;
using System.Collections.Generic;

using IdentityServer4;
using IdentityServer4.Models;

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
			// client credentials client
			return new List<Client>
			{

			};
		}
	}
}
