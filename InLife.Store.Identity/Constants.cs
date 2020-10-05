using System;
namespace InLife.Store.Identity
{
	public static class Constants
	{
		public static class AccountType
		{
			public const string Email = "EMAIL";
			public const string Phone = "PHONE";
		}

		public static class TokenProvider
		{
			public const string Email = "PasswordlessEmailTokenProvider";
			public const string Phone = "PasswordlessPhoneTokenProvider";
		}

		public static class TokenPurpose
		{
			public const string EmailVerification = "EmailVerification";
			public const string PhoneVerification = "PhoneVerification";
		}

		public static class Identity
		{
			public const string EmailCodeGrant = "email_code";
			public const string EmailRequest = "email";
			public const string EmailCodeRequest = "email_code";

			public const string PhoneCodeGrant = "phone_code";
			public const string PhoneRequest = "phone";
			public const string PhoneCodeRequest = "phone_code";
		}
	}
}
