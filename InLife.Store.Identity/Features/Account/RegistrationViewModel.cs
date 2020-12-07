using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace InLife.Store.Identity.Features
{
	public class RegistrationViewModel : LoginInputModel
	{
		[Required]
		[Compare("Password", ErrorMessage = "The passwords didn't match.")]
		public string PasswordConfirm { get; set; }

		public bool AllowRememberLogin { get; set; } = false;
		public bool EnableLocalLogin { get; set; } = true;


		public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();
		public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));

		public bool IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders?.Count() == 1;
		public string ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;

	}
}
