using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

using IdentityServer4.Models;
using IdentityServer4.Validation;

using InLife.Store.Identity.Models;
using InLife.Store.Identity.Utilities;


namespace InLife.Store.Identity.GrantValidators
{
	public class EmailCodeGrantValidator : IExtensionGrantValidator
	{
		private readonly ITokenValidator validator;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly ILogger<EmailCodeGrantValidator> logger;

		public EmailCodeGrantValidator
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

		public string GrantType => Constants.Identity.EmailCodeGrant;

		public async Task ValidateAsync(ExtensionGrantValidationContext context)
		{
			var account = context.Request.Raw.Get(Constants.Identity.EmailRequest);
			var code = context.Request.Raw.Get(Constants.Identity.EmailCodeRequest);

			//var account = Base62.Decode(rawAccount);
			//var code = Base62.Decode(rawCode);

			if (String.IsNullOrEmpty(account) || String.IsNullOrEmpty(code))
			{
				var errorMessage = $"Parameters email ({account}) and email_code ({code}) must have a value.";
				context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, errorMessage);
				logger.LogError(errorMessage);
				return;
			}

			var user = await userManager.FindByEmailAsync(account);
			if (user == null)
			{
				var errorMessage = $"Failed to find user {account}.";
				context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, errorMessage);
				logger.LogError(errorMessage);
				return;
			}

			if (!await userManager.VerifyUserTokenAsync(
				user,
				Constants.TokenProvider.Email,
				Constants.TokenPurpose.EmailVerification,
				code))
			{
				var errorMessage = $"Failed to verify the email_code {code} to of account {account}.";
				context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest);
				logger.LogError(errorMessage);
				return;
			}

			if (!user.DateActivated.HasValue)
				user.DateActivated = DateTimeOffset.Now;

			// Confirm email account
			user.EmailConfirmed = true;
			var emailConfirmationResult = await userManager.UpdateAsync(user);
			if (!emailConfirmationResult.Succeeded)
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
