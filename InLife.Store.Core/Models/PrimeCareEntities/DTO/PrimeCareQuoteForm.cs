using System;
using System.Collections.Generic;
using System.Text;

namespace InLife.Store.Core.Models
{
	public class PrimeCareQuoteForm
	{
		#region Product Details

		public string PlanCode { get; set; }
		public string PlanName { get; set; }
		public decimal PlanFaceAmount { get; set; }
		public decimal PlanPremium { get; set; }
		public string PaymentFrequency { get; set; }

		#endregion Product Details

		#region Customer Details

		public string NamePrefix { get; set; }
		public string NameSuffix { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Gender { get; set; }
		public DateTime? BirthDate { get; set; }

		#endregion Customer Details

		#region Contact Details

		public string EmailAddress { get; set; }
		public string MobileNumber { get; set; }
		public string PhoneNumber { get; set; }

		#endregion Product Details

		#region Location Details

		public string Country { get; set; }
		public string Region { get; set; }
		public string City { get; set; }

		#endregion Product Details

		#region Referral Details

		public string ReferralSource { get; set; }
		public string AgentCode { get; set; }
		public string AgentFirstName { get; set; }
		public string AgentLastName { get; set; }

		#endregion Product Details

		#region Health Questions

		public bool Health1 { get; set; }
		public bool Health2 { get; set; }
		public bool Health3 { get; set; }
		public bool IsEligible { get; set; }

		#endregion Product Details
	}
}
