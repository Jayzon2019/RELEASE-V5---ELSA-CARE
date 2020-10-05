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

using InLife.Store.Infrastructure.Services;

using InLife.Store.Api.Messages;


namespace InLife.Store.Api
{
	[ApiController]
	[Route("/")]
	[Produces(MediaTypeNames.Application.Json)]
	public class DefaultController : BaseController
	{
		public DefaultController
		(
			ILogger<BaseController> logger
		) : base
		(
			logger
		)
		{

		}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public ActionResult Get()
        {
            try
            {
                var result = new
                {
                    Name = "InLife Store API",
                    Version = "1.0.0",
                    UniqueId = Guid.NewGuid().ToString(),
                    TimeStamp = DateTimeOffset.Now.ToString()
                };

                return Ok(result);
            }
            catch (Exception e)
            {
                return GenericServerErrorResult(e);
            }
        }

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesErrorResponseType(typeof(ProblemDetails))]
        //public ActionResult Post([FromBody]TestRequest request)
        //{
        //	try
        //	{
        //		var result = new
        //		{
        //			Name = "InLife PrimeCare Store API",
        //			Verb = "POST",
        //			Data = $"{request.Data}",
        //			UniqueId = Guid.NewGuid().ToString(),
        //			TimeStamp = DateTimeOffset.Now.ToString()
        //		};

        //		return Ok(result);
        //	}
        //	catch (Exception e)
        //	{
        //		return GenericServerErrorResult(e);
        //	}
        //}

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
