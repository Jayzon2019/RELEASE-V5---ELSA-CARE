using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class PrimeCareApplication : Application
	{
		public PrimeCareApplication()
		{
			ProductCode = "AH0017";
			ProductName = "Prime Care";
		}

		public int ReferenceId { get; set; }

		// Customer
		public Guid? CustomerId { get; set; }
		public virtual PrimeCarePerson Customer { get; set; }

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

		// Health Questions
		public bool Health1 { get; set; }
		public bool Health2 { get; set; }
		public bool Health3 { get; set; }

		// Fatca Questions
		public bool Fatca1 { get; set; }
		public string Fatca2 { get; set; }

		// Misc Questions
		public bool Question1 { get; set; }
		public bool Question2 { get; set; }


		//public virtual ICollection<PrimeCarePerson> Beneficiaries { get; set; }

		//public virtual ICollection<PrimeCareOtherInsurance> OtherInsurances { get; set; }
	}
}
