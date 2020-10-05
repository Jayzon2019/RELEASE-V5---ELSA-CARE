using InLifeCMS.Helpers;
using InLifeCMS.Services;
using InLifeCMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using InLife.Store.Core.Models;

namespace InLifeCMS.Repos
{
    public class ProductDetailRepo
    {
       //dbinlifecmshostContext db = new dbinlifecmshostContext();
        InLifePrimeCareStoreContext db = new InLifePrimeCareStoreContext();
        LogsRepo lR = new LogsRepo();
        LogsService LS = new LogsService();

        public List<ProductDetailsViewModel> GetProductDetailsList(ref string log)
        {
            try
            {
                var productDetailsList = (from p in db.TblProducts
                                          join proDet in db.TblProductDetails on p.ProductId equals proDet.ProductId
                                          where proDet.IsActive == true
                                          orderby p.CreatedDate descending
                                          select new ProductDetailsViewModel
                                          {
                                              intProductId = proDet.ProductId,
                                              strProductName = p.ProductName,
                                              strProductImg = p.ProductImg,
                                              strProductCode = p.ProductCode,
                                              strProductPrice = p.ProductPrice,
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
                                              dteCreatedDate = proDet.CreatedDate,
                                              dteUpdatedDate = proDet.UpdatedDate,
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
                                              strInclusions = proDet.Inclusions,
                                              strSpecialModalities = proDet.SpecialModalities,
                                              strExclusions = proDet.Exclusions,
                                              strFtfconsultation = proDet.Ftfconsultation,
                                              strTelemedicine = proDet.Telemedicine,
                                              strDentalConsultation = proDet.DentalConsultation,
                                              strDentalServicesBenefit = proDet.DentalServicesBenefit,
                                              strHospitalNetwork = proDet.HospitalNetwork,
                                              strRegistrationRules = proDet.RegistrationRules,
                                              strMedicalCoverage = proDet.MedicalCoverage,
                                              strLearnMoreBtnLink = proDet.LearnMoreBtnLink,
                                              strBuyNowBtnLink = proDet.BuyNowBtnLink,
                                              strCoverage = proDet.Coverage,
                                              strVoucherUsed = proDet.VoucherUsed,
                                              strVoucherUnused = proDet.VoucherUnused,
                                              strConsultationCards = proDet.ConsultationCards,
                                              strInPatient = proDet.InPatient,
                                              strOutPatient = proDet.OutPatient
                                          }).ToList();

                return productDetailsList;

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void SaveProductDetail(ref string log, TblProductDetails proDet)
        {
            try
            {
                var AddedProductDetail = db.TblProductDetails.Add(proDet);
                db.SaveChanges();
                if (AddedProductDetail.Entity.ProductDetailId > 0)
                {
                    var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Added.ToString(), "a Product's detail", AddedProductDetail.Entity.ProductDetailId);
                    LS.SaveActivityLogs(Comman.ActivityActions.Added.ToString(), activityLog);
                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

        public TblProductDetails GetProductDetailById(ref string log, int? id)
        {
            try
            {
                var proDet = db.TblProductDetails.Where(x => x.ProductDetailId == id && x.IsActive == true).FirstOrDefault();
                return proDet;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void EditProductDetail(ref string log, TblProductDetails proDet)
        {
            try
            {
                var oldProDet = db.TblProductDetails.Where(x => x.ProductDetailId == proDet.ProductDetailId && x.IsArchived == false && x.IsActive == true).FirstOrDefault();
                oldProDet.ProductId = proDet.ProductId;
                oldProDet.AccreditedHospitals = proDet.AccreditedHospitals;
                oldProDet.Afr = proDet.Afr;
                oldProDet.AgeEligibility = proDet.AgeEligibility;
                oldProDet.Arp = proDet.Arp;
                oldProDet.Arthoscopic = proDet.Arthoscopic;
                oldProDet.BenefitLimit = proDet.BenefitLimit;
                oldProDet.BenefitType = proDet.BenefitType;
                oldProDet.CasesCovered = proDet.CasesCovered;
                oldProDet.Ct = proDet.Ct;
                oldProDet.DocProFee = proDet.DocProFee;
                oldProDet.LaboratoryDiagnosticPro = proDet.LaboratoryDiagnosticPro;
                oldProDet.Laparoscopic = proDet.Laparoscopic;
                oldProDet.MedicinesAsMedicallyNeeded = proDet.MedicinesAsMedicallyNeeded;
                oldProDet.Mer = proDet.Mer;
                oldProDet.Mra = proDet.Mra;
                oldProDet.Mri = proDet.Mri;
                oldProDet.NumberOfAvailments = proDet.NumberOfAvailments;
                oldProDet.NumberOfRegistrations = proDet.NumberOfRegistrations;
                oldProDet.OneTime = proDet.OneTime;
                oldProDet.OtherMedical = proDet.OtherMedical;
                oldProDet.PainManagement = proDet.PainManagement;
                oldProDet.ProductDetailId = proDet.ProductDetailId;
                oldProDet.RoomAccommodation = proDet.RoomAccommodation;
                oldProDet.SurgerySurgonFees = proDet.SurgerySurgonFees;
                oldProDet.Therapetic = proDet.Therapetic;
                oldProDet.Usage = proDet.Usage;
                oldProDet.UseOfOperationRoom = proDet.UseOfOperationRoom;
                oldProDet.Validity = proDet.Validity;
                oldProDet.Waiting = proDet.Waiting;
                oldProDet.UnlimitedTeleMed = proDet.UnlimitedTeleMed;
                oldProDet.PreExistingConCover = proDet.PreExistingConCover;
                oldProDet.NonAccreditedHos = proDet.NonAccreditedHos;
                oldProDet.ReimbursementNonAccreditedHos = proDet.ReimbursementNonAccreditedHos;
                oldProDet.TopSixHospitalAccess = proDet.TopSixHospitalAccess;
                oldProDet.RegistrationOfSucceedingVouchers = proDet.RegistrationOfSucceedingVouchers;
                oldProDet.Combinability = proDet.Combinability;
                oldProDet.IndividualOrGroup = proDet.IndividualOrGroup;
                oldProDet.PrepaidPlan = proDet.PrepaidPlan;
                oldProDet.Consultation = proDet.Consultation;
                oldProDet.UpdatedBy = proDet.UpdatedBy;
                oldProDet.UpdatedDate = proDet.UpdatedDate;
                oldProDet.Inclusions = proDet.Inclusions;
                oldProDet.SpecialModalities = proDet.SpecialModalities;
                oldProDet.Exclusions = proDet.Exclusions;
                oldProDet.Ftfconsultation = proDet.Ftfconsultation;
                oldProDet.Telemedicine = proDet.Telemedicine;
                oldProDet.DentalConsultation = proDet.DentalConsultation;
                oldProDet.DentalServicesBenefit = proDet.DentalServicesBenefit;
                oldProDet.HospitalNetwork = proDet.HospitalNetwork;
                oldProDet.RegistrationRules = proDet.RegistrationRules;
                oldProDet.MedicalCoverage = proDet.MedicalCoverage;
                oldProDet.LearnMoreBtnLink = proDet.LearnMoreBtnLink;
                oldProDet.BuyNowBtnLink = proDet.BuyNowBtnLink;
                oldProDet.Coverage = proDet.Coverage;
                oldProDet.VoucherUsed = proDet.VoucherUsed;
                oldProDet.VoucherUnused = proDet.VoucherUnused;
                oldProDet.ConsultationCards = proDet.ConsultationCards;
                oldProDet.InPatient = proDet.InPatient;
                oldProDet.OutPatient = proDet.OutPatient;
                db.TblProductDetails.Update(oldProDet);
                db.SaveChanges();
                var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Updated.ToString(), "Product Detail Entery", oldProDet.ProductDetailId);
                LS.SaveActivityLogs(Comman.ActivityActions.Updated.ToString(), activityLog);
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
                if (delete)
                {
                    var proDet = db.TblProductDetails.Where(x => x.IsActive == true && x.ProductDetailId == id).FirstOrDefault();
                    if (proDet != null)
                    {
                        proDet.IsArchived = true;
                        proDet.IsActive = false;
                        db.TblProductDetails.Update(proDet);
                        db.SaveChanges();
                        var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deleted.ToString(), "Product Detail Entery", proDet.ProductDetailId);
                        LS.SaveActivityLogs(Comman.ActivityActions.Deleted.ToString(), activityLog);
                    }
                }
                else
                {
                    var proDet = db.TblProductDetails.Where(x => x.IsActive == true && x.ProductDetailId == id).FirstOrDefault();
                    if (proDet != null)
                    {
                        proDet.IsArchived = false;
                        proDet.IsActive = true;
                        db.TblProductDetails.Update(proDet);
                        db.SaveChanges();
                        var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deactivated.ToString(), "Product Entery", proDet.ProductDetailId);
                        LS.SaveActivityLogs(Comman.ActivityActions.Deactivated.ToString(), activityLog);
                    }
                }
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
