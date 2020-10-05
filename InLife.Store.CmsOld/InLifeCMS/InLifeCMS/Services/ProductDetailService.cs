using InLifeCMS.Helpers;
using InLifeCMS.Models;
using InLifeCMS.Repos;
using InLifeCMS.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLifeCMS.Services
{
    public class ProductDetailService
    {
        ProductDetailRepo PDR = new ProductDetailRepo();
        ProductsRepo PR = new ProductsRepo();
        LogsRepo lR = new LogsRepo();
        UsersRepo UR = new UsersRepo();

        private static IHttpContextAccessor httpContextAccessor;
        public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }

        public List<ProductDetailsViewModel> GetProductDetailsList(ref string log)
        {
            try
            {
                var proDetList = PDR.GetProductDetailsList(ref log);
                return proDetList;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }

        }

        public string SaveProductDetail(ref string log, ProductDetailsViewModel ProDetVM)
        {
            try
            {
                TblProductDetails ProDet = new TblProductDetails
                {
                    CreatedDate = DateTime.Now,
                    CreatedBy = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value),
                    IsActive = true,
                    ProductId = ProDetVM.intProductId,
                    Afr = ProDetVM.strAfr,
                    AgeEligibility = ProDetVM.strAgeEligibility,
                    Arp = ProDetVM.strArp,
                    BenefitLimit = ProDetVM.strBenefitLimit,
                    BenefitType = ProDetVM.strBenefitType,
                    CasesCovered = ProDetVM.strCasesCovered,
                    Mer = ProDetVM.strMer,
                    NumberOfAvailments = ProDetVM.strNumberOfAvailments,
                    NumberOfRegistrations = ProDetVM.strNumberOfRegistrations,
                    RoomAccommodation = ProDetVM.strRoomAccommodation,
                    Usage = ProDetVM.strUsage,
                    Validity = ProDetVM.strValidity,
                    Waiting = ProDetVM.strWaiting,
                    AccreditedHospitals = ProDetVM.strAccreditedHospitals,
                    Arthoscopic = ProDetVM.strArthoscopic,
                    Ct = ProDetVM.strCt,
                    DocProFee = ProDetVM.strDocProFee,
                    LaboratoryDiagnosticPro = ProDetVM.strLaboratoryDiagnosticPro,
                    Laparoscopic = ProDetVM.strLaparoscopic,
                    MedicinesAsMedicallyNeeded = ProDetVM.strMedicinesAsMedicallyNeeded,
                    Mra = ProDetVM.strMra,
                    Mri = ProDetVM.strMri,
                    OneTime = ProDetVM.strOneTime,
                    OtherMedical = ProDetVM.strOtherMedical,
                    PainManagement = ProDetVM.strPainManagement,
                    SurgerySurgonFees = ProDetVM.strSurgerySurgonFees,
                    Therapetic = ProDetVM.strTherapetic,
                    UseOfOperationRoom = ProDetVM.strUseOfOperationRoom,
                    UnlimitedTeleMed = ProDetVM.strUnlimitedTeleMed,
                    PreExistingConCover = ProDetVM.strPreExistingConCover,
                    NonAccreditedHos = ProDetVM.strNonAccreditedHos,
                    ReimbursementNonAccreditedHos = ProDetVM.strReimbursementNonAccreditedHos,
                    TopSixHospitalAccess = ProDetVM.strTopSixHospitalAccess,
                    RegistrationOfSucceedingVouchers = ProDetVM.strRegistrationOfSucceedingVouchers,
                    Combinability = ProDetVM.strCombinability,
                    IndividualOrGroup = ProDetVM.strIndividualOrGroup,
                    PrepaidPlan = ProDetVM.strPrepaidPlan,
                    Consultation = ProDetVM.strConsultation,
                    Inclusions = ProDetVM.strInclusions,
                    SpecialModalities = ProDetVM.strSpecialModalities,
                    Exclusions = ProDetVM.strExclusions,
                    Ftfconsultation = ProDetVM.strFtfconsultation,
                    Telemedicine = ProDetVM.strTelemedicine,
                    DentalConsultation = ProDetVM.strDentalConsultation,
                    DentalServicesBenefit = ProDetVM.strDentalServicesBenefit,
                    HospitalNetwork = ProDetVM.strHospitalNetwork,
                    RegistrationRules = ProDetVM.strRegistrationRules,
                    MedicalCoverage = ProDetVM.strMedicalCoverage,
                    LearnMoreBtnLink = ProDetVM.strLearnMoreBtnLink,
                    BuyNowBtnLink = ProDetVM.strBuyNowBtnLink,
                   Coverage = ProDetVM.strCoverage,
                   VoucherUsed = ProDetVM.strVoucherUsed,
                   VoucherUnused = ProDetVM.strVoucherUnused,
                  ConsultationCards =  ProDetVM.strConsultationCards,
                  InPatient =  ProDetVM.strInPatient,
                   OutPatient = ProDetVM.strOutPatient
                };


                PDR.SaveProductDetail(ref log, ProDet);
                return "Saved";
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return Comman.SomethingWntWrong;
            }
        }


        public ProductDetailsViewModel GetProductDetailById(ref string log, int? id)
        {
            try
            {
                var proDet = PDR.GetProductDetailById(ref log, id);

                ProductDetailsViewModel PDVM = new ProductDetailsViewModel
                {
                    intProductId = proDet.ProductId,
                    strAccreditedHospitals = proDet.AccreditedHospitals,
                    strArthoscopic = proDet.Arthoscopic,
                    strCt = proDet.Ct,
                    strDocProFee = proDet.DocProFee,
                    blnIsActive = proDet.IsActive,
                    blnIsArchived = proDet.IsArchived,
                    strLaboratoryDiagnosticPro = proDet.LaboratoryDiagnosticPro,
                    strLaparoscopic = proDet.Laparoscopic,
                    strMedicinesAsMedicallyNeeded = proDet.MedicinesAsMedicallyNeeded,
                    strMra = proDet.Mra,
                    strMri = proDet.Mri,
                    strOneTime = proDet.OneTime,
                    strOtherMedical = proDet.OtherMedical,
                    strPainManagement = proDet.PainManagement,
                    strSurgerySurgonFees = proDet.SurgerySurgonFees,
                    strTherapetic = proDet.Therapetic,
                    strUseOfOperationRoom = proDet.UseOfOperationRoom,
                 
                    intCreatedBy = proDet.CreatedBy,
                    intProductDetailId = proDet.ProductDetailId,
                    intUpdatedBy = proDet.UpdatedBy,
                    strAfr = proDet.Afr,
                    strAgeEligibility = proDet.AgeEligibility,
                    strArp = proDet.Arp,
                    strBenefitLimit = proDet.BenefitLimit,
                    strBenefitType = proDet.BenefitType,
                    strCasesCovered = proDet.CasesCovered,
                    strMer = proDet.Mer,
                    strNumberOfAvailments = proDet.NumberOfAvailments,
                    strNumberOfRegistrations = proDet.NumberOfRegistrations,
                    strRoomAccommodation = proDet.RoomAccommodation,
                    strUsage = proDet.Usage,
                    strValidity = proDet.Validity,
                    strWaiting = proDet.Waiting,
                    strUnlimitedTeleMed = proDet.UnlimitedTeleMed,
                    strPreExistingConCover = proDet.PreExistingConCover,
                    strNonAccreditedHos = proDet.NonAccreditedHos,
                    strReimbursementNonAccreditedHos = proDet.ReimbursementNonAccreditedHos,
                    strTopSixHospitalAccess = proDet.TopSixHospitalAccess,
                    strRegistrationOfSucceedingVouchers = proDet.RegistrationOfSucceedingVouchers,
                    strCombinability = proDet.Combinability,
                    strIndividualOrGroup = proDet.IndividualOrGroup,
                    strPrepaidPlan = proDet.PrepaidPlan,
                    strConsultation = proDet.Consultation,
                    strMedicalCoverage = proDet.MedicalCoverage,
                    strLearnMoreBtnLink = proDet.LearnMoreBtnLink,
                    strBuyNowBtnLink = proDet.BuyNowBtnLink,
                    strCoverage = proDet.Coverage,
                    strVoucherUsed = proDet.VoucherUsed,
                    strVoucherUnused = proDet.VoucherUnused,
                    strConsultationCards = proDet.ConsultationCards,
                    strInPatient = proDet.InPatient,
                    strOutPatient = proDet.OutPatient

                };
                if (proDet.CreatedDate != null)
                {
                    PDVM.dteCreatedDate = Comman.getClientTime(proDet.CreatedDate.ToString());
                }
                if (proDet.UpdatedDate != null)
                {
                    PDVM.dteUpdatedDate = Comman.getClientTime(proDet.UpdatedDate.ToString());
                }
                var createdBy = UR.GETUserCreatedBy_UpdatedBy(ref log, Convert.ToInt32(PDVM.intCreatedBy));
                PDVM.strCreatedByUser = createdBy;
                if (PDVM.intUpdatedBy > 0)
                {
                    if (PDVM.intCreatedBy != PDVM.intUpdatedBy)
                    {
                        int uId = Convert.ToInt32(PDVM.intUpdatedBy);
                        PDVM.strUpdatedByUser = UR.GETUserCreatedBy_UpdatedBy(ref log, uId);
                    }
                    else
                    {
                        PDVM.strUpdatedByUser = createdBy;
                    }
                }
                var pro = PR.GetProductById(ref log, PDVM.intProductId);
                PDVM.strProductName = pro.ProductName;
                PDVM.strProductImg = pro.ProductImg;
                PDVM.strProductCode = pro.ProductCode;
                PDVM.strProductPrice = pro.ProductPrice;
                return PDVM;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void EditProductDetail(ref string log, ProductDetailsViewModel ProDetVM)
        {
            try
            {
                // string uploadPath = "images/ProductImgs";
                TblProductDetails pd = new TblProductDetails
                {
                    ProductId = ProDetVM.intProductId,
                    ProductDetailId = ProDetVM.intProductDetailId,
                    AccreditedHospitals = ProDetVM.strAccreditedHospitals,
                    Arthoscopic = ProDetVM.strArthoscopic,
                    Ct = ProDetVM.strCt,
                    DocProFee = ProDetVM.strDocProFee,
                    LaboratoryDiagnosticPro = ProDetVM.strLaboratoryDiagnosticPro,
                    Laparoscopic = ProDetVM.strLaparoscopic,
                    MedicinesAsMedicallyNeeded = ProDetVM.strMedicinesAsMedicallyNeeded,
                    Mra = ProDetVM.strMra,
                    Mri = ProDetVM.strMri,
                    OneTime = ProDetVM.strOneTime,
                    OtherMedical = ProDetVM.strOtherMedical,
                    PainManagement = ProDetVM.strPainManagement,
                    SurgerySurgonFees = ProDetVM.strSurgerySurgonFees,
                    Therapetic = ProDetVM.strTherapetic,
                    UseOfOperationRoom = ProDetVM.strUseOfOperationRoom,
                    Afr = ProDetVM.strAfr,
                    AgeEligibility = ProDetVM.strAgeEligibility,
                    Arp = ProDetVM.strArp,
                    BenefitLimit = ProDetVM.strBenefitLimit,
                    BenefitType = ProDetVM.strBenefitType,
                    CasesCovered = ProDetVM.strCasesCovered,
                    Mer = ProDetVM.strMer,
                    NumberOfAvailments = ProDetVM.strNumberOfAvailments,
                    NumberOfRegistrations = ProDetVM.strNumberOfRegistrations,
                    RoomAccommodation = ProDetVM.strRoomAccommodation,
                    Usage = ProDetVM.strUsage,
                    Validity = ProDetVM.strValidity,
                    Waiting = ProDetVM.strWaiting,
                    UnlimitedTeleMed = ProDetVM.strUnlimitedTeleMed,
                    PreExistingConCover = ProDetVM.strPreExistingConCover,
                    NonAccreditedHos = ProDetVM.strNonAccreditedHos,
                    ReimbursementNonAccreditedHos = ProDetVM.strReimbursementNonAccreditedHos,
                    TopSixHospitalAccess = ProDetVM.strTopSixHospitalAccess,
                    RegistrationOfSucceedingVouchers = ProDetVM.strRegistrationOfSucceedingVouchers,
                    Combinability = ProDetVM.strCombinability,
                    IndividualOrGroup = ProDetVM.strIndividualOrGroup,
                    PrepaidPlan = ProDetVM.strPrepaidPlan,
                    Consultation = ProDetVM.strConsultation,
                    Inclusions = ProDetVM.strInclusions,
                    SpecialModalities = ProDetVM.strSpecialModalities,
                    Exclusions = ProDetVM.strExclusions,
                    Ftfconsultation = ProDetVM.strFtfconsultation,
                    Telemedicine = ProDetVM.strTelemedicine,
                    DentalConsultation = ProDetVM.strDentalConsultation,
                    DentalServicesBenefit = ProDetVM.strDentalServicesBenefit,
                    HospitalNetwork = ProDetVM.strHospitalNetwork,
                    RegistrationRules = ProDetVM.strRegistrationRules,
                    MedicalCoverage = ProDetVM.strMedicalCoverage,
                    LearnMoreBtnLink = ProDetVM.strLearnMoreBtnLink,
                    BuyNowBtnLink = ProDetVM.strBuyNowBtnLink,
                    Coverage = ProDetVM.strCoverage,
                    VoucherUsed = ProDetVM.strVoucherUsed,
                    VoucherUnused = ProDetVM.strVoucherUnused,
                    ConsultationCards = ProDetVM.strConsultationCards,
                    InPatient = ProDetVM.strInPatient,
                    OutPatient = ProDetVM.strOutPatient,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value)
                };
                
                PDR.EditProductDetail(ref log, pd);

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

        public void Deactivate_DeleteProductDetail(ref string log, int id, bool delete)
        {
            try
            {
                PDR.Deactivate_DeleteProductDetail(ref log, id, delete);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

    }
}
