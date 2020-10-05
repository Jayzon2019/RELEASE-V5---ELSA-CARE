// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace InLife.Store.Identity.Features
{
	public class TestUsers
	{
		public static List<TestUser> Users = new List<TestUser>
		{
			new TestUser{SubjectId = "11111111", Username = "test.user.01", Password = "P@ssw0rd",
				Claims =
				{
					new Claim(JwtClaimTypes.Name, "Test User 01"),
					new Claim(JwtClaimTypes.GivenName, "Test User"),
					new Claim(JwtClaimTypes.FamilyName, "01"),
					new Claim(JwtClaimTypes.Email, "test.user.01@email.com"),
					new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
					new Claim(JwtClaimTypes.WebSite, "http://test-user-01.com"),
					new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'Street', 'locality': 'Locality', 'postal_code': '1111', 'country': 'Country' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
				}
			},
			new TestUser{SubjectId = "22222222", Username = "test.user.02", Password = "P@ssw0rd",
				Claims =
				{
					new Claim(JwtClaimTypes.Name, "Test User 02"),
					new Claim(JwtClaimTypes.GivenName, "Test User"),
					new Claim(JwtClaimTypes.FamilyName, "02"),
					new Claim(JwtClaimTypes.Email, "test.user.02@email.com"),
					new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
					new Claim(JwtClaimTypes.WebSite, "http://test-user-02.com"),
					new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'Street', 'locality': 'Locality', 'postal_code': '2222', 'country': 'Country' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
				}
			}
		};
	}
}