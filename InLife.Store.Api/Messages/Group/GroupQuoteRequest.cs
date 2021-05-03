using System;
using System.ComponentModel.DataAnnotations;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class GroupQuoteRequest
	{
		public GroupQuoteForm Map(GroupQuoteForm model = null)
		{
			if (model == null)
				model = new GroupQuoteForm();

			model.PlanCode = PlanCode;
			model.PlanVariantCode = PlanVariantCode;
			model.PlanFaceAmount = PlanFaceAmount;
			model.PlanPremium = PlanPremium;

			model.TotalMembers = TotalMembers;
			model.TotalTeachers = TotalTeachers;
			model.TotalStudents = TotalStudents;

			model.RepresentativeNamePrefix = RepresentativeNamePrefix;
			model.RepresentativeNameSuffix = RepresentativeNameSuffix;
			model.RepresentativeFirstName = RepresentativeFirstName;
			model.RepresentativeMiddleName = RepresentativeMiddleName;
			model.RepresentativeLastName = RepresentativeLastName;
			model.RepresentativePhoneNumber = RepresentativePhoneNumber;
			model.RepresentativeMobileNumber = RepresentativeMobileNumber;
			model.RepresentativeEmailAddress = RepresentativeEmailAddress;

			model.BusinessStructure = BusinessStructure;
			model.CompanyName = CompanyName;
			model.CompanyPhoneNumber = CompanyPhoneNumber;
			model.CompanyMobileNumber = CompanyMobileNumber;
			model.CompanyEmailAddress = CompanyEmailAddress;

			model.CompanyAddress1 = CompanyAddress1;
			model.CompanyAddress2 = CompanyAddress2;
			model.CompanyTown = CompanyTown;
			model.CompanyCity = CompanyCity;
			model.CompanyRegion = CompanyRegion;
			model.CompanyZipCode = CompanyZipCode;
			model.CompanyCountry = CompanyCountry;

			return model;
		}


		#region Product Details

		[Required]
		[StringLength(50)]
		public string PlanCode { get; set; }

		[Required]
		[StringLength(50)]
		public string PlanVariantCode { get; set; }

		[Required]
		public decimal PlanFaceAmount { get; set; }

		[Required]
		public decimal PlanPremium { get; set; }

		public int? TotalMembers { get; set; }
		public int? TotalTeachers { get; set; }
		public int? TotalStudents { get; set; }

		#endregion Product Details

		#region Representative Details

		[StringLength(20)]
		public string RepresentativeNamePrefix { get; set; }

		[StringLength(20)]
		public string RepresentativeNameSuffix { get; set; }

		[Required]
		[StringLength(50)]
		public string RepresentativeFirstName { get; set; }

		[StringLength(50)]
		public string RepresentativeMiddleName { get; set; }

		[Required]
		[StringLength(50)]
		public string RepresentativeLastName { get; set; }

		[StringLength(20)]
		public string RepresentativePhoneNumber { get; set; }

		[Required]
		[StringLength(20)]
		[Phone]
		public string RepresentativeMobileNumber { get; set; }

		[Required]
		[StringLength(300)]
		[EmailAddress]
		public string RepresentativeEmailAddress { get; set; }

		#endregion Representative Details

		#region Company Details

		[Required]
		[StringLength(30)]
		public string BusinessStructure { get; set; }

		[Required]
		[StringLength(300)]
		public string CompanyName { get; set; }

		[StringLength(20)]
		public string CompanyPhoneNumber { get; set; }

		[StringLength(20)]
		public string CompanyMobileNumber { get; set; }

		[StringLength(300)]
		[EmailAddress]
		public string CompanyEmailAddress { get; set; }

		#endregion Company Details

		#region Company Address

		[Required]
		[StringLength(300)]
		public string CompanyAddress1 { get; set; }

		[Required]
		[StringLength(300)]
		public string CompanyAddress2 { get; set; }

		[Required]
		[StringLength(50)]
		public string CompanyTown { get; set; }

		[Required]
		[StringLength(50)]
		public string CompanyCity { get; set; }

		[Required]
		[StringLength(50)]
		public string CompanyRegion { get; set; }

		[Required]
		[StringLength(5)]
		public string CompanyZipCode { get; set; }

		[StringLength(300)]
		public string CompanyCountry { get; set; }

		#endregion Company Address
	}
}
