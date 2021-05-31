using System;
using System.ComponentModel.DataAnnotations;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class PrimeSecureQuoteRequest
	{
		public PrimeSecureQuoteForm Map(PrimeSecureQuoteForm model = null)
		{
			if (model == null)
				model = new PrimeSecureQuoteForm();

			model.PlanCode = PlanCode;
			model.PlanName = PlanName;
			model.PlanFaceAmount = PlanFaceAmount;
			model.PlanPremium = PlanPremium;

			model.CustomerNamePrefix = CustomerNamePrefix;
			model.CustomerNameSuffix = CustomerNameSuffix;
			model.CustomerFirstName = CustomerFirstName;
			model.CustomerMiddleName = CustomerMiddleName;
			model.CustomerLastName = CustomerLastName;
			model.CustomerPhoneNumber = CustomerPhoneNumber;
			model.CustomerMobileNumber = CustomerMobileNumber;
			model.CustomerEmailAddress = CustomerEmailAddress;

			model.Height = Height;
			model.Weight = Weight;

			model.Company = Company;
			model.Occupation = Occupation;
			model.IncomeSource = IncomeSource;
			model.IncomeAmount = IncomeAmount;

			model.AddressCity = AddressCity;
			model.AddressRegion = AddressRegion;
			model.AddressCountry = AddressCountry;

			model.HealthDeclaration1 = HealthDeclaration1;
			model.HealthDeclaration2 = HealthDeclaration2;
			model.HealthDeclaration3 = HealthDeclaration3;
			model.HealthDeclaration4 = HealthDeclaration4;

			model.CovidQuestion1 = CovidQuestion1;
			model.CovidQuestion2 = CovidQuestion2;
			model.CovidQuestion3 = CovidQuestion3;
			model.CovidQuestion4 = CovidQuestion4;

			return model;
		}


		#region Product Details

		[Required]
		[StringLength(50)]
		public string PlanCode { get; set; }

		[Required]
		[StringLength(50)]
		public string PlanName { get; set; }

		[Required]
		public decimal PlanFaceAmount { get; set; }

		[Required]
		public decimal PlanPremium { get; set; }

		#endregion Product Details

		#region Customer Details

		[StringLength(20)]
		public string CustomerNamePrefix { get; set; }

		[StringLength(20)]
		public string CustomerNameSuffix { get; set; }

		[Required]
		[StringLength(50)]
		public string CustomerFirstName { get; set; }

		[StringLength(50)]
		public string CustomerMiddleName { get; set; }

		[Required]
		[StringLength(50)]
		public string CustomerLastName { get; set; }

		[StringLength(20)]
		public string CustomerPhoneNumber { get; set; }

		[Required]
		[StringLength(20)]
		[Phone]
		public string CustomerMobileNumber { get; set; }

		[Required]
		[StringLength(300)]
		[EmailAddress]
		public string CustomerEmailAddress { get; set; }

		public int Height { get; set; }

		public int Weight { get; set; }

		#endregion Customer Details

		#region Income

		[StringLength(300)]
		public string Company { get; set; }

		[StringLength(50)]
		public string Occupation { get; set; }

		[StringLength(50)]
		public string IncomeSource { get; set; }

		public decimal IncomeAmount { get; set; }

		#endregion Company Details

		#region Address

		[Required]
		[StringLength(50)]
		public string AddressCity { get; set; }

		[Required]
		[StringLength(50)]
		public string AddressRegion { get; set; }


		[StringLength(300)]
		public string AddressCountry { get; set; }

		#endregion Address

		#region Health Declarations

		[Required]
		public bool HealthDeclaration1 { get; set; }
		[Required]
		public bool HealthDeclaration2 { get; set; }
		[Required]
		public bool HealthDeclaration3 { get; set; }
		[Required]
		public bool HealthDeclaration4 { get; set; }

		#endregion Health Declarations

		#region COVID Questions

		[Required]
		public bool CovidQuestion1 { get; set; }
		[Required]
		public bool CovidQuestion2 { get; set; }
		[Required]
		public bool CovidQuestion3 { get; set; }
		[Required]
		public bool CovidQuestion4 { get; set; }

		#endregion COVID Questions
	}
}
