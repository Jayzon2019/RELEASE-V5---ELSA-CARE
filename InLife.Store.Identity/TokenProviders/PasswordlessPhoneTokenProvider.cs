using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;


namespace InLife.Store.Identity.TokenProviders
{
	public class PasswordlessPhoneTokenProviderOptions
		: DataProtectionTokenProviderOptions
	{
		public PasswordlessPhoneTokenProviderOptions()
		{
			Name = Constants.TokenProvider.Phone;
			TokenLifespan = TimeSpan.FromMinutes(15);
		}
	}

	//public class PasswordlessPhoneTokenProvider<TUser>
	//	: DataProtectorTokenProvider<TUser> where TUser : class
	//{
	//	public PasswordlessPhoneTokenProvider
	//	(
	//		IDataProtectionProvider dataProtectionProvider,
	//		IOptions<PasswordlessPhoneTokenProviderOptions> options,
	//		ILogger<PasswordlessPhoneTokenProvider<TUser>> logger
	//	) : base(dataProtectionProvider, options, logger)
	//	{
	//	}
	//}

	public class PasswordlessPhoneTokenProvider<TUser>
		: PhoneNumberTokenProvider<TUser> where TUser : class
	{
		public PasswordlessPhoneTokenProvider
		(
			IDataProtectionProvider dataProtectionProvider,
			IOptions<PasswordlessPhoneTokenProviderOptions> options,
			ILogger<PasswordlessPhoneTokenProvider<TUser>> logger
		)
		{
		}
	}
}
