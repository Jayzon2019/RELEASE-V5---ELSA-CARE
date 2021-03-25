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

namespace InLife.Store.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[Produces(MediaTypeNames.Application.Json)]
	public class FeedbackController : BaseController
	{
		private readonly IFeedbackRepository feedbackRepository;
		private readonly IFeedbackService feedbackService;
		public FeedbackController
		(
			ILogger<BaseController> logger,
			IFeedbackRepository feedbackRepository,
			IFeedbackService feedbackService
		) : base
		(
			logger
		)
		{
			this.feedbackRepository = feedbackRepository;
			this.feedbackService = feedbackService;
		}

		[HttpGet]
		[Route("GetFeedbackList")]
		public IActionResult GetFeedbackList()
		{
			try
			{
				var result = feedbackRepository
					.GetAll()
					.Select(model => new FeedbackResponse
					{
						Id = model.Id,
						RefId = model.RefId,
						Comment = model.Comment
					})
					.ToList();

				return Ok(result);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public ActionResult Post([FromBody] FeedbackRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var feedbackType = "";
				switch (request.FeedbackType)
				{	
					case "0":
						{
							feedbackType = "Poor";
							break;
						}
					case "1":
						{
							feedbackType = "Fair";
							break;
						}
					case "2":
						{
							feedbackType = "Good";
							break;
						}
					case "3":
						{
							feedbackType = "Great";
							break;
						}
				}
				var feedback = new Feedback
				{
					RefId = request.RefId,
					FeedbackType = feedbackType,
					Comment = request.Comment,
					CreatedDate = DateTime.Now
				};

				this.feedbackService.SaveFeedback(feedback);

				var response = new FeedbackResponse
				{
					Id = feedback.Id
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
