using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics.Contracts;

using Newtonsoft.Json;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;
using InLife.Store.Core.Services;
using InLife.Store.Core.Utilities;

namespace InLife.Store.Core.Business
{
	public class GroupApplicationProcessing : IGroupApplicationProcessing
	{
		private readonly IGroupApplicationRepository applicationRepository;
		private readonly IReferenceCodeRepository referenceCodeRepository;
		private readonly IEmailService emailService;
		private readonly ISftpService sftpService;

		public GroupApplicationProcessing
		(
			IGroupApplicationRepository applicationRepository,
			IReferenceCodeRepository referenceCodeRepository,
			IEmailService emailService,
			ISftpService sftpService
		)
		{
			this.applicationRepository = applicationRepository;
			this.referenceCodeRepository = referenceCodeRepository;
			this.emailService = emailService;
			this.sftpService = sftpService;
		}

		public GroupApplication GetApplication(string refcode)
		{
			return applicationRepository.GetByReferenceCode(refcode);
		}

		public async Task<GroupApplication> RequestQuote(GroupQuoteForm form)
		{
			Contract.Requires(form != null);

			var id = Guid.NewGuid();
			var referenceCode = String.Empty;
			var isReferenceCodeExisting = true;

			// Check for reference code collisions
			while (isReferenceCodeExisting)
			{
				referenceCode = KeyGenerator.NewReferenceCode();
				isReferenceCodeExisting = referenceCodeRepository.Get(referenceCode) != null;
			}
			referenceCodeRepository.Create(new ReferenceCode
			{
				Id = referenceCode
			});

			var application = new GroupApplication
			{
				Id = id,
				ReferenceCode = referenceCode,

				PlanCode = form.PlanCode.Contains(" - ")
					? form.PlanCode.Split(" - ")[1]
					: form.PlanCode,
				PlanVariantCode = form.PlanVariantCode,
				PlanFaceAmount = form.PlanFaceAmount,
				PlanPremium = form.PlanPremium,

				TotalMembers = form.TotalMembers,
				TotalTeachers = form.TotalTeachers,
				TotalStudents = form.TotalStudents,

				RepresentativeNamePrefix = form.RepresentativeNamePrefix,
				RepresentativeNameSuffix = form.RepresentativeNameSuffix,
				RepresentativeFirstName = form.RepresentativeFirstName,
				RepresentativeMiddleName = form.RepresentativeMiddleName,
				RepresentativeLastName = form.RepresentativeLastName,
				RepresentativePhoneNumber = form.RepresentativePhoneNumber,
				RepresentativeMobileNumber = form.RepresentativeMobileNumber,
				RepresentativeEmailAddress = form.RepresentativeEmailAddress?.ToLower(),

				BusinessStructure = form.BusinessStructure,
				CompanyName = form.CompanyName,
				CompanyPhoneNumber = form.CompanyPhoneNumber,
				CompanyMobileNumber = form.CompanyMobileNumber,
				CompanyEmailAddress = form.CompanyEmailAddress?.ToLower(),

				CompanyAddress1 = form.CompanyAddress1,
				CompanyAddress2 = form.CompanyAddress2,
				CompanyTown = form.CompanyTown,
				CompanyCity = form.CompanyCity,
				CompanyRegion = form.CompanyRegion,
				CompanyZipCode = form.CompanyZipCode,
				CompanyCountry = form.CompanyCountry,

				Session = Guid.NewGuid().ToString(),
				SessionExpiration = DateTimeOffset.UtcNow.AddMinutes(5)
			};

			application.Status = GetStatus(application).Id;

			applicationRepository.Create(application);

			await emailService.SendGroupApplicationReferenceCode(application);

			return application;
		}

		public async Task<GroupApplication> UpdateQuote(string refcode, GroupQuoteForm form)
		{
			Contract.Requires(form != null);

			var application = applicationRepository.GetByReferenceCode(refcode);

			if (application == null)
				return null;

			application.PlanCode = form.PlanCode.Contains(" - ")
				? form.PlanCode.Split(" - ")[1]
				: form.PlanCode;
			application.PlanVariantCode = form.PlanVariantCode;
			application.PlanFaceAmount = form.PlanFaceAmount;
			application.PlanPremium = form.PlanPremium;

			application.TotalMembers = form.TotalMembers;
			application.TotalTeachers = form.TotalTeachers;
			application.TotalStudents = form.TotalStudents;

			application.RepresentativeNamePrefix = form.RepresentativeNamePrefix;
			application.RepresentativeNameSuffix = form.RepresentativeNameSuffix;
			application.RepresentativeFirstName = form.RepresentativeFirstName;
			application.RepresentativeMiddleName = form.RepresentativeMiddleName;
			application.RepresentativeLastName = form.RepresentativeLastName;
			application.RepresentativePhoneNumber = form.RepresentativePhoneNumber;
			application.RepresentativeMobileNumber = form.RepresentativeMobileNumber;
			application.RepresentativeEmailAddress = form.RepresentativeEmailAddress?.ToLower();

			application.BusinessStructure = form.BusinessStructure;
			application.CompanyName = form.CompanyName;
			application.CompanyPhoneNumber = form.CompanyPhoneNumber;
			application.CompanyMobileNumber = form.CompanyMobileNumber;
			application.CompanyEmailAddress = form.CompanyEmailAddress?.ToLower();

			application.CompanyAddress1 = form.CompanyAddress1;
			application.CompanyAddress2 = form.CompanyAddress2;
			application.CompanyTown = form.CompanyTown;
			application.CompanyCity = form.CompanyCity;
			application.CompanyRegion = form.CompanyRegion;
			application.CompanyZipCode = form.CompanyZipCode;
			application.CompanyCountry = form.CompanyCountry;

			application.Status = GetStatus(application).Id;

			applicationRepository.Update(application);

			await emailService.SendGroupApplicationReferenceCode(application);

			return application;
		}

		public async Task<GroupApplication> SaveApplication(string refcode, GroupApplicationForm form)
		{
			Contract.Requires(form != null);

			var application = applicationRepository.GetByReferenceCode(refcode);

			if (application == null)
				return null;

			application.RepresentativeNamePrefix = form.RepresentativeNamePrefix;
			application.RepresentativeNameSuffix = form.RepresentativeNameSuffix;
			application.RepresentativeFirstName = form.RepresentativeFirstName;
			application.RepresentativeMiddleName = form.RepresentativeMiddleName;
			application.RepresentativeLastName = form.RepresentativeLastName;
			application.RepresentativePhoneNumber = form.RepresentativePhoneNumber;
			application.RepresentativeMobileNumber = form.RepresentativeMobileNumber;
			application.RepresentativeEmailAddress = form.RepresentativeEmailAddress?.ToLower();

			application.BusinessStructure = form.BusinessStructure;
			application.CompanyName = form.CompanyName;
			application.CompanyPhoneNumber = form.CompanyPhoneNumber;
			application.CompanyMobileNumber = form.CompanyMobileNumber;
			application.CompanyEmailAddress = form.CompanyEmailAddress?.ToLower();

			application.CompanyAddress1 = form.CompanyAddress1;
			application.CompanyAddress2 = form.CompanyAddress2;
			application.CompanyTown = form.CompanyTown;
			application.CompanyCity = form.CompanyCity;
			application.CompanyRegion = form.CompanyRegion;
			application.CompanyZipCode = form.CompanyZipCode;
			application.CompanyCountry = form.CompanyCountry;

			application.Status = GetStatus(application).Id;

			applicationRepository.Update(application);

			await emailService.SendGroupApplicationReferenceCode(application);

			return application;
		}

		public async Task<GroupApplication> UploadFile(string refcode, string documentType, string contentType, string filename, Stream stream)
		{
			var application = applicationRepository.GetByReferenceCode(refcode);

			if (application == null)
				return null;

			documentType = documentType.ToLower();

			var companyName = application.CompanyName.Replace("  ", " ");
			foreach (char c in Path.GetInvalidFileNameChars())
				companyName = companyName.Replace(c, Char.MinValue);

			var directory = $"{application.ReferenceCode} - {companyName}";

			if (String.IsNullOrWhiteSpace(filename))
				filename = documentType + "." + MediaType.FromId(contentType).Extension;

			var uploadFilename = $"{documentType} - {filename}";

			await this.sftpService.UploadGroupFile(directory, uploadFilename, stream);

			//await sftpService.UploadGroupFile(application, documentType, contentType, stream);

			switch (documentType)
			{
				case "employee-census":
					application.EmployeeCensusFile = filename;
					break;
				case "admin-form":
					application.AdminFormFile = filename;
					break;
				case "representative":
					application.RepresentativeFile = filename;
					break;
				case "bir-document":
					application.BirDocumentFile = filename;
					break;
				case "business-registration-document":
					application.BusinessRegistrationDocumentFile = filename;
					break;
				case "incorporation-document":
					application.IncorporationDocumentFile = filename;
					break;
				case "authorization-document":
					application.AuthorizationDocumentFile = filename;
					break;
				case "individual-applications":
					application.IndividualApplicationsFile = filename;
					break;
				case "payment-proof":
					application.PaymentProofFile = filename;
					break;
				default:
					return null;
			}

			application.Status = GetStatus(application).Id;
			applicationRepository.Update(application);

			if (application.Status == GroupApplicationStatus.Complete.Id || application.Status == GroupApplicationStatus.PaymentProof.Id)
			{
				//await sftpService.UploadGroupApplicationData(application);
				await emailService.SendGroupApplicationPaymentProof(application, contentType, stream);
			}

			return application;
		}

		public async Task<GroupApplication> SetPaymentMode(string refcode, PaymentMode mode)
		{
			var application = applicationRepository.GetByReferenceCode(refcode);
			var benefits = GetApplicationBenefits(application);

			if (application == null)
				return null;

			application.PaymentMode = mode.Id;
			application.Status = GetStatus(application).Id;

			if (application.Status == GroupApplicationStatus.PaymentProof.Id)
				application.CompletedDate = DateTime.Now;

			applicationRepository.Update(application);

			if (application.Status == GroupApplicationStatus.Complete.Id || application.Status == GroupApplicationStatus.PaymentProof.Id)
				await sftpService.UploadGroupApplicationData(application);

			await emailService.SendGroupApplicationThankYou(application, benefits);

			return application;
		}

		public async Task<GroupApplication> Feedback(string refcode, GroupFeedbackForm form)
		{
			var application = applicationRepository.GetByReferenceCode(refcode);

			if (application == null)
				return null;

			application.Status = GetStatus(application).Id;
			application.CompletedDate = DateTimeOffset.Now;
			application.FeedbackRating = form.Rating;
			application.FeedbackMessage = form.Message;
			applicationRepository.Update(application);

			await emailService.SendGroupApplicationFeedback(application);

			return application;
		}

		public async Task<GroupApplication> Cancel(string refcode, GroupCancelForm form)
		{
			var application = applicationRepository.GetByReferenceCode(refcode);

			if (application == null)
				return null;

			if (application.Status == GroupApplicationStatus.Complete.Id
				|| application.Status == GroupApplicationStatus.Cancelled.Id)
				return null;

			application.Status = GroupApplicationStatus.Cancelled.Id;
			application.CancellationReason = form.Reason;
			application.CancellationComments = form.Comments;
			applicationRepository.Update(application);

			await emailService.SendGroupApplicationCancel(application);

			return application;
		}

		public async Task<bool> RequestOtp(string refcode)
		{
			var application = applicationRepository.GetByReferenceCode(refcode);
			if (application == null)
				return false;

			var length = 6;
			string[] characterMap = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

			string otp = "";
			string temp;
			var rand = new Random();

			for (int i = 0; i < length; i++)
			{
				rand.Next(0, characterMap.Length);
				temp = characterMap[rand.Next(0, characterMap.Length)];
				otp += temp;
			}

			application.Otp = otp;
			application.OtpExpiration = DateTimeOffset.UtcNow.AddMinutes(15);
			applicationRepository.Update(application);

			await emailService.SendGroupApplicationOtp(application);

			return true;
		}

		public GroupApplication RequestSession(string refcode, string otp)
		{
			Contract.Requires
			(
				!String.IsNullOrWhiteSpace(refcode) &&
				!String.IsNullOrWhiteSpace(otp)
			);

			var application = applicationRepository.GetByReferenceCode(refcode);

			if (application == null)
				return null;

			if (application.Otp == otp
				&& application.OtpExpiration.HasValue
				&& application.OtpExpiration.Value > DateTimeOffset.UtcNow)
			{
				application.Session = Guid.NewGuid().ToString();
				application.SessionExpiration = DateTimeOffset.UtcNow.AddMinutes(5);
			}
			else
			{
				application.Session = null;
				application.SessionExpiration = null;
			}

			applicationRepository.Update(application);

			return application;
		}

		public bool VerifySession(string refcode, string session)
		{
			Contract.Requires
			(
				!String.IsNullOrWhiteSpace(refcode) &&
				!String.IsNullOrWhiteSpace(session)
			);

			var application = applicationRepository.GetByReferenceCode(refcode);

			if (application == null)
				return false;

			if (application.Session == session
				&& application.SessionExpiration.HasValue
				&& application.SessionExpiration.Value > DateTimeOffset.UtcNow)
			{
				//application.SessionExpiration = DateTimeOffset.UtcNow.AddMinutes(5);
				application.SessionExpiration = DateTimeOffset.UtcNow.AddMinutes(60); // TODO: Put this in appsettings
				applicationRepository.Update(application);
				return true;
			}
			else
			{
				application.Session = null;
				application.SessionExpiration = null;
				applicationRepository.Update(application);
				return false;
			}
		}

		public async Task ProcessCompletedApplications()
		{
			// For testing purposes only
			// This will not mark the applications as exported
			//var applications = applicationRepository
			//	.GetAll()
			//	.Where(x => x.Status == GroupApplicationStatus.PaymentProof.Id && !x.ExportedDate.HasValue)
			//	.ToList();
			//await emailService.SendGroupApplicationsCompletedBatch(applications);

			try
			{
				// Check all completed (PaymentProof) but not submitted
				var applications = applicationRepository
					.GetAll()
					.Where(x => x.Status == GroupApplicationStatus.PaymentProof.Id && !x.ExportedDate.HasValue)
					.ToList();

				if (applications.Count == 0)
				{
					// Notify admin that the task is still running but no new data
					await emailService.SendGroupApplicationsCompletedBatch(applications);
				}
				else
				{
					// Upload to SFTP
					await sftpService.UploadGroupApplicationsBatchData(applications);

					// Send Notification
					await emailService.SendGroupApplicationsCompletedBatch(applications);

					// Set as exported
					var currentDate = DateTimeOffset.Now;
					applications.ForEach(x =>
					{
						x.ExportedDate = currentDate;
					});

					applicationRepository.Update(applications);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private GroupFile ParseDataUri(GroupFile file, string dataUri, string fileName = null)
		{
			var regex = new Regex(@"data:(?<mime>[\w/\-\.]+);(?<encoding>\w+),(?<data>.*)", RegexOptions.Compiled);
			var match = regex.Match(dataUri);

			//var mime = match.Groups["mime"].Value;
			//var encoding = match.Groups["encoding"].Value;
			//var data = match.Groups["data"].Value;

			file.MediaType = MediaType.FromId(match.Groups["mime"].Value);
			file.Data = match.Groups["data"].Value;
			file.FileName = fileName;

			return file;
		}

		// 1. Quote
		// 2. Application
		// 3. Payment
		// 4. PaymentProof
		// 5A Complete
		// 5B Cancelled
		// 6. Feedback (Optional)
		private GroupApplicationStatus GetStatus(GroupApplication application)
		{
			Contract.Requires(application != null);

			// Ignore if completed already
			if (application.Status == GroupApplicationStatus.Complete.Id)
				return GroupApplicationStatus.Complete;

			// Ignore if cancelled already
			if (application.Status == GroupApplicationStatus.Cancelled.Id)
				return GroupApplicationStatus.Cancelled;

			// Quote
			if (String.IsNullOrWhiteSpace(application.ReferenceCode) ||
				String.IsNullOrWhiteSpace(application.PlanCode) ||
				String.IsNullOrWhiteSpace(application.PlanVariantCode) ||
				application.PlanFaceAmount == default ||
				application.PlanPremium == default ||
				String.IsNullOrWhiteSpace(application.BusinessStructure) ||
				String.IsNullOrWhiteSpace(application.CompanyName) ||
				String.IsNullOrWhiteSpace(application.CompanyAddress1) ||
				String.IsNullOrWhiteSpace(application.CompanyAddress2) ||
				String.IsNullOrWhiteSpace(application.CompanyTown) ||
				String.IsNullOrWhiteSpace(application.CompanyCity) ||
				String.IsNullOrWhiteSpace(application.CompanyRegion) ||
				String.IsNullOrWhiteSpace(application.CompanyZipCode) ||
				String.IsNullOrWhiteSpace(application.RepresentativeFirstName) ||
				String.IsNullOrWhiteSpace(application.RepresentativeLastName) ||
				String.IsNullOrWhiteSpace(application.RepresentativeMobileNumber) ||
				String.IsNullOrWhiteSpace(application.RepresentativeEmailAddress))
			{
				return GroupApplicationStatus.Quote;
			}

			// Application
			if (String.IsNullOrWhiteSpace(application.EmployeeCensusFile) ||
				String.IsNullOrWhiteSpace(application.AdminFormFile) ||
				String.IsNullOrWhiteSpace(application.RepresentativeFile) ||
				String.IsNullOrWhiteSpace(application.BusinessRegistrationDocumentFile) ||
				String.IsNullOrWhiteSpace(application.AuthorizationDocumentFile))
			{
				return GroupApplicationStatus.Application;
			}

			// Payment
			if (String.IsNullOrWhiteSpace(application.PaymentMode))
				return GroupApplicationStatus.Payment;

			// PaymenProof
			if (String.IsNullOrWhiteSpace(application.PaymentProofFile))
				return GroupApplicationStatus.PaymentProof;

			return GroupApplicationStatus.Complete;
		}

		private string[] GetApplicationBenefits(GroupApplication application)
		{
			var code = application.PlanVariantCode.Replace(" ", "");

			// Administrative and Office-based

			if ("EmployeeSecurePlan1".Equals(code, StringComparison.OrdinalIgnoreCase))
				return new string[] { "Life Coverage: ₱100,000", "Accidental Death and Disability/Total and Permanent Disability Benefit: ₱100,000" };

			if ("EmployeeSecurePlan2".Equals(code, StringComparison.OrdinalIgnoreCase))
				return new string[] { "Life Coverage: ₱150,000", "Accidental Death and Disability/Total and Permanent Disability Benefit: ₱150,000" };

			if ("EmployeeSecurePlan3".Equals(code, StringComparison.OrdinalIgnoreCase))
				return new string[] { "Life Coverage: ₱200,000", "Accidental Death and Disability/Total and Permanent Disability Benefit: ₱150,000" };

			if ("EmployeeSecurePlan4".Equals(code, StringComparison.OrdinalIgnoreCase))
				return new string[] { "Life Coverage: ₱250,000", "Accidental Death and Disability/Total and Permanent Disability Benefit: ₱250,000" };

			// Security Agencies

			if ("SecurityGuardPlan1".Equals(code, StringComparison.OrdinalIgnoreCase))
				return new string[] { "Life Coverage: ₱25,000", "Accidental Death and Disability: ₱25,000", "Burial Benefit: ₱5,000" };

			if ("SecurityGuardPlan2".Equals(code, StringComparison.OrdinalIgnoreCase))
				return new string[] { "Life Coverage: ₱35,000", "Accidental Death and Disability: ₱35,000", "Burial Benefit: ₱5,000" };

			if ("SecurityGuardPlan3".Equals(code, StringComparison.OrdinalIgnoreCase))
				return new string[] { "Life Coverage: ₱55,000", "Accidental Death and Disability: ₱55,000", "Burial Benefit: ₱5,000" };

			if ("SecurityGuardPlan4".Equals(code, StringComparison.OrdinalIgnoreCase))
				return new string[] { "Life Coverage: ₱65,000", "Accidental Death and Disability: ₱65,000", "Burial Benefit: ₱5,000" };

			// Schools and Universities

			if ("StudentsAndTeachersPlan1".Equals(code, StringComparison.OrdinalIgnoreCase))
				return new string[]
				{
					"Life Coverage(non-accident): ₱10,000",
					"Life Coverage(accident): ₱25,000",
					"For Students (coverage per Head)",
					"Disability(both hands or feet, sight of both eyes, one hand & a foot, one hand & a sight of one eye, a foot & a sight of one eye): ₱10,000",
					"Disability(one hand or a foot, sight of one eye): ₱7,500",
					"Room and Board: ₱200",
					"Special Hospital Services: ₱2,400",
					"Doctor's Call: ₱2,400",
					"Maximum Hospitalization per Disability: ₱8,000",
					"Outpatient Benefits: ₱2,000"
				};

			if ("StudentsAndTeachersPlan2".Equals(code, StringComparison.OrdinalIgnoreCase))
				return new string[]
				{
					"Life Coverage(non-accident): ₱20,000",
					"Life Coverage(accident): ₱45,000",
					"For Students (coverage per Head)",
					"Disability(both hands or feet, sight of both eyes, one hand & a foot, one hand & a sight of one eye, a foot & a sight of one eye): ₱20,000",
					"Disability(one hand or a foot, sight of one eye): ₱12,500",
					"Room and Board: ₱400",
					"Special Hospital Services: ₱4,800",
					"Doctor's Call: ₱4,800",
					"Maximum Hospitalization per Disability: ₱16,000",
					"Outpatient Benefits: ₱4,000"
				};

			if ("StudentsAndTeachersPlan3".Equals(code, StringComparison.OrdinalIgnoreCase))
				return new string[]
				{
					"Life Coverage(non-accident): ₱25,000",
					"Life Coverage(accident): ₱55,000",
					"For Students (coverage per Head)",
					"Disability(both hands or feet, sight of both eyes, one hand & a foot, one hand & a sight of one eye, a foot & a sight of one eye): ₱25,000",
					"Disability(one hand or a foot, sight of one eye): ₱15,000",
					"Room and Board: ₱500",
					"Special Hospital Services: ₱6,000",
					"Doctor's Call: ₱6,000",
					"Maximum Hospitalization per Disability: ₱20,000",
					"Outpatient Benefits: ₱5,000"
				};

			if ("StudentsAndTeachersPlan4".Equals(code, StringComparison.OrdinalIgnoreCase))
				return new string[]
				{
					"Life Coverage(non-accident): ₱35,000",
					"Life Coverage(accident): ₱75,000",
					"For Students (coverage per Head)",
					"Disability(both hands or feet, sight of both eyes, one hand & a foot, one hand & a sight of one eye, a foot & a sight of one eye): ₱35,000",
					"Disability(one hand or a foot, sight of one eye): ₱20,000",
					"Room and Board: ₱700",
					"Special Hospital Services: ₱8,400",
					"Doctor's Call: ₱8,400",
					"Maximum Hospitalization per Disability: ₱28,000",
					"Outpatient Benefits: ₱7,000"
				};

			return null;
		}
	}
}
