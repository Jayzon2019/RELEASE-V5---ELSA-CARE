using System;
using System.Collections.Generic;

namespace InLifeCMS.Models
{
    public partial class TblProductDetails
    {
        public int ProductDetailId { get; set; }
        public int ProductId { get; set; }
        public string CasesCovered { get; set; }
        public string BenefitType { get; set; }
        public string AgeEligibility { get; set; }
        public string NumberOfAvailments { get; set; }
        public string BenefitLimit { get; set; }
        public string DocProFee { get; set; }
        public string RoomAccommodation { get; set; }
        public string LaboratoryDiagnosticPro { get; set; }
        public string MedicinesAsMedicallyNeeded { get; set; }
        public string UseOfOperationRoom { get; set; }
        public string SurgerySurgonFees { get; set; }
        public string Laparoscopic { get; set; }
        public string Mra { get; set; }
        public string Mri { get; set; }
        public string Ct { get; set; }
        public string Therapetic { get; set; }
        public string PainManagement { get; set; }
        public string Arthoscopic { get; set; }
        public string OtherMedical { get; set; }
        public string OneTime { get; set; }
        public string Usage { get; set; }
        public string AccreditedHospitals { get; set; }
        public string Mer { get; set; }
        public string Afr { get; set; }
        public string Arp { get; set; }
        public string Validity { get; set; }
        public string Waiting { get; set; }
        public string NumberOfRegistrations { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
        public string UnlimitedTeleMed { get; set; }
        public string PreExistingConCover { get; set; }
        public string NonAccreditedHos { get; set; }
        public string ReimbursementNonAccreditedHos { get; set; }
        public string TopSixHospitalAccess { get; set; }
        public string RegistrationOfSucceedingVouchers { get; set; }
        public string Combinability { get; set; }
        public string IndividualOrGroup { get; set; }
        public string PrepaidPlan { get; set; }
        public string Consultation { get; set; }
        public string Inclusions { get; set; }
        public string SpecialModalities { get; set; }
        public string Exclusions { get; set; }
        public string Ftfconsultation { get; set; }
        public string Telemedicine { get; set; }
        public string DentalConsultation { get; set; }
        public string DentalServicesBenefit { get; set; }
        public string HospitalNetwork { get; set; }
        public string RegistrationRules { get; set; }
        public string MedicalCoverage { get; set; }
        public string LearnMoreBtnLink { get; set; }
        public string BuyNowBtnLink { get; set; }
        public string Coverage { get; set; }
        public string VoucherUsed { get; set; }
        public string VoucherUnused { get; set; }
        public string ConsultationCards { get; set; }
        public string InPatient { get; set; }
        public string OutPatient { get; set; }
    }
}
