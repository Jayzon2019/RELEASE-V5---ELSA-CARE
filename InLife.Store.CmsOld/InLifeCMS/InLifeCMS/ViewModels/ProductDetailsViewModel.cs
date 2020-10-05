using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InLifeCMS.ViewModels
{
    public partial class ProductDetailsViewModel
    {
        [Key]
        public int intProductDetailId { get; set; }
        public int intProductId { get; set; }
        public string strProductImg { get; set; }
        public string strProductName { get; set; }
        public string strProductPrice { get; set; }
        public string strProductCode { get; set; }
        public string strCasesCovered { get; set; }
        public string strBenefitType { get; set; }
        public string strAgeEligibility { get; set; }
        public string strNumberOfAvailments { get; set; }
        public string strBenefitLimit { get; set; }
        public string strDocProFee { get; set; }
        public string strRoomAccommodation { get; set; }
        public string strLaboratoryDiagnosticPro { get; set; }
        public string strMedicinesAsMedicallyNeeded { get; set; }
        public string strUseOfOperationRoom { get; set; }
        public string strSurgerySurgonFees { get; set; }
        public string strLaparoscopic { get; set; }
        public string strMra { get; set; }
        public string strMri { get; set; }
        public string strCt { get; set; }
        public string strTherapetic { get; set; }
        public string strPainManagement { get; set; }
        public string strArthoscopic { get; set; }
        public string strOtherMedical { get; set; }
        public string strOneTime { get; set; }
        public string strUsage { get; set; }
        public string strAccreditedHospitals { get; set; }
        public string strMer { get; set; }
        public string strAfr { get; set; }
        public string strArp { get; set; }
        public string strValidity { get; set; }
        public string strWaiting { get; set; }
        public string strNumberOfRegistrations { get; set; }
        public DateTime dteCreatedDate { get; set; }
        public int intCreatedBy { get; set; }
        public string strCreatedByUser { get; set; }
        public DateTime? dteUpdatedDate { get; set; }
        public int? intUpdatedBy { get; set; }
        public string strUpdatedByUser { get; set; }
        public bool blnIsActive { get; set; }
        public bool blnIsArchived { get; set; }

        public string strUnlimitedTeleMed { get; set; }
        public string strPreExistingConCover { get; set; }
        public string strNonAccreditedHos { get; set; }
        public string strReimbursementNonAccreditedHos { get; set; }
        public string strTopSixHospitalAccess { get; set; }
        public string strRegistrationOfSucceedingVouchers { get; set; }
        public string strCombinability { get; set; }
        public string strIndividualOrGroup { get; set; }
        public string strPrepaidPlan { get; set; }
        public string strConsultation { get; set; }

        public string strInclusions { get; set; }
        public string strSpecialModalities { get; set; }
        public string strExclusions { get; set; }
        public string strFtfconsultation { get; set; }
        public string strTelemedicine { get; set; }
        public string strDentalConsultation { get; set; }
        public string strDentalServicesBenefit { get; set; }
        public string strHospitalNetwork { get; set; }
        public string strRegistrationRules { get; set; }

        public string strMedicalCoverage { get; set; }

        public string strLearnMoreBtnLink { get; set; }
        public string strBuyNowBtnLink { get; set; }

        public string strCoverage { get; set; }
        public string strVoucherUsed { get; set; }
        public string strVoucherUnused { get; set; }
        public string strConsultationCards { get; set; }
        public string strInPatient { get; set; }
        public string strOutPatient { get; set; }

    }
}
