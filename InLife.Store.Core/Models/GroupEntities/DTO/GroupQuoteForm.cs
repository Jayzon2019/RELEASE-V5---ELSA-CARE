using System;
using InLife.Store.Core.Models.Enumerations;

namespace InLife.Store.Core.Models
{
	public class GroupQuoteForm
	{

		#region Product Details

		public string PlanCode { get; set; }
		public string PlanVariantCode { get; set; }
		public decimal PlanFaceAmount { get; set; }
		public decimal PlanPremium { get; set; }

		public int? TotalMembers { get; set; }
		public int? TotalTeachers { get; set; }
		public int? TotalStudents { get; set; }

		#endregion Product Details

		#region Representative Details

		public string RepresentativeNamePrefix { get; set; }
		public string RepresentativeNameSuffix { get; set; }
		public string RepresentativeFirstName { get; set; }
		public string RepresentativeMiddleName { get; set; }
		public string RepresentativeLastName { get; set; }
		public string RepresentativePhoneNumber { get; set; }
		public string RepresentativeMobileNumber { get; set; }
		public string RepresentativeEmailAddress { get; set; }

		#endregion Representative Details

		#region Company Details

		public string BusinessStructure { get; set; }
		public string CompanyName { get; set; }
		public string CompanyPhoneNumber { get; set; }
		public string CompanyMobileNumber { get; set; }
		public string CompanyEmailAddress { get; set; }

		#endregion Company Details

		#region Company Address

		public string CompanyAddress1 { get; set; }
		public string CompanyAddress2 { get; set; }
		public string CompanyTown { get; set; }
		public string CompanyCity { get; set; }
		public string CompanyRegion { get; set; }
		public string CompanyZipCode { get; set; }
		public string CompanyCountry { get; set; }

		#endregion Company Address
	}
}
