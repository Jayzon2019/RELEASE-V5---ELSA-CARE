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
	[Route("group")]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesErrorResponseType(typeof(ProblemDetails))]
	public class GroupController : BaseController
	{
		private readonly IGroupApplicationProcessing applicationProcessing;

		public GroupController
		(
			ILogger<BaseController> logger,
			IGroupApplicationProcessing applicationProcessing
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
					Name = "InLife Store API - Group Endpoint",
					TimeStamp = DateTimeOffset.Now.ToString()
				};

				return Ok(result);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET /group/applications/:refcode
		[HttpGet("applications/{refcode}")]
		public ActionResult Get(string refcode, [FromHeader]RequestHeaders headers)
		{
			try
			{
				if (!applicationProcessing.VerifySession(refcode, headers.Session))
					return Unauthorized();

				var application = applicationProcessing.GetApplication(refcode);

				if (application == null)
					return NotFound();

				var response = new GroupApplicationSummaryResponse(application);

				return Ok(response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET /group/applications/:refcode/summary
		[HttpGet("applications/{refcode}/summary")]
		public ActionResult GetSummary(string refcode, [FromHeader] RequestHeaders headers)
		{
			try
			{
				if (!applicationProcessing.VerifySession(refcode, headers.Session))
					return Unauthorized();

				var application = applicationProcessing.GetApplication(refcode);

				if (application == null)
					return NotFound();

				var response = new GroupApplicationSummaryResponse(application);

				return Ok(response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET /group/applications/:refcode/status
		[HttpGet("applications/{refcode}/status")]
		public ActionResult GetStatus(string refcode, [FromHeader] RequestHeaders headers)
		{
			try
			{
				if (!applicationProcessing.VerifySession(refcode, headers.Session))
					return Unauthorized();

				var application = applicationProcessing.GetApplication(refcode);

				if (application == null)
					return NotFound();

				var response = new BaseGroupResponse(application);

				return Ok(response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST /group/applications
		[HttpPost("applications")]
		public async Task<ActionResult> RequestQuote([FromBody]GroupQuoteRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var req = HttpContext.Request;
				var url = $"{req.Scheme}://{req.Host}";
				var quoteForm = request.Map();
				var application = await applicationProcessing.RequestQuote(quoteForm, url);

				var response = new GroupQuoteResponse(application);

				return Ok(response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// PATCH /group/applications
		[HttpPatch("applications/{refcode}")]
		public async Task<ActionResult> UpdateQuote(string refcode, [FromHeader] RequestHeaders headers, [FromBody] GroupQuoteRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				if (!applicationProcessing.VerifySession(refcode, headers.Session))
					return Unauthorized();

				var req = HttpContext.Request;
				var url = $"{req.Scheme}://{req.Host}";
				var quoteForm = request.Map();
				var application = await applicationProcessing.UpdateQuote(refcode, quoteForm, url);

				var response = new BaseGroupResponse(application);

				return Ok(response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// PATCH /group/applications
		[HttpPatch("applications/{refcode}/status")]
		public ActionResult UpdateQuoteStatus(string refcode, [FromHeader] RequestHeaders headers)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				if (!applicationProcessing.VerifySession(refcode, headers.Session))
					return Unauthorized();

				var application = applicationProcessing.UpdateQuoteStatus(refcode);

				var response = new BaseGroupResponse(application);

				return Ok(response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// PUT /group/applications/:refcode
		[HttpPut("applications/{refcode}")]
		public async Task<ActionResult> SaveApplication(string refcode, [FromHeader] RequestHeaders headers, [FromBody] GroupApplicationRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				if (!applicationProcessing.VerifySession(refcode, headers.Session))
					return Unauthorized();

				var req = HttpContext.Request;
				var url = $"{req.Scheme}://{req.Host}";
				var applicationForm = request.Map();
				var application = await applicationProcessing.SaveApplication(refcode, applicationForm, url);

				if (application == null)
					return NotFound();

				var response = new BaseGroupResponse(application);

				return Ok(response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// PUT /group/applications/:refcode/files/{type}
		[HttpPut("applications/{refcode}/files/{type}")]
		[Consumes("application/octet-stream", new string[]
		{
			"image/jpg", "image/jpeg", "image/png",
			"application/pdf",
			"application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
			"application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
			"application/zip", "application/gzip", "application/x-7z-compressed", "application/vnd.rar"
		})]
		[DisableRequestSizeLimit]
		public async Task<ActionResult> UploadFile(string refcode, string type, [FromHeader] FileUploadRequestHeaders headers)
		{
			try
			{
				if (!applicationProcessing.VerifySession(refcode, headers.Session))
					return Unauthorized();

				var application = await applicationProcessing.UploadFile(refcode, type, Request.ContentType, headers.Filename, Request.Body);

				if (application == null)
					return NotFound();

				var response = new BaseGroupResponse(application);

				return Ok(response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// PUT /group/applications/:refcode/payment
		[HttpPut("applications/{refcode}/payment")]
		public async Task<ActionResult> Payment(string refcode, [FromHeader] RequestHeaders headers, [FromBody] GroupPaymentRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var paymentMethod = PaymentMode.FromId(request.PaymentMethod, null);
			if (paymentMethod == null)
				return BadRequest();

			try
			{
				if (!applicationProcessing.VerifySession(refcode, headers.Session))
					return Unauthorized();

				var application = await applicationProcessing.SetPaymentMode(refcode, paymentMethod);

				if (application == null)
					return NotFound();

				var response = new BaseGroupResponse(application);

				return Ok(response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// PUT /group/applications/:refcode/feedback
		[HttpPut("applications/{refcode}/feedback")]
		public async Task<ActionResult> Feedback(string refcode, [FromHeader] RequestHeaders headers, [FromBody] GroupFeedbackRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				if (!applicationProcessing.VerifySession(refcode, headers.Session))
					return Unauthorized();

				var feedbackForm = request.Map();

				var application = await applicationProcessing.Feedback(refcode, feedbackForm);

				if (application == null)
					return NotFound();

				var response = new BaseGroupResponse(application);

				return Ok(response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// PUT /group/applications/:refcode/cancel
		[HttpPut("applications/{refcode}/cancel")]
		public async Task<ActionResult> Cancel(string refcode, [FromHeader] RequestHeaders headers, [FromBody] GroupCancelRequest request)
		{
			try
			{
				if (!applicationProcessing.VerifySession(refcode, headers.Session))
					return Unauthorized();

				var cancelForm = request.Map();

				var application = await applicationProcessing.Cancel(refcode, cancelForm);

				if (application == null)
					return NotFound();

				var response = new BaseGroupResponse(application);

				return Ok(response);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// PUT /group/applications/:refcode/request-otp
		[HttpPut("applications/{refcode}/request-otp")]
		public async Task<ActionResult> RequestOTP(string refcode)
		{
			try
			{
				var result = await applicationProcessing.RequestOtp(refcode);

				if (!result)
					return NotFound();

				return Ok();
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET /group/applications/:refcode/request-session?otp={otp}
		[HttpGet("applications/{refcode}/request-session")]
		public ActionResult RequestSession(string refcode, [FromQuery] string otp)
		{
			try
			{
				var application = applicationProcessing.RequestSession(refcode, otp);

				if (application == null)
					return NotFound();

				if (String.IsNullOrWhiteSpace(application.Session))
					return Unauthorized();

				var result = new GroupRequestSessionResponse(application);

				return Ok(result);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		[HttpGet("applications/batch-process-test")]
		public async Task<ActionResult> BatchProcessTest()
		{
			try
			{

				await applicationProcessing.ProcessCompletedApplications();
				return Ok();
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

	}
}
