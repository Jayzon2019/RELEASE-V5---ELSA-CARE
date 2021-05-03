using System;
using InLife.Store.Core.Models.Enumerations;

namespace InLife.Store.Core.Models
{
	public class PrimeSecureQuoteForm
	{

		#region Product Details

		public string PlanCode { get; set; }
		public string PlanVariantCode { get; set; }
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
		public int Height { get; set; }
		public int Weight { get; set; }

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

		#endregion Referrer

		#region Health Condition

		// Health Condition
		public bool? Health1 { get; set; }
		public bool? Health2 { get; set; }
		public bool? Health3a { get; set; }
		public bool? Health3b { get; set; }
		public bool? Health4 { get; set; }

		#endregion Health Condition
	}
}
