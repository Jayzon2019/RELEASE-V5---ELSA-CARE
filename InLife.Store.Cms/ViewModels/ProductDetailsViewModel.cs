using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InLife.Store.Cms.ViewModels
{
	public class ProductDetailsViewModel : BaseContentViewModel
	{
		#region Product

		public int ProductId { get; set; }

		public string ProductImg { get; set; }

		public string ProductName { get; set; }

		public string ProductPrice { get; set; }

		public string ProductCode { get; set; }

		#endregion

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

		public string MRA { get; set; }

		public string MRI { get; set; }

		public string CT { get; set; }

		public string Therapetic { get; set; }

		public string PainManagement { get; set; }

		public string Arthoscopic { get; set; }

		public string OtherMedical { get; set; }

		public string OneTime { get; set; }

		public string Usage { get; set; }

		public string AccreditedHospitals { get; set; }

		public string MER { get; set; }

		public string AFR { get; set; }

		public string ARP { get; set; }

		public string Validity { get; set; }

		public string Waiting { get; set; }

		public string NumberOfRegistrations { get; set; }

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

		public string FTFConsultation { get; set; }

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
