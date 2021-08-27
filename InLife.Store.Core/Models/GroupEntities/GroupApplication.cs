using System;
using System.Text;

using InLife.Store.Core.Utilities;
using InLife.Store.Core.Models.Enumerations;

namespace InLife.Store.Core.Models
{
	public class GroupApplication : Application
	{
		public GroupApplication()
		{
			ProductCode = "Group";
			ProductName = "Group";
		}

		// Temporary OTP Solution
		public string Otp { get; set; }
		public DateTimeOffset? OtpExpiration { get; set; }
		public string Session { get; set; }
		public DateTimeOffset? SessionExpiration { get; set; }

		public int? TotalMembers { get; set; }
		public int? TotalTeachers { get; set; }
		public int? TotalStudents { get; set; }

		public DateTimeOffset? ExportedDate { get; set; }
		public DateTime? ExportedDateLocal => ExportedDate?.ToLocalDateTime();

		public string RepresentativeNamePrefix { get; set; }
		public string RepresentativeNameSuffix { get; set; }
		public string RepresentativeFirstName { get; set; }
		public string RepresentativeMiddleName { get; set; }
		public string RepresentativeLastName { get; set; }
		public string RepresentativePhoneNumber { get; set; }
		public string RepresentativeMobileNumber { get; set; }
		public string RepresentativeEmailAddress { get; set; }
		public virtual string RepresentativeFullName
		{
			get
			{
				StringBuilder name = new StringBuilder();

				if (!String.IsNullOrWhiteSpace(RepresentativeNamePrefix))
					name.Append(RepresentativeNamePrefix).Append(". ");

				name
					.Append(RepresentativeFirstName).Append(' ')
					.Append(RepresentativeMiddleName).Append(' ')
					.Append(RepresentativeLastName).Append(' ');

				if (!String.IsNullOrWhiteSpace(RepresentativeNameSuffix))
					name.Append(RepresentativeNameSuffix);

				name.Replace("  ", " ");

				return name.ToString().Trim();
			}
		}

		public string BusinessStructure { get; set; }
		public string CompanyName { get; set; }
		public string CompanyPhoneNumber { get; set; }
		public string CompanyMobileNumber { get; set; }
		public string CompanyEmailAddress { get; set; }

		public string CompanyAddress1 { get; set; }
		public string CompanyAddress2 { get; set; }
		public string CompanyTown { get; set; }
		public string CompanyCity { get; set; }
		public string CompanyRegion { get; set; }
		public string CompanyZipCode { get; set; }
		public string CompanyCountry { get; set; }

		public virtual string CompanyAddress
		{
			get
			{
				StringBuilder address = new StringBuilder();

				address
					.Append(CompanyAddress1).Append(", ")
					.Append(CompanyAddress2).Append(", ")
					.Append(CompanyTown).Append(", ")
					.Append(CompanyCity).Append(", ")
					.Append(CompanyRegion).Append(", ")
					.Append(CompanyZipCode).Append(", ")
					.Append(CompanyCountry).Append(", ")
					.Replace(", ,", ",")
					.Replace(",,", ",")
					.Remove(address.Length - 2, 1);

				return address.ToString().Trim();
			}
		}

		public int? FeedbackRating { get; set; }
		public string FeedbackMessage { get; set; }

		public string CancellationReason { get; set; }
		public string CancellationComments { get; set; }

		// Files
		//public Guid? PaymentProofFileId { get; set; }
		//public virtual GroupFile PaymentProofFile { get; set; }

		//public Guid? EmployeeCensusFileId { get; set; }
		//public virtual GroupFile EmployeeCensusFile { get; set; }

		//public Guid? AdminFormFileId { get; set; }
		//public virtual GroupFile AdminFormFile { get; set; }

		//public Guid? RepresentativeFileId { get; set; }
		//public virtual GroupFile RepresentativeFile { get; set; }

		//public Guid? BirDocumentFileId { get; set; }
		//public virtual GroupFile BirDocumentFile { get; set; }

		//public Guid? BusinessRegistrationDocumentFileId { get; set; }
		//public virtual GroupFile BusinessRegistrationDocumentFile { get; set; }

		//public Guid? IncorporationDocumentFileId { get; set; }
		//public virtual GroupFile IncorporationDocumentFile { get; set; }

		//public Guid? AuthorizationDocumentFileId { get; set; }
		//public virtual GroupFile AuthorizationDocumentFile { get; set; }

		//public Guid? IndividualApplicationsFileId { get; set; }
		//public virtual GroupFile IndividualApplicationsFile { get; set; }

		public string PaymentProofFile { get; set; }
		public string EmployeeCensusFile { get; set; }
		public string AdminFormFile { get; set; }
		public string RepresentativeFile { get; set; }
		public string BirDocumentFile { get; set; }
		public string BusinessRegistrationDocumentFile { get; set; }
		public string IncorporationDocumentFile { get; set; }
		public string AuthorizationDocumentFile { get; set; }
		public string IndividualApplicationsFile { get; set; }

		public bool AlreadyDeclared { get; set; }
	}
}
