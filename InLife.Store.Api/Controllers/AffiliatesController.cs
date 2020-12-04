using System;
using System.Linq;
using System.Net.Mime;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using InLife.Store.Core.Models;
using InLife.Store.Core.Business;
using InLife.Store.Core.Services;
using InLife.Store.Core.Settings;

using InLife.Store.Api.Messages;
using Newtonsoft.Json;

namespace InLife.Store.Api
{
	[ApiController]
	[Route("[controller]")]
	[Produces(MediaTypeNames.Application.Json)]
	public class AffiliatesController : BaseController
	{
		private readonly AffiliateApi affiliateApi;

		private readonly Lazy<HttpClient> affiliateApiClient = new Lazy<HttpClient>(() => new HttpClient());

		public AffiliatesController
		(
			ILogger<BaseController> logger,
			IOptions<ExternalServices> externalServices
		) : base
		(
			logger
		)
		{
			this.affiliateApi = externalServices.Value.AffiliateApi;

			this.affiliateApiClient.Value.DefaultRequestHeaders.Add("ClientID", affiliateApi.ClientId);
			this.affiliateApiClient.Value.DefaultRequestHeaders.Add("ClientSecret", affiliateApi.ClientSecret);
		}


		// GET /affiliates/:code
		[HttpGet("{code}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public async Task<ActionResult> Get(string code)
		{
			var endpoint = $"{affiliateApi.Host}{affiliateApi.AgentInfoEndpoint}/{code}";

			var result = await affiliateApiClient.Value.GetAsync(endpoint);

			if (result.IsSuccessStatusCode)
			{
				var resultJson = await result.Content.ReadAsStringAsync();
				var resultObject = JsonConvert.DeserializeObject<AffiliateInfoResponse>(resultJson);

				if (resultObject.Result.Success)
				{
					return Ok(resultObject.Agent);
				}

				return NotFound();
			}

			return GenericServerErrorResult();
		}

	}
}
