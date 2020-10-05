using System;
using InLife.Store.Core.Models.Enumerations;

namespace InLife.Store.Core.Models
{
	public class Quote : Entity<int>
	{
		public virtual Customer Customer { get; set; }

		// Product
		// TODO: Move this in a separate table
		public string ProductCode { get; set; }
		public string ProductName { get; set; }

		public decimal ProductFaceAmount { get; set; }
		public int PaymentMode { get; set; }

		// Referrer
		public string AgentFirstName { get; set; }
		public string AgentLastName { get; set; }
		public string ReferralSource { get; set; }

		// Health Condition
		public bool Health1 { get; set; }
		public bool Health2 { get; set; }
		public bool Health3 { get; set; }

		public bool IsEligible { get; set; }
	}
}
