using System;
using System.Linq;
using System.Net.Mime;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using InLife.Store.Core.Models;
using InLife.Store.Core.Business;
using InLife.Store.Core.Services;
using InLife.Store.Core.Settings;
using InLife.Store.Resources;

using InLife.Store.Api.Messages;
using InLife.Store.Api.Messages.External.OrderApi;

namespace InLife.Store.Api
{
	[ApiController]
	[Route("[controller]")]
	[Produces(MediaTypeNames.Application.Json)]
	public class OrdersController : BaseController
	{
		private readonly OrderApi orderApi;
		private readonly Lazy<HttpClient> orderApiClient = new Lazy<HttpClient>(() => new HttpClient());

		private readonly IPrimeCareApplicationProcessing primeCareApplicationProcessing;
		private readonly IPrimeSecureApplicationProcessing primeSecureApplicationProcessing;

		public OrdersController
		(
			ILogger<BaseController> logger,
			IOptions<ExternalServices> externalServices,
			IPrimeCareApplicationProcessing primeCareApplicationProcessing,
			IPrimeSecureApplicationProcessing primeSecureApplicationProcessing
		) : base
		(
			logger
		)
		{
			this.orderApi = externalServices.Value.OrderApi;

			this.orderApiClient.Value.DefaultRequestHeaders.Add("XPartnerKey", orderApi.PartnerKey);
			this.orderApiClient.Value.DefaultRequestHeaders.Add("XPartnerSecret", orderApi.PartnerSecret);
			this.orderApiClient.Value.DefaultRequestHeaders.Add("APIMSubscriptionKey", orderApi.SubscriptionKey);

			this.primeCareApplicationProcessing = primeCareApplicationProcessing;
			this.primeSecureApplicationProcessing = primeSecureApplicationProcessing;
		}


		// GET /orders/:refno
		[HttpGet("{refno}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public async Task<ActionResult> Get(string refno)
		{
			try
			{
				// Retrieve from external API
				var endpoint = $"{orderApi.Host}{orderApi.GetEndpoint}".Replace("{reference_number}", refno);
				var apiResult = await orderApiClient.Value.GetAsync(endpoint);

				if (apiResult.IsSuccessStatusCode)
				{
					var apiResultJson = await apiResult.Content.ReadAsStringAsync();
					var apiResultObject = JsonConvert.DeserializeObject<ExternalOrderResponse>(apiResultJson);

					// Save to Azure DB
					// If PrimeCare
					// TODO: If PrimeSecure
					var orderForm = apiResultObject.ToExternalOrderForm();
					var application = await primeCareApplicationProcessing.ExternalOrder(orderForm);

					var orderDetailsResponse = apiResultObject.ToOrderDetailResponse();

					return Ok(orderDetailsResponse);
				}

				return StatusCode((int)apiResult.StatusCode);
			}
			catch (Exception ex)
			{
				return GenericServerErrorResult(ex);
			}
		}
	}
}
