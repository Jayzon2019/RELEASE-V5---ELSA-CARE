using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public class ProductDetailViewModel : BaseContentViewModel
	{
		private readonly IProductRepository productRepository;
		private readonly IProductDetailRepository productDetailRepository;

		public ProductDetailViewModel
		(
			IProductRepository productRepository,
			IProductDetailRepository productDetailRepository
		)
		{
			this.productRepository = productRepository;
			this.productDetailRepository = productDetailRepository;
		}

		public ProductDetailViewModel(ProductDetail model) : base(model)
		{
			this.ProductId = model.Product.Id;
			this.ProductImg = model.Product.ProductImg;
			this.ProductName = model.Product.ProductName;
			this.ProductPrice = model.Product.ProductPrice;
			this.ProductCode = model.Product.ProductCode;

			this.CasesCovered = model.CasesCovered;
			this.BenefitType = model.BenefitType;
			this.AgeEligibility = model.AgeEligibility;
			this.NumberOfAvailments = model.NumberOfAvailments;
			this.BenefitLimit = model.BenefitLimit;
			this.DocProFee = model.DocProFee;
			this.RoomAccommodation = model.RoomAccommodation;
			this.LaboratoryDiagnosticPro = model.LaboratoryDiagnosticPro;
			this.MedicinesAsMedicallyNeeded = model.MedicinesAsMedicallyNeeded;
			this.UseOfOperationRoom = model.UseOfOperationRoom;
			this.SurgerySurgonFees = model.SurgerySurgonFees;
			this.Laparoscopic = model.Laparoscopic;
			this.MRA = model.MRA;
			this.MRI = model.MRI;
			this.CT = model.CT;
			this.Therapetic = model.Therapetic;
			this.PainManagement = model.PainManagement;
			this.Arthoscopic = model.Arthoscopic;
			this.OtherMedical = model.OtherMedical;
			this.OneTime = model.OneTime;
			this.Usage = model.Usage;
			this.AccreditedHospitals = model.AccreditedHospitals;
			this.MER = model.MER;
			this.AFR = model.AFR;
			this.ARP = model.ARP;
			this.Validity = model.Validity;
			this.Waiting = model.Waiting;
			this.NumberOfRegistrations = model.NumberOfRegistrations;
			this.UnlimitedTeleMed = model.UnlimitedTeleMed;
			this.PreExistingConCover = model.PreExistingConCover;
			this.NonAccreditedHospitals = model.NonAccreditedHospitals;
			this.ReimbursementNonAccreditedHospitals = model.ReimbursementNonAccreditedHospitals;
			this.TopSixHospitalAccess = model.TopSixHospitalAccess;
			this.RegistrationOfSucceedingVouchers = model.RegistrationOfSucceedingVouchers;
			this.Combinability = model.Combinability;
			this.IndividualOrGroup = model.IndividualOrGroup;
			this.PrepaidPlan = model.PrepaidPlan;
			this.Consultation = model.Consultation;
			this.Inclusions = model.Inclusions;
			this.SpecialModalities = model.SpecialModalities;
			this.Exclusions = model.Exclusions;
			this.FTFConsultation = model.FTFConsultation;
			this.Telemedicine = model.Telemedicine;
			this.DentalConsultation = model.DentalConsultation;
			this.DentalServicesBenefit = model.DentalServicesBenefit;
			this.HospitalNetwork = model.HospitalNetwork;
			this.RegistrationRules = model.RegistrationRules;
			this.MedicalCoverage = model.MedicalCoverage;
			this.LearnMoreBtnLink = model.LearnMoreBtnLink;
			this.BuyNowBtnLink = model.BuyNowBtnLink;
			this.Coverage = model.Coverage;
			this.VoucherUsed = model.VoucherUsed;
			this.VoucherUnused = model.VoucherUnused;
			this.ConsultationCards = model.ConsultationCards;
			this.InPatient = model.InPatient;
			this.OutPatient = model.OutPatient;
		}

		public ProductDetail Map()
		{
			var model = this.productDetailRepository.Get(Id);

			if (model == null)
				model = new ProductDetail();

			return this.Map(model);
		}

		public ProductDetail Map(ProductDetail model)
		{
			model.Product = this.productRepository.Get(this.ProductId);

			model.CasesCovered = this.CasesCovered;
			model.BenefitType = this.BenefitType;
			model.AgeEligibility = this.AgeEligibility;
			model.NumberOfAvailments = this.NumberOfAvailments;
			model.BenefitLimit = this.BenefitLimit;
			model.DocProFee = this.DocProFee;
			model.RoomAccommodation = this.RoomAccommodation;
			model.LaboratoryDiagnosticPro = this.LaboratoryDiagnosticPro;
			model.MedicinesAsMedicallyNeeded = this.MedicinesAsMedicallyNeeded;
			model.UseOfOperationRoom = this.UseOfOperationRoom;
			model.SurgerySurgonFees = this.SurgerySurgonFees;
			model.Laparoscopic = this.Laparoscopic;
			model.MRA = this.MRA;
			model.MRI = this.MRI;
			model.CT = this.CT;
			model.Therapetic = this.Therapetic;
			model.PainManagement = this.PainManagement;
			model.Arthoscopic = this.Arthoscopic;
			model.OtherMedical = this.OtherMedical;
			model.OneTime = this.OneTime;
			model.Usage = this.Usage;
			model.AccreditedHospitals = this.AccreditedHospitals;
			model.MER = this.MER;
			model.AFR = this.AFR;
			model.ARP = this.ARP;
			model.Validity = this.Validity;
			model.Waiting = this.Waiting;
			model.NumberOfRegistrations = this.NumberOfRegistrations;
			model.UnlimitedTeleMed = this.UnlimitedTeleMed;
			model.PreExistingConCover = this.PreExistingConCover;
			model.NonAccreditedHospitals = this.NonAccreditedHospitals;
			model.ReimbursementNonAccreditedHospitals = this.ReimbursementNonAccreditedHospitals;
			model.TopSixHospitalAccess = this.TopSixHospitalAccess;
			model.RegistrationOfSucceedingVouchers = this.RegistrationOfSucceedingVouchers;
			model.Combinability = this.Combinability;
			model.IndividualOrGroup = this.IndividualOrGroup;
			model.PrepaidPlan = this.PrepaidPlan;
			model.Consultation = this.Consultation;
			model.Inclusions = this.Inclusions;
			model.SpecialModalities = this.SpecialModalities;
			model.Exclusions = this.Exclusions;
			model.FTFConsultation = this.FTFConsultation;
			model.Telemedicine = this.Telemedicine;
			model.DentalConsultation = this.DentalConsultation;
			model.DentalServicesBenefit = this.DentalServicesBenefit;
			model.HospitalNetwork = this.HospitalNetwork;
			model.RegistrationRules = this.RegistrationRules;
			model.MedicalCoverage = this.MedicalCoverage;
			model.LearnMoreBtnLink = this.LearnMoreBtnLink;
			model.BuyNowBtnLink = this.BuyNowBtnLink;
			model.Coverage = this.Coverage;
			model.VoucherUsed = this.VoucherUsed;
			model.VoucherUnused = this.VoucherUnused;
			model.ConsultationCards = this.ConsultationCards;
			model.InPatient = this.InPatient;
			model.OutPatient = this.OutPatient;

			return model;
		}


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
