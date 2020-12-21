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

using InLife.Store.Api.Messages;


namespace InLife.Store.Api
{
	[ApiController]
	[Route("[controller]")]
	[Produces(MediaTypeNames.Application.Json)]
	public class QuotesController : BaseController
	{
		private readonly IOrderProcessing orderProcessing;

		public QuotesController
		(
			ILogger<BaseController> logger,
			IOrderProcessing orderProcessing
		) : base
		(
			logger
		)
		{
			this.orderProcessing = orderProcessing;
		}


		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult Post([FromBody]QuoteRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var quote = new Quote
				{
					Customer = new Customer
					{
						NamePrefix = request.NamePrefix,
						NameSuffix = request.NameSuffix,
						FirstName = request.FirstName,
						MiddleName = request.MiddleName,
						LastName = request.LastName,
						Gender = request.Gender,
						BirthDate = request.BirthDate,
						EmailAddress = request.EmailAddress,
						MobileNumber = request.MobileNumber,

						HomeAddress = new Address
						{
							PhoneNumber = request.PhoneNumber,
							Country = request.Country,
							Region = request.Region,
							City = request.City
						}
					},

					ProductCode = request.ProductCode,
					ProductName = request.ProductName,
					ProductFaceAmount = request.ProductFaceAmount,
					PaymentMode = request.PaymentMode,

					ReferralSource = request.ReferralSource,
					AgentCode = request.AgentCode,
					AgentFirstName = request.AgentFirstName,
					AgentLastName = request.AgentLastName,

					Health1 = request.Health1,
					Health2 = request.Health2,
					Health3 = request.Health3,

					IsEligible = request.IsEligible
				};

				this.orderProcessing.RequestQuote(quote);

				var response = new QuoteResponse
				{
					Id = quote.Id
				};

				return Ok(response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		//[Route("send-order-email")]
		//[HttpPost]
		//[ProducesResponseType(StatusCodes.Status200OK)]
		//[ProducesErrorResponseType(typeof(ProblemDetails))]
		//public async Task<ActionResult> SendEmail([FromBody]TestRequest request)
		//{
		//	try
		//	{
		//		var person = new Person()
		//		{
		//			NamePrefix = "",
		//			NameSuffix = "",
		//			FirstName = "Doraemon",
		//			MiddleName = "Takure",
		//			LastName = "Mojacko",

		//			Nationality = "Filipino",
		//			CivilStatus = "Single",
		//			Gender = "Male",

		//			BirthDate = DateTime.Now.AddYears(-40),
		//			BirthCountry = "Philippines",
		//			BirthRegion = "Metro Manila",
		//			BirthCity = "Pasig",

		//			EmailAddress = "email@email.com",
		//			MobileNumber = "631112223333",

		//			HomePhoneNumber = "63001112222",
		//			HomeAddress1 = "HomeAddress1",
		//			HomeAddress2 = "HomeAddress2",
		//			HomeAddress3 = "HomeAddress3",
		//			HomeCity = "Pasig",
		//			HomeRegion = "Metro Manila",
		//			HomeZipCode = "1234",
		//			HomeCountry = "Philippines"
		//		};

		//		var model = new InsuranceApplication()
		//		{
		//			Owner = person,
		//			Insured = person,
		//			Beneficiary = person,
		//		};
		//		await this.emailService.SendOrderConfirmationAsync(model);

		//		return Ok();
		//	}
		//	catch (Exception e)
		//	{
		//		return GenericServerErrorResult(e);
		//	}
		//}
	}
}
