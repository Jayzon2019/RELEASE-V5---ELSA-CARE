// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Models;

using InLife.Store.Identity.Models;


namespace InLife.Store.Identity.Services
{
	public class ProfileService : IProfileService
	{
		private readonly IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory;
		private readonly UserManager<ApplicationUser> userManager;

		public ProfileService
		(
			UserManager<ApplicationUser> userManager,
			IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory
		)
		{
			this.claimsFactory = claimsFactory;
			this.userManager = userManager;
		}

		public async Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			var sub = context.Subject.GetSubjectId();
			var user = await userManager.FindByIdAsync(sub);
			var principal = await claimsFactory.CreateAsync(user);
			var hasPassword = await userManager.HasPasswordAsync(user);

			var claims = principal.Claims.ToList();
			claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
			//claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
			//claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
			claims.Remove(claims.Find(claim => claim.Type == JwtClaimTypes.Name));
			claims.Add(new Claim(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}".Trim()));
			claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, user.Email ?? String.Empty));
			claims.Add(new Claim(IdentityServerConstants.StandardScopes.Phone, user.PhoneNumber ?? String.Empty));
			claims.Add(new Claim("has_password", hasPassword.ToString()));

			context.IssuedClaims = claims;
		}

		public async Task IsActiveAsync(IsActiveContext context)
		{
			var sub = context.Subject.GetSubjectId();
			var user = await userManager.FindByIdAsync(sub);
			context.IsActive = user != null;
		}
	}
}
