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
	public class PrimeHeroController : BaseController
	{
		private readonly IPrimeHeroRepository primeHeroRepository;

		public PrimeHeroController
		(
			ILogger<BaseController> logger,
			IPrimeHeroRepository primeHeroRepository
		) : base
		(
			logger
		)
		{
			this.primeHeroRepository = primeHeroRepository;
		}

		[HttpGet]
		[Route("GetPrimeHeroSliders")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(ProblemDetails))]
		public IActionResult GetPrimeHeroSliders()
		{
			var result = primeHeroRepository
					.GetAll()
					.Select(model => new PrimeHeroResponse(model))
					.ToList();

			return Ok(result);

			//try
			//{
			//	var result = primeHeroRepository
			//		.GetAll()
			//		.Select(model => new PrimeHeroResponse
			//		{
			//			Id = model.Id,
			//			PrimeHeroBg = ParseImageData(model.PrimeHeroBg),
			//			PrimeHeroTitle = model.PrimeHeroTitle,
			//			PrimeHeroBtnTxt = model.PrimeHeroBtnTxt,
			//			BtnTxtLink = model.BtnTxtLink,
			//			Heading = model.Heading,
			//			SubHeading = model.SubHeading,
			//			PrimeHeroMobBg = ParseImageData(model.PrimeHeroMobBg),
			//			HeadingColor = model.HeadingColor,
			//			SubHeadingColor = model.SubHeadingColor,
			//			ContentPostion = model.ContentPostion
			//		})
			//		.ToList();

			//	return Ok(result);
			//}
			//catch (Exception e)
			//{
			//	return GenericServerErrorResult(e);
			//}
		}
	}
}
