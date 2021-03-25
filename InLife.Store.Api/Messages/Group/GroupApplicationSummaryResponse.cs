using System;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class GroupApplicationSummaryResponse : BaseGroupResponse
	{
		public GroupApplicationSummaryResponse() : base ()
		{
		}

		public GroupApplicationSummaryResponse(GroupApplication model) : base (model)
		{
			PlanCode = model.PlanCode;
			PlanVariantCode = model.PlanVariantCode;
			PlanFaceAmount = model.PlanFaceAmount;
			PlanPremium = model.PlanPremium;

			TotalMembers = model.TotalMembers;
			TotalTeachers = model.TotalTeachers;
			TotalStudents = model.TotalStudents;

			RepresentativeNamePrefix = model.RepresentativeNamePrefix;
			RepresentativeNameSuffix = model.RepresentativeNameSuffix;
			RepresentativeFirstName = model.RepresentativeFirstName;
			RepresentativeMiddleName = model.RepresentativeMiddleName;
			RepresentativeLastName = model.RepresentativeLastName;
			RepresentativePhoneNumber = model.RepresentativePhoneNumber;
			RepresentativeMobileNumber = model.RepresentativeMobileNumber;
			RepresentativeEmailAddress = model.RepresentativeEmailAddress;

			BusinessStructure = model.BusinessStructure;
			CompanyName = model.CompanyName;
			CompanyPhoneNumber = model.CompanyPhoneNumber;
			CompanyMobileNumber = model.CompanyMobileNumber;
			CompanyEmailAddress = model.CompanyEmailAddress;

			CompanyAddress1 = model.CompanyAddress1;
			CompanyAddress2 = model.CompanyAddress2;
			CompanyTown = model.CompanyTown;
			CompanyCity = model.CompanyCity;
			CompanyRegion = model.CompanyRegion;
			CompanyZipCode = model.CompanyZipCode;
			CompanyCountry = model.CompanyCountry;

			EmployeeCesusForm = model.EmployeeCensusFile;
			EntityPlanForm = model.AdminFormFile;
			AuthRepresentativeId = model.RepresentativeFile;
			BIRNoticeForm = model.BirDocumentFile;
			SECRegistration = model.BusinessRegistrationDocumentFile;
			IncorporationArticles = model.IncorporationDocumentFile;
			IdentityCertificate = model.AuthorizationDocumentFile;
			PostPolicyForm = model.IndividualApplicationsFile;

	}


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

		public string EmployeeCesusForm { get; set; }
		public string EntityPlanForm { get; set; }
		public string AuthRepresentativeId { get; set; }
		public string BIRNoticeForm { get; set; }
		public string SECRegistration { get; set; }
		public string IncorporationArticles { get; set; }
		public string IdentityCertificate { get; set; }
		public string PostPolicyForm { get; set; }

		#endregion Company Address
	}
}
