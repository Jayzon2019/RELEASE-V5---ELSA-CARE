using System;
using Microsoft.AspNetCore.Identity;

namespace InLife.Store.Identity.TokenProviders
{
	public static class CustomIdentityBuilderExtensions
	{
		public static IdentityBuilder AddPasswordlessEmailTokenProvider(this IdentityBuilder builder)
		{
			var userType = builder.UserType;
			var provider = typeof(PasswordlessEmailTokenProvider<>).MakeGenericType(userType);
			return builder.AddTokenProvider(Constants.TokenProvider.Email, provider);
		}

		public static IdentityBuilder AddPasswordlessPhoneTokenProvider(this IdentityBuilder builder)
		{
			var userType = builder.UserType;
			var provider = typeof(PasswordlessPhoneTokenProvider<>).MakeGenericType(userType);
			return builder.AddTokenProvider(Constants.TokenProvider.Phone, provider);
		}
	}
}
