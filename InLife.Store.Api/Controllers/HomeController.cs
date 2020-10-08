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
					.Select(model => new HeroResponse
					{
						Id = model.Id,
						HeroBg = model.HeroBg,
						HeroTitle = model.HeroTitle,
						HeroBtnTxt = model.HeroBtnTxt,
						BtnTxtLink = model.BtnTxtLink,
						Heading = model.Heading,
						SubHeading = model.SubHeading,
						HeroMobBg = model.HeroMobBg,
						HeadingColor = model.HeadingColor,
						SubHeadingColor = model.SubHeadingColor,
						ContentPostion = model.ContentPostion
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
		[Route("GetProductsList")]
		public IActionResult GetProductsList()
		{
			try
			{
				var result = productRepository
					.GetAll()
					.Select(model => new ProductResponse
					{
						Id = model.Id,
						ProductImg = model.ProductImg,
						ProductName = model.ProductName,
						ProductPrice = model.ProductPrice,
						ProductCode = model.ProductCode,
						ShortDescription = model.ShortDescription,
						PriceWithOffer = model.PriceWithOffer,
						SortNum = model.SortNum
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
		[Route("GetProductsDetailList")]
		public IActionResult GetProductsDetailList()
		{
			try
			{
				var result = productDetailRepository
					.GetAll()
					.Select(model => new ProductDetailResponse
					{
						Id = model.Id,

						ProductId = model.Product.Id,
						ProductImg = model.Product.ProductImg,
						ProductName = model.Product.ProductName,
						ProductPrice = model.Product.ProductPrice,
						ProductCode = model.Product.ProductCode,

						CasesCovered = model.CasesCovered,
						BenefitType = model.BenefitType,
						AgeEligibility = model.AgeEligibility,
						NumberOfAvailments = model.NumberOfAvailments,
						BenefitLimit = model.BenefitLimit,
						DocProFee = model.DocProFee,
						RoomAccommodation = model.RoomAccommodation,
						LaboratoryDiagnosticPro = model.LaboratoryDiagnosticPro,
						MedicinesAsMedicallyNeeded = model.MedicinesAsMedicallyNeeded,
						UseOfOperationRoom = model.UseOfOperationRoom,
						SurgerySurgonFees = model.SurgerySurgonFees,
						Laparoscopic = model.Laparoscopic,
						MRA = model.MRA,
						MRI = model.MRI,
						CT = model.CT,
						Therapetic = model.Therapetic,
						PainManagement = model.PainManagement,
						Arthoscopic = model.Arthoscopic,
						OtherMedical = model.OtherMedical,
						OneTime = model.OneTime,
						Usage = model.Usage,
						AccreditedHospitals = model.AccreditedHospitals,
						MER = model.MER,
						AFR = model.AFR,
						ARP = model.ARP,
						Validity = model.Validity,
						Waiting = model.Waiting,
						NumberOfRegistrations = model.NumberOfRegistrations,
						UnlimitedTeleMed = model.UnlimitedTeleMed,
						PreExistingConCover = model.PreExistingConCover,
						NonAccreditedHospitals = model.NonAccreditedHospitals,
						ReimbursementNonAccreditedHospitals = model.ReimbursementNonAccreditedHospitals,
						TopSixHospitalAccess = model.TopSixHospitalAccess,
						RegistrationOfSucceedingVouchers = model.RegistrationOfSucceedingVouchers,
						Combinability = model.Combinability,
						IndividualOrGroup = model.IndividualOrGroup,
						PrepaidPlan = model.PrepaidPlan,
						Consultation = model.Consultation,
						Inclusions = model.Inclusions,
						SpecialModalities = model.SpecialModalities,
						Exclusions = model.Exclusions,
						FTFConsultation = model.FTFConsultation,
						Telemedicine = model.Telemedicine,
						DentalConsultation = model.DentalConsultation,
						DentalServicesBenefit = model.DentalServicesBenefit,
						HospitalNetwork = model.HospitalNetwork,
						RegistrationRules = model.RegistrationRules,
						MedicalCoverage = model.MedicalCoverage,
						LearnMoreBtnLink = model.LearnMoreBtnLink,
						BuyNowBtnLink = model.BuyNowBtnLink,
						Coverage = model.Coverage,
						VoucherUsed = model.VoucherUsed,
						VoucherUnused = model.VoucherUnused,
						ConsultationCards = model.ConsultationCards,
						InPatient = model.InPatient,
						OutPatient = model.OutPatient,
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
