using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class PrimeSecureApplication : Application
	{
		public PrimeSecureApplication()
		{
			ProductCode = "PrimeSecure";
			ProductName = "PrimeSecure";
		}

		// Referrer
		public string AgentCode { get; set; }
		public string AgentFirstName { get; set; }
		public string AgentLastName { get; set; }
		public string ReferralSource { get; set; }

		// Health Condition
		public bool? Health1 { get; set; }
		public bool? Health2 { get; set; }
		public bool? Health3a { get; set; }
		public bool? Health3b { get; set; }
		public bool? Health4 { get; set; }

		public string Company { get; set; }
		public string Occupation { get; set; }
		public string IncomeSource { get; set; }
		public decimal? IncomeAmount { get; set; }


		public int? Height { get; set; }
		public int? Weight { get; set; }

		public bool IsEligible { get; set; }

		public virtual PrimeSecurePerson Customer { get; set; }

		public virtual PrimeSecurePerson Insured { get; set; }
		public string InsuredRelationship { get; set; }

		public virtual PrimeSecurePerson Beneficiary { get; set; }
		public string BeneficiaryRelationship { get; set; }
	}
}
