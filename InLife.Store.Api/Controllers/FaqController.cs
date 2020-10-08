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
	public class FaqController : BaseController
	{
		private readonly IFaqRepository faqRepository;
		private readonly IFaqCategoryRepository faqCategoryRepository;

		public FaqController
		(
			ILogger<BaseController> logger,
			IFaqRepository faqRepository,
			IFaqCategoryRepository faqCategoryRepository
		) : base
		(
			logger
		)
		{
			this.faqRepository = faqRepository;
			this.faqCategoryRepository = faqCategoryRepository;
		}

		[HttpGet]
		[Route("GetFaqCatList")]
		public IActionResult GetFaqCatList()
		{
			try
			{
				var result = faqCategoryRepository
					.GetAll()
					.Select(model => new FaqCategoryResponse
					{
						Id = model.Id,
						Name = model.Name,
						Description = model.Description
					})
					.ToList();

				return Ok(result);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		[HttpGet]
		[Route("GetFaqListByCatId")]
		public IActionResult GetFaqListByCatId(int id)
		{
			try
			{
				var result = faqRepository
					.GetAll()
					.Where(model => model.Category.Id == id)
					.Select(model => new FaqResponse
					{
						Id = model.Id,
						CategoryId = model.Category.Id,
						CategoryName = model.Category.Name,
						Question = model.Question,
						Answer = model.Answer,
						SortNum = model.SortNum ?? 1000
					})
					.ToList();

				return Ok(result);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		[HttpGet]
		[Route("GetFaqList")]
		public IActionResult GetFaqList()
		{
			try
			{
				var result = faqRepository
					.GetAll()
					.Select(model => new FaqResponse
					{
						Id = model.Id,
						CategoryId = model.Category.Id,
						CategoryName = model.Category.Name,
						Question = model.Question,
						Answer = model.Answer,
						SortNum = model.SortNum ?? 1000
					})
					.ToList();

				return Ok(result);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}
	}
}
