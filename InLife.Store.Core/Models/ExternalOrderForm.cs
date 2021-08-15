using System;
using System.Collections.Generic;
using System.Text;

namespace InLife.Store.Core.Models
{
	public class ExternalOrderForm
	{
		public string ReferenceCode { get; set; }

		public string OrderNumber { get; set; }
		public string OrderItemNumber { get; set; }
		public string OrderStatus { get; set; }

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

		#endregion Product Details

		#region Birth Location Details

		public string BirthCountry { get; set; }
		public string BirthRegion { get; set; }
		public string BirthCity { get; set; }
		public string BirthZipCode { get; set; }

		#endregion Birth Location Details

		#region Home Location Details

		public string HomeCountry { get; set; }
		public string HomeRegion { get; set; }
		public string HomeCity { get; set; }
		public string HomeZipCode { get; set; }
		public string HomeAddress1 { get; set; }
		public string HomeAddress2 { get; set; }
		public string HomePhoneNumber { get; set; }

		#endregion Home Location Details
	}
}
