using System;
using InLife.Store.Core.Models.Enumerations;

namespace InLife.Store.Core.Models
{
	public class PrimeSecureQuoteForm
	{

		#region Product Details

		public string PlanCode { get; set; }
		public string PlanName { get; set; }
		public decimal PlanFaceAmount { get; set; }
		public decimal PlanPremium { get; set; }

		#endregion Product Details

		#region Customer Details

		public string CustomerNamePrefix { get; set; }
		public string CustomerNameSuffix { get; set; }
		public string CustomerFirstName { get; set; }
		public string CustomerMiddleName { get; set; }
		public string CustomerLastName { get; set; }
		public string CustomerPhoneNumber { get; set; }
		public string CustomerMobileNumber { get; set; }
		public string CustomerEmailAddress { get; set; }
		public DateTime CustomerBirthdate { get; set; }
		public int Height { get; set; }
		public int Weight { get; set; }
		public bool IsEligible { get; set; }

		#endregion Customer Details

		#region Address

		public string AddressCity { get; set; }
		public string AddressRegion { get; set; }
		public string AddressCountry { get; set; }

		#endregion Address

		#region Income

		public string Company { get; set; }
		public string Occupation { get; set; }
		public string IncomeSource { get; set; }
		public decimal? IncomeAmount { get; set; }

		#endregion Income

		#region Referrer

		public string AgentCode { get; set; }
		public string AgentFirstName { get; set; }
		public string AgentLastName { get; set; }
		public string ReferralSource { get; set; }

		public string AffiliateCode { get; set; }
		public string AffiliateName { get; set; }
		public string AffiliateStatus { get; set; }

		public string BranchCode { get; set; }
		public string BranchName { get; set; }

		#endregion Referrer

		#region Health Declarations

		public bool? HealthDeclaration1 { get; set; }
		public bool? HealthDeclaration2 { get; set; }
		public bool? HealthDeclaration3 { get; set; }
		public bool? HealthDeclaration4 { get; set; }

		#endregion Health Declarations

		#region COVID Questions

		public bool? CovidQuestion1 { get; set; }
		public bool? CovidQuestion2 { get; set; }
		public bool? CovidQuestion3 { get; set; }
		public bool? CovidQuestion4 { get; set; }

		#endregion COVID Questions
	}
}
