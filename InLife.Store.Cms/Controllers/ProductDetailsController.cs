using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;
using InLife.Store.Cms.ViewModels;

namespace InLife.Store.Cms.Controllers
{
	public class ProductDetailsController : BaseController
	{
		private readonly IProductRepository productRepository;
		private readonly IProductDetailRepository productDetailRepository;

		public ProductDetailsController
		(
			ILogger<ProductDetailsController> logger,
			IUserRepository userRepository,
			IActivityLogRepository activityLogRepository,
			IProductRepository productRepository,
			IProductDetailRepository productDetailRepository
		) : base
		(
			userRepository,
			logger,
			activityLogRepository
		)
		{
			this.productRepository = productRepository;
			this.productDetailRepository = productDetailRepository;
		}

		// GET: ProductDetails
		public ActionResult Index()
		{
			try
			{
				var viewModelList = productDetailRepository
					.GetAll()
					.Select(model => new ProductDetailViewModel(model))
					.ToList();

				if (viewModelList == null)
					return NotFound();

				return View(viewModelList);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: ProductDetails/Details/5
		public ActionResult Details(int? id)
		{
			//var model = productDetailRepository.Get(id); 
			var model = productDetailRepository.GetAll().SingleOrDefault(x => x.Id == id);

			if (model == null)
				return NotFound();

			var viewModel = new ProductDetailViewModel(model);

			return View(viewModel);
		}

		// GET: ProductDetails/Create
		public IActionResult Create()
		{
			try
			{
				var faqCategoryViewModelList = productRepository
					.GetAll()
					.Select(model => new ProductViewModel(model))
					.ToList();

				ViewBag.Products = faqCategoryViewModelList;
				return View();
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: ProductDetails/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind("ProductId, ProductImg, ProductName, ProductPrice, CasesCovered, BenefitType, AgeEligibility, NumberOfAvailments, BenefitLimit, DocProFee, RoomAccommodation, LaboratoryDiagnosticPro, MedicinesAsMedicallyNeeded, UseOfOperationRoom, SurgerySurgonFees, Laparoscopic, MRA, MRI, CT, Therapetic, PainManagement, Arthoscopic, OtherMedical, OneTime, Usage, AccreditedHospitals, MER, AFR, ARP, Validity, Waiting, NumberOfRegistrations, UnlimitedTeleMed, PreExistingConCover, NonAccreditedHospitals, ReimbursementNonAccreditedHospitals, TopSixHospitalAccess, RegistrationOfSucceedingVouchers, Combinability, IndividualOrGroup, PrepaidPlan, Consultation, Inclusions, SpecialModalities, Exclusions, FTFConsultation, Telemedicine, DentalConsultation, DentalServicesBenefit, HospitalNetwork, RegistrationRules, MedicalCoverage, LearnMoreBtnLink, BuyNowBtnLink, Coverage, VoucherUsed, VoucherUnused, ConsultationCards, InPatient, OutPatient")]ProductDetailViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var model = viewModel.Map();

				var product = this.productRepository.Get(viewModel.ProductId);
				model.Product = product;

				model.CreatedBy = this.CurrentUser();
				model.CreatedDate = DateTimeOffset.Now;

				this.productDetailRepository.Create(model);

				LogUserActivity("Created a Product Detail", $"Created a new Product Detail - {model.Product.ProductName}");
	
				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: ProductDetails/Edit/5
		public ActionResult Edit(int? id)
		{
			try
			{
				var model = this.productDetailRepository.Get(id);
				if (model == null)
					return NotFound();

				var productViewModelList = productRepository
					.GetAll()
					.Select(model => new ProductViewModel(model))
					.ToList();

				ViewBag.Products = productViewModelList;

				var viewModel = new ProductDetailViewModel(model);

				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: ProductDetails/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, [Bind("ProductId, ProductImg, ProductName, ProductPrice, CasesCovered, BenefitType, AgeEligibility, NumberOfAvailments, BenefitLimit, DocProFee, RoomAccommodation, LaboratoryDiagnosticPro, MedicinesAsMedicallyNeeded, UseOfOperationRoom, SurgerySurgonFees, Laparoscopic, MRA, MRI, CT, Therapetic, PainManagement, Arthoscopic, OtherMedical, OneTime, Usage, AccreditedHospitals, MER, AFR, ARP, Validity, Waiting, NumberOfRegistrations, UnlimitedTeleMed, PreExistingConCover, NonAccreditedHospitals, ReimbursementNonAccreditedHospitals, TopSixHospitalAccess, RegistrationOfSucceedingVouchers, Combinability, IndividualOrGroup, PrepaidPlan, Consultation, Inclusions, SpecialModalities, Exclusions, FTFConsultation, Telemedicine, DentalConsultation, DentalServicesBenefit, HospitalNetwork, RegistrationRules, MedicalCoverage, LearnMoreBtnLink, BuyNowBtnLink, Coverage, VoucherUsed, VoucherUnused, ConsultationCards, InPatient, OutPatient")] ProductDetailViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var model = this.productDetailRepository.Get(id);

				if (model == null)
					return NotFound();

				model = viewModel.Map(model);

				var product = this.productRepository.Get(viewModel.ProductId);
				model.Product = product;

				model.UpdatedBy = this.CurrentUser();
				model.UpdatedDate = DateTimeOffset.Now;

				this.productDetailRepository.Update(model);

				LogUserActivity("Updated a Product Detail", $"Product Detail '{model.Product.ProductName}' [{model.Id}] has been updated.");

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: Users/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		public ActionResult Delete(int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var model = this.productDetailRepository.Get(id);
				if (model == null)
					return NotFound();

				this.productDetailRepository.Delete(model);

				LogUserActivity("Deleted a Product Detail", $"Product Detail '{model.Product.ProductName}' [{model.Id}] has been deleted.");

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: Users/Delete/5
		//[HttpPost, ActionName("Deactive")]
		//[ValidateAntiForgeryToken]
		//public ActionResult Deactive(int id)
		//{
		//	var log = "";
		//	try
		//	{
		//		PDS.Deactivate_DeleteProductDetail(ref log, id, false);
		//		return RedirectToAction(nameof(Index));
		//	}
		//	catch (Exception ex)
		//	{
		//		string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
		//		var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
		//		lR.SaveExceptionLogs(exLog, ex, methodName);
		//		ViewBag.error = Comman.SomethingWntWrong;
		//		return RedirectToAction(nameof(Index));
		//	}
		//}
	}
}
