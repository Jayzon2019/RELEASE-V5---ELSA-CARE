using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class Order : Entity<int>
	{
		public bool IsEligible { get; set; }

		// Insurance Information
		public string  PlanCode { get; set; }
		public string  PlanName { get; set; }
		public string  PlanPaymentMode { get; set; }
		public decimal PlanFaceAmount{ get; set; }
		public decimal PlanPremium { get; set; }

		// Referrer
		public string AgentFirstName { get; set; }
		public string AgentLastName { get; set; }
		public string ReferralSource { get; set; }

		// Health Questions
		public string Health1 { get; set; }
		public string Health2 { get; set; }
		public string Health3 { get; set; }

		// Fatca Questions
		public string Fatca1 { get; set; }
		public string Fatca2 { get; set; }

		// Misc Questions
		public string Question1 { get; set; }
		public string Question2 { get; set; }

		public string PolicyDeliveryOption { get; set; }

		public virtual Customer Owner { get; set; }

		public virtual Customer Insured { get; set; }
		public string InsuredRelationship { get; set; }

		public virtual Customer Beneficiary { get; set; }
		public string BeneficiaryRelationship { get; set; }
		public string BeneficiaryRight { get; set; }
		public string BeneficiaryPriority { get; set; }

		public virtual ICollection<OtherProduct> OtherProducts { get; set; }
	}
}
