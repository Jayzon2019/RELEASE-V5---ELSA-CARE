using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class PrimeSecureApplication : Application
	{
		public PrimeSecureApplication()
		{
			ProductCode = "TR0091";
			ProductName = "Prime Secure Lite";
		}

		public int ReferenceId { get; set; }

		// Order
		public string OrderNumber { get; set; }
		public string OrderItemNumber { get; set; }
		public string OrderStatus { get; set; }

		// Customer
		public Guid? CustomerId { get; set; }
		public virtual PrimeSecurePerson Customer { get; set; }

		// Quote
		public bool IsEligible { get; set; }

		// Referrer
		public string AgentCode { get; set; }
		public string AgentFirstName { get; set; }
		public string AgentLastName { get; set; }
		public string ReferralSource { get; set; }

		public string AffiliateCode { get; set; }
		public string AffiliateName { get; set; }
		public string AffiliateStatus { get; set; }

		public string BranchCode { get; set; }
		public string BranchName { get; set; }

		// Health Condition
		public bool? HealthDeclaration1 { get; set; }
		public bool? HealthDeclaration2 { get; set; }
		public bool? HealthDeclaration3 { get; set; }
		public bool? HealthDeclaration4 { get; set; }

		public bool? CovidQuestion1 { get; set; }
		public bool? CovidQuestion2 { get; set; }
		public bool? CovidQuestion3 { get; set; }
		public bool? CovidQuestion4 { get; set; }


		public string Company { get; set; }
		public string Occupation { get; set; }
		public string IncomeSource { get; set; }
		public decimal? IncomeAmount { get; set; }


		public int? Height { get; set; }
		public int? Weight { get; set; }

		//public virtual PrimeSecurePerson Insured { get; set; }
		//public string InsuredRelationship { get; set; }

		//public virtual PrimeSecurePerson Beneficiary { get; set; }
		//public string BeneficiaryRelationship { get; set; }
	}
}
