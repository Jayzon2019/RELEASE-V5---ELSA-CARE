using System;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class ProductDetailResponse : BaseResponse
	{
		public ProductDetailResponse()
		{
		}

		public ProductDetailResponse(ProductDetail model)
		{
			Id = model.Id;

			ProductId = model.Product.Id;

			// This is a hack, old uploaded images doesn't have an image data
			// Clean this up when StoreFront has been updated
			ProductImg = ParseImageData(model.Product.ProductImg);

			ProductName = model.Product.ProductName;
			ProductPrice = model.Product.ProductPrice;
			ProductCode = model.Product.ProductCode;

			CasesCovered = model.CasesCovered;
			BenefitType = model.BenefitType;
			AgeEligibility = model.AgeEligibility;
			NumberOfAvailments = model.NumberOfAvailments;
			BenefitLimit = model.BenefitLimit;
			DocProFee = model.DocProFee;
			RoomAccommodation = model.RoomAccommodation;
			LaboratoryDiagnosticPro = model.LaboratoryDiagnosticPro;
			MedicinesAsMedicallyNeeded = model.MedicinesAsMedicallyNeeded;
			UseOfOperationRoom = model.UseOfOperationRoom;
			SurgerySurgonFees = model.SurgerySurgonFees;
			Laparoscopic = model.Laparoscopic;
			MRA = model.MRA;
			MRI = model.MRI;
			CT = model.CT;
			Therapetic = model.Therapetic;
			PainManagement = model.PainManagement;
			Arthoscopic = model.Arthoscopic;
			OtherMedical = model.OtherMedical;
			OneTime = model.OneTime;
			Usage = model.Usage;
			AccreditedHospitals = model.AccreditedHospitals;
			MER = model.MER;
			AFR = model.AFR;
			ARP = model.ARP;
			Validity = model.Validity;
			Waiting = model.Waiting;
			NumberOfRegistrations = model.NumberOfRegistrations;
			UnlimitedTeleMed = model.UnlimitedTeleMed;
			PreExistingConCover = model.PreExistingConCover;
			NonAccreditedHospitals = model.NonAccreditedHospitals;
			ReimbursementNonAccreditedHospitals = model.ReimbursementNonAccreditedHospitals;
			TopSixHospitalAccess = model.TopSixHospitalAccess;
			RegistrationOfSucceedingVouchers = model.RegistrationOfSucceedingVouchers;
			Combinability = model.Combinability;
			IndividualOrGroup = model.IndividualOrGroup;
			PrepaidPlan = model.PrepaidPlan;
			Consultation = model.Consultation;
			Inclusions = model.Inclusions;
			SpecialModalities = model.SpecialModalities;
			Exclusions = model.Exclusions;
			FTFConsultation = model.FTFConsultation;
			Telemedicine = model.Telemedicine;
			DentalConsultation = model.DentalConsultation;
			DentalServicesBenefit = model.DentalServicesBenefit;
			HospitalNetwork = model.HospitalNetwork;
			RegistrationRules = model.RegistrationRules;
			MedicalCoverage = model.MedicalCoverage;
			LearnMoreBtnLink = model.LearnMoreBtnLink;
			BuyNowBtnLink = model.BuyNowBtnLink;
			Coverage = model.Coverage;
			VoucherUsed = model.VoucherUsed;
			VoucherUnused = model.VoucherUnused;
			ConsultationCards = model.ConsultationCards;
			InPatient = model.InPatient;
			OutPatient = model.OutPatient;
		}

		public int Id { get; set; }

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

		public string NonAccreditedHospitals { get; set; }

		public string ReimbursementNonAccreditedHospitals { get; set; }

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
