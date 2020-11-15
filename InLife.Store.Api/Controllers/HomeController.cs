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
	public class HomeController : BaseController
	{
		private readonly IHeroRepository heroRepository;
		private readonly IProductRepository productRepository;
		private readonly IProductDetailRepository productDetailRepository;
		private readonly IPrimeCareRepository primeCareRepository;

		public HomeController
		(
			ILogger<BaseController> logger,
			IHeroRepository heroRepository,
			IProductRepository productRepository,
			IProductDetailRepository productDetailRepository,
			IPrimeCareRepository primeCareRepository
		) : base
		(
			logger
		)
		{
			this.heroRepository = heroRepository;
			this.productRepository = productRepository;
			this.productDetailRepository = productDetailRepository;
			this.primeCareRepository = primeCareRepository;
		}

		[HttpGet]
		[Route("GetHeroSliders")]
		public IActionResult GetHeroSliders()
		{
			try
			{
				var result = heroRepository
					.GetAll()
					.Select(model => new HeroResponse(model))
					.ToList();

				return Ok(result);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		[HttpGet]
		[Route("GetProductsList")]
		public IActionResult GetProductsList()
		{
			try
			{
				var result = productRepository
					.GetAll()
					.Select(model => new ProductResponse(model))
					.ToList();

				return Ok(result);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		[HttpGet]
		[Route("GetProductsDetailList")]
		public IActionResult GetProductsDetailList()
		{
			try
			{
				var result = productDetailRepository
					.GetAll()
					.Select(model => new ProductDetailResponse(model))
					.ToList();

				return Ok(result);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}


		[HttpGet]
		[Route("GetPageId_SetPageView")]
		public IActionResult GetPageId_SetPageView(string page)
		{
			return Ok();

			//var log = "";
			//try
			//{
			//	var result = HS.GetPageId_SetPageView(ref log, page);
			//	return Ok(result);
			//}
			//catch (Exception ex)
			//{
			//	string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
			//	var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
			//	lR.SaveExceptionLogs(exLog, ex, methodName);
			//	return NotFound();
			//}
		}

		[HttpGet]
		[Route("SetUserLeaveTime")]
		public IActionResult SetUserLeaveTime(int id)
		{
			return Ok();
			//var log = "";
			//try
			//{
			//	HS.SetUserLeaveTime(ref log, id);
			//	return Ok("Success");
			//}
			//catch (Exception ex)
			//{
			//	string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
			//	var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
			//	lR.SaveExceptionLogs(exLog, ex, methodName);
			//	return NotFound();
			//}
		}

		[HttpGet]
		[Route("GetFiles")]
		public IActionResult GetFiles()
		{
			try
			{
				var result = primeCareRepository
					.GetAll()
					.Select(model => new PrimeCareResponse
					{
						Id = model.Id,
						PrimeCareFile = model.PrimeCareFile,
						PrimeCareFileName = model.PrimeCareFileName,
						PrimeCareFileDescription = model.PrimeCareFileDescription
					})
					.ToList();

				return Ok(result);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}

			//var log = "";
			//try
			//{
			//	var obj = HS.GetFiles(ref log);
			//	return Ok(obj);
			//}
			//catch (Exception ex)
			//{
			//	string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
			//	var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
			//	lR.SaveExceptionLogs(exLog, ex, methodName);
			//	return NotFound();
			//}
		}
	}
}
