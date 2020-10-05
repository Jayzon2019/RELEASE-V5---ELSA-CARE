using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using InLife.Store.Core.Models;
using InLife.Store.Core.Business;
using InLife.Store.Core.Services;

using InLife.Store.Infrastructure.Services;


namespace InLife.Store.Api
{
	public class BaseController : ControllerBase
	{
		protected readonly ILogger<BaseController> logger;
		//protected readonly IEmailService emailService;

		public BaseController
		(
			ILogger<BaseController> logger//,
			//IEmailService emailService
		)
		{
			this.logger = logger;
			//this.emailService = emailService;
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
				title: $"InLife PrimeCare Store API failed",
				detail: $"Something went wrong with the API. Please notify InLife support with the error. Please attach this number for reference: #LOG-ID#",
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
					$"ExceptionSource: {exception.Source} \n" +
					$"ExceptionMessage: {exception.Message} \n" +
					$"ExceptionInner: {exception.InnerException} \n" +
					$"ExceptionStackTrace: {exception.StackTrace} \n";

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
