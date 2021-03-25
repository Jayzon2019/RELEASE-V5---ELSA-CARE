using InLife.Store.Api.Messages;
using InLife.Store.Core.Business;
using InLife.Store.Core.Models.ContentEntities;
using InLife.Store.Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace InLife.Store.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	[Produces(MediaTypeNames.Application.Json)]
	public class PaymentStatusController : BaseController
	{

		private readonly IPayMentRepository payMentRepository;
		private readonly IPaymentService paymentService;
		public PaymentStatusController
		(
			ILogger<BaseController> logger,
			IPayMentRepository payMentRepository,
			IPaymentService paymentService
		) : base
		(
			logger
		)
		{
			this.payMentRepository = payMentRepository;
			this.paymentService = paymentService;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		[Route("create")]
		public ActionResult Create([FromBody] PaymentStatusResponse paymentStatusResponse)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{

				var paymentStatus = new PaymentStatus
				{
					PayMentId = paymentStatusResponse.PayMentId,
					PayMentMode = paymentStatusResponse.PayMentMode,
				};

				this.paymentService.SavePaymentStatus(paymentStatus);

				var response = new PaymentStatus
				{
					PayMentId = paymentStatus.PayMentId
				};

				return Ok(response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}
	}
}
