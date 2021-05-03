using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using InLife.Store.Core.Models;
using InLife.Store.Core.Business;
using InLife.Store.Core.Services;
using InLife.Store.Core.Repository;
using InLife.Store.Core.Utilities;

using InLife.Store.Api.Messages;
using System.IO;

namespace InLife.Store.Api
{
	[ApiController]
	[Route("prime-secure")]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesErrorResponseType(typeof(ProblemDetails))]
	public class PrimeSecureController : BaseController
	{
		private readonly IPrimeSecureApplicationProcessing applicationProcessing;

		public PrimeSecureController
		(
			ILogger<BaseController> logger,
			IPrimeSecureApplicationProcessing applicationProcessing
		) : base
		(
			logger
		)
		{
			this.applicationProcessing = applicationProcessing;
		}

		[HttpGet]
		public ActionResult Get()
		{
			try
			{
				var result = new
				{
					Name = "InLife Store API - PrimeSecure Endpoint",
					TimeStamp = DateTimeOffset.Now.ToString()
				};

				return Ok(result);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST /prime-secure/applications
		[HttpPost("applications")]
		public async Task<ActionResult> RequestQuote([FromBody]PrimeSecureQuoteRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var quoteForm = request.Map();
				var application = await applicationProcessing.RequestQuote(quoteForm);

				var response = new PrimeSecureQuoteResponse(application);

				return Ok(response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

	}
}
