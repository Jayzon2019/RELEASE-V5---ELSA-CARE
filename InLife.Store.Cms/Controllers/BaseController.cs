using System;
using System.Linq;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.Controllers
{
	public class BaseController : Controller
	{
		//protected readonly UserManager<ApplicationUser> userManager;
		protected readonly IUserRepository userRepository;
		protected readonly ILogger<BaseController> logger;

		public BaseController
		(
			//UserManager<ApplicationUser> userManager,
			IUserRepository userRepository,
			ILogger<BaseController> logger
		)
		{
			//this.userManager = userManager;
			this.userRepository = userRepository;
			this.logger = logger;
		}

		protected User CurrentUser()
		{
			var userId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
			var user = this.userRepository.Get(userId);
			return user;
		}

		protected ObjectResult GenericServerErrorResult
		(
			Exception exception = null,
			[CallerLineNumber] int lineNumber = 0,
			[CallerFilePath] string callerFilePath = null,
			[CallerMemberName] string callerMemberName = null
		)
		{
			return ErrorResult
			(
				exception: exception,
				status: StatusCodes.Status500InternalServerError,
				title: $"InLife Store CMS failed",
				detail: $"Something went wrong with the CMS. Please notify InLife support with the error and attach this number for reference: #LOG-ID#",
				lineNumber: lineNumber,
				callerFilePath: callerFilePath,
				callerMemberName: callerMemberName
			);
		}

		protected ObjectResult ErrorResult
		(
			int? status,
			string title,
			string detail,
			Exception exception = null,
			[CallerLineNumber] int lineNumber = 0,
			[CallerFilePath] string callerFilePath = null,
			[CallerMemberName] string callerMemberName = null
		)
		{
			///var logId = Guid.NewGuid().Shorten();
			var logId = Guid.NewGuid().ToString();

			title = title.Replace("#LOG-ID#", logId);
			detail = detail.Replace("#LOG-ID#", logId);

			var log =
				$"LogId: {logId} \n" +
				$"Title: {title} \n" +
				$"Detail: {detail} \n" +
				$"Source: {callerFilePath} {callerMemberName} at line {lineNumber} \n";

			if (exception != null)
				log +=
					$"ExceptionSource: {exception.Source}\n" +
					$"ExceptionMessage: {exception.Message}\n" +
					$"ExceptionInner: {exception.InnerException}\n" +
					$"ExceptionStackTrace: {exception.StackTrace}\n";

			Debug.WriteLine(log);
			logger.LogError(log);

			var problemDetails = new ProblemDetails
			{
				Status = status,
				Type = "about:blank",
				Title = title,
				Detail = detail,
				Instance = HttpContext.Request.Path
			};

			return new ObjectResult(problemDetails)
			{
				ContentTypes = { "application/problem+json" },
				StatusCode = status,
			};
		}
	}
}
