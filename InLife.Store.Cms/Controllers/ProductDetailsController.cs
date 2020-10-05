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
	[Authorize]
	public class ProductDetailsController : BaseController
	{
		private readonly IProductDetailRepository productDetailRepository;

		public ProductDetailsController
		(
			ILogger<ProductDetailsController> logger,
			IUserRepository userRepository,
			IProductDetailRepository productDetailRepository
		) : base
		(
			userRepository,
			logger
		)
		{
			this.productDetailRepository = productDetailRepository;
		}



		ProductDetailService PDS = new ProductDetailService();
		ProductsService PS = new ProductsService();
		LogsRepo lR = new LogsRepo();

		// GET: ProductDetails
		public ActionResult Index()
		{
			var log = "";
			try
			{
				var proDetList = PDS.GetProductDetailsList(ref log);
				if (proDetList != null)
				{
					return View(proDetList);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				return NotFound();
			}

		}

		// GET: ProductDetails/Details/5
		public ActionResult Details(int? id)
		{
			var log = "";
			try
			{
				if (id == null)
				{
					return NotFound();
				}

				var ProDet = PDS.GetProductDetailById(ref log, id);
				if (ProDet == null)
				{
					return NotFound();
				}

				return View(ProDet);
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				return NotFound();
			}
		}

		// GET: ProductDetails/Create
		public IActionResult Create()
		{
			var log = "";
			try
			{
				var products = PS.GetProductsList(ref log);
				ViewBag.products = products;
				return View();
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				return NotFound();
			}
		}

		// POST: ProductDetails/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind("intProductDetailId,intProductId,strProductImg,strProductName,strProductPrice,strCasesCovered,strBenefitType,strAgeEligibility,strNumberOfAvailments,strBenefitLimit,strDocProFee,strRoomAccommodation,strLaboratoryDiagnosticPro,strMedicinesAsMedicallyNeeded,strUseOfOperationRoom,strSurgerySurgonFees,strLaparoscopic,strMra,strMri,strCt,strTherapetic,strPainManagement,strArthoscopic,strOtherMedical,strOneTime,strUsage,strAccreditedHospitals,strMer,strAfr,strArp,strValidity,strWaiting,strNumberOfRegistrations,strUnlimitedTeleMed , strPreExistingConCover , strNonAccreditedHos , strReimbursementNonAccreditedHos , strTopSixHospitalAccess , strRegistrationOfSucceedingVouchers , strCombinability , strIndividualOrGroup , strPrepaidPlan , strConsultation , strInclusions , strSpecialModalities , strExclusions , strFtfconsultation , strTelemedicine , strDentalConsultation , strDentalServicesBenefit , strHospitalNetwork , strRegistrationRules,strMedicalCoverage,strLearnMoreBtnLink,strBuyNowBtnLink,strCoverage , strVoucherUsed , strVoucherUnused , strConsultationCards, strInPatient , strOutPatient")] ProductDetailsViewModel productDetailsViewModel)
		{
			var log = "";
			try
			{
				if (ModelState.IsValid)
				{

					var isSaved = PDS.SaveProductDetail(ref log, productDetailsViewModel);
					if (isSaved != "Saved")
					{
						ViewBag.error = isSaved;
					}
					return RedirectToAction(nameof(Index));
				}
				else
				{
					var products = PS.GetProductsList(ref log);
					ViewBag.products = products;
				}
				return View(productDetailsViewModel);
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				ViewBag.error = Comman.SomethingWntWrong;
				return View();
			}
		}

		// GET: ProductDetails/Edit/5
		public ActionResult Edit(int? id)
		{
			var log = "";
			try
			{
				if (id == null)
				{
					return NotFound();
				}
				var products = PS.GetProductsList(ref log);
				ViewBag.products = products;
				var proDetVM = PDS.GetProductDetailById(ref log, id);
				if (proDetVM == null)
				{
					return NotFound();
				}
				return View(proDetVM);
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				ViewBag.error = Comman.SomethingWntWrong;
				return NotFound();
			}
		}

		// POST: ProductDetails/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, [Bind("intProductDetailId,intProductId,strProductImg,strProductName,strProductPrice,strCasesCovered,strBenefitType,strAgeEligibility,strNumberOfAvailments,strBenefitLimit,strDocProFee,strRoomAccommodation,strLaboratoryDiagnosticPro,strMedicinesAsMedicallyNeeded,strUseOfOperationRoom,strSurgerySurgonFees,strLaparoscopic,strMra,strMri,strCt,strTherapetic,strPainManagement,strArthoscopic,strOtherMedical,strOneTime,strUsage,strAccreditedHospitals,strMer,strAfr,strArp,strValidity,strWaiting,strNumberOfRegistrations,strUnlimitedTeleMed , strPreExistingConCover , strNonAccreditedHos , strReimbursementNonAccreditedHos , strTopSixHospitalAccess , strRegistrationOfSucceedingVouchers , strCombinability , strIndividualOrGroup , strPrepaidPlan , strConsultation,strInclusions , strSpecialModalities , strExclusions , strFtfconsultation , strTelemedicine , strDentalConsultation , strDentalServicesBenefit , strHospitalNetwork , strRegistrationRules,strMedicalCoverage,strLearnMoreBtnLink,strBuyNowBtnLink,strCoverage , strVoucherUsed , strVoucherUnused , strConsultationCards, strInPatient , strOutPatient")] ProductDetailsViewModel productDetailsViewModel)
		{
			var log = "";
			if (ModelState.IsValid)
			{
				try
				{
					if (id != productDetailsViewModel.intProductDetailId)
					{
						return NotFound();
					}

					PDS.EditProductDetail(ref log, productDetailsViewModel);
				}
				catch (Exception ex)
				{
					string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
					var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
					lR.SaveExceptionLogs(exLog, ex, methodName);
					ViewBag.error = Comman.SomethingWntWrong;
					return View();
				}
				return RedirectToAction(nameof(Index));
			}
			return View(productDetailsViewModel);

		}


		// POST: Users/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		public ActionResult Delete(int id)
		{
			var log = "";
			try
			{
				PDS.Deactivate_DeleteProductDetail(ref log, id, true);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				ViewBag.error = Comman.SomethingWntWrong;
				return RedirectToAction(nameof(Index));
			}
		}

		// POST: Users/Delete/5
		//[HttpPost, ActionName("Deactive")]
		//[ValidateAntiForgeryToken]
		public ActionResult Deactive(int id)
		{
			var log = "";
			try
			{
				PDS.Deactivate_DeleteProductDetail(ref log, id, false);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				ViewBag.error = Comman.SomethingWntWrong;
				return RedirectToAction(nameof(Index));
			}
		}
	}
}
