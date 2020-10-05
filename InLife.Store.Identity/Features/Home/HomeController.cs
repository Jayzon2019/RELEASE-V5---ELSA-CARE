using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using IdentityServer4.Services;

namespace InLife.Store.Identity.Features
{
	[SecurityHeaders]
	[AllowAnonymous]
	public class HomeController : Controller
	{
		private readonly IIdentityServerInteractionService interaction;
		private readonly IWebHostEnvironment environment;
		private readonly ILogger logger;

		public HomeController(IIdentityServerInteractionService interaction, IWebHostEnvironment environment, ILogger<HomeController> logger)
		{
			this.interaction = interaction;
			this.environment = environment;
			this.logger = logger;
		}

		public IActionResult Index()
		{
			if (this.environment.IsDevelopment())
			{
				// only show in development
				return View();
			}

			this.logger.LogInformation("Homepage is disabled in production. Returning 404.");
			return NotFound();
		}

		/// <summary>
		/// Shows the error page
		/// </summary>
		public async Task<IActionResult> Error(string errorId)
		{
			var vm = new ErrorViewModel();

			// retrieve error details from identityserver
			var message = await this.interaction.GetErrorContextAsync(errorId);
			if (message != null)
			{
				vm.Error = message;

				if (!this.environment.IsDevelopment())
				{
					// only show in development
					message.ErrorDescription = null;
				}
			}

			return View("Error", vm);
		}
	}
}