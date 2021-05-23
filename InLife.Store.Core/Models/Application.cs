using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class Application : Entity<Guid>
	{
		public string ReferenceCode { get; set; }
		public string PolicyNumber { get; set; }
		public string Status { get; set; }

		// Insurance Information
		public string ProductCode { get; set; }
		public string ProductName { get; set; }
		public string PlanCode { get; set; }
		public string PlanName { get; set; }
		public string PlanVariantCode { get; set; }
		public decimal PlanFaceAmount { get; set; }
		public decimal PlanPremium { get; set; }
		public string PaymentMode { get; set; }
		public string PaymentFrequency { get; set; }

		// Timestamp
		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset? CompletedDate { get; set; }
	}
}
