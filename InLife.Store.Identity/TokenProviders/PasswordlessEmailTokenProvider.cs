using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;


namespace InLife.Store.Identity.TokenProviders
{
	public class PasswordlessEmailTokenProviderOptions
		: DataProtectionTokenProviderOptions
	{
		public PasswordlessEmailTokenProviderOptions()
		{
			Name = Constants.TokenProvider.Email;
			TokenLifespan = TimeSpan.FromMinutes(15);
		}
	}

	//public class PasswordlessEmailTokenProvider<TUser>
	//	: DataProtectorTokenProvider<TUser> where TUser : class
	//{
	//	public PasswordlessEmailTokenProvider
	//	(
	//		IDataProtectionProvider dataProtectionProvider,
	//		IOptions<PasswordlessEmailTokenProviderOptions> options,
	//		ILogger<PasswordlessEmailTokenProvider<TUser>> logger
	//	) : base(dataProtectionProvider, options, logger)
	//	{
	//	}
	//}

	public class PasswordlessEmailTokenProvider<TUser>
		: EmailTokenProvider<TUser> where TUser : class
	{
		public PasswordlessEmailTokenProvider
		(
			IDataProtectionProvider dataProtectionProvider,
			IOptions<PasswordlessEmailTokenProviderOptions> options,
			ILogger<PasswordlessEmailTokenProvider<TUser>> logger
		)
		{
		}
	}
}
