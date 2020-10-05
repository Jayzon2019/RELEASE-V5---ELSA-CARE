using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

using IdentityServer4.Models;
using IdentityServer4.Validation;

using InLife.Store.Identity.Models;


namespace InLife.Store.Identity.GrantValidators
{
	public class PhoneCodeGrantValidator : IExtensionGrantValidator
	{
		private readonly ITokenValidator validator;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly ILogger<EmailCodeGrantValidator> logger;

		public PhoneCodeGrantValidator
		(
			ITokenValidator validator,
			UserManager<ApplicationUser> userManager,
			ILogger<EmailCodeGrantValidator> logger
		)
		{
			this.validator = validator;
			this.userManager = userManager;
			this.logger = logger;
		}

		public string GrantType => Constants.Identity.PhoneCodeGrant;

		public async Task ValidateAsync(ExtensionGrantValidationContext context)
		{
			var account = context.Request.Raw.Get(Constants.Identity.PhoneRequest);
			var code = context.Request.Raw.Get(Constants.Identity.PhoneCodeRequest);
			
			if (String.IsNullOrEmpty(account) || String.IsNullOrEmpty(code))
			{
				context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
				return;
			}

			var user = await userManager.FindByEmailAsync(account);
			if (user == null)
			{
				context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
				return;
			}

			if (!await userManager.VerifyUserTokenAsync(
				user,
				Constants.TokenProvider.Phone,
				Constants.TokenPurpose.PhoneVerification,
				code))
			{
				context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
				return;
			}

			if (!user.DateActivated.HasValue)
				user.DateActivated = DateTimeOffset.Now;

			// Confirm phone number
			user.PhoneNumberConfirmed = true;
			var phoneConirmationResult = await userManager.UpdateAsync(user);
			if (!phoneConirmationResult.Succeeded)
			{
				var errorMessage = $"Failed to verify the email {account}.";
				context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest);
				logger.LogError(errorMessage);
				return;
			}

			context.Result = new GrantValidationResult(user.Id, GrantType);
			return;
		}
	}
}
