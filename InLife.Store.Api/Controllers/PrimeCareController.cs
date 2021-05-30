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

using InLife.Store.Api.Messages;


namespace InLife.Store.Api
{
	[ApiController]
	[Route("prime-care")]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesErrorResponseType(typeof(ProblemDetails))]
	public class PrimeCareController : BaseController
	{

		private readonly IPrimeCareApplicationProcessing applicationProcessing;

		public PrimeCareController
		(
			ILogger<BaseController> logger,
			IPrimeCareApplicationProcessing applicationProcessing
		) : base
		(
			logger
		)
		{
			this.applicationProcessing = applicationProcessing;
		}

		// POST /prime-care/applications
		[HttpPost("applications")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public async Task<ActionResult> RequestQuote([FromBody]PrimeCareQuoteRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var quoteForm = request.Map();
				var application = await applicationProcessing.RequestQuote(quoteForm);

				var response = new PrimeCareQuoteResponse(application);

				return Ok(response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}
	}
}
