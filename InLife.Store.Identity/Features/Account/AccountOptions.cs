using System;

namespace InLife.Store.Identity.Features
{
	public class AccountOptions
	{
		public static bool AllowLocalLogin = true;
		public static bool AllowRememberLogin = false;
		public static TimeSpan RememberMeLoginDuration = TimeSpan.FromMinutes(5);

		public static bool ShowLogoutPrompt = true;
		public static bool AutomaticRedirectAfterSignOut = false;

		public static string InvalidCredentialsErrorMessage = "Invalid username or password";
	}
}
