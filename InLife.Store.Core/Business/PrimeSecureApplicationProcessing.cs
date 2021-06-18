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
	public class PrimeSecureApplicationProcessing : IPrimeSecureApplicationProcessing
	{
		private readonly IPrimeSecureApplicationRepository applicationRepository;
		private readonly IReferenceCodeRepository referenceCodeRepository;
		private readonly IEmailService emailService;

		public PrimeSecureApplicationProcessing
		(
			IPrimeSecureApplicationRepository applicationRepository,
			IReferenceCodeRepository referenceCodeRepository,
			IEmailService emailService
		)
		{
			this.applicationRepository = applicationRepository;
			this.referenceCodeRepository = referenceCodeRepository;
			this.emailService = emailService;
		}

		public PrimeSecureApplication GetApplication(string refcode)
		{
			return applicationRepository.GetByReferenceCode(refcode);
		}

		public async Task<PrimeSecureApplication> RequestQuote(PrimeSecureQuoteForm form)
		{
			Contract.Requires(form != null);

			var id = Guid.NewGuid();

			var application = new PrimeSecureApplication
			{
				Id = id,
				Status = PrimeSecureApplicationStatus.Quote.Name,

				PlanCode = form.PlanCode,
				PlanName = form.PlanName,
				PlanVariantCode = form.PlanCode,
				PlanFaceAmount = form.PlanFaceAmount,
				PlanPremium = form.PlanPremium,

				Customer = new PrimeSecurePerson
				{
					NamePrefix = form.CustomerNamePrefix,
					NameSuffix = form.CustomerNameSuffix,
					FirstName = form.CustomerFirstName,
					MiddleName = form.CustomerMiddleName,
					LastName = form.CustomerLastName,
					MobileNumber = form.CustomerMobileNumber,
					EmailAddress = form.CustomerEmailAddress,

					HomeAddress = new PrimeSecureAddress
					{
						PhoneNumber = form.CustomerPhoneNumber,
						City = form.AddressCity,
						Region = form.AddressRegion,
						Country = form.AddressCountry
					}
				},

				Height = form.Height,
				Weight = form.Weight,

				Company = form.Company,
				Occupation = form.Occupation,
				IncomeSource = form.IncomeSource,
				IncomeAmount = form.IncomeAmount,

				AgentCode = form.AgentCode,
				AgentFirstName = form.AgentFirstName,
				AgentLastName = form.AgentLastName,
				ReferralSource = form.ReferralSource,

				HealthDeclaration1 = form.HealthDeclaration1,
				HealthDeclaration2 = form.HealthDeclaration2,
				HealthDeclaration3 = form.HealthDeclaration3,
				HealthDeclaration4 = form.HealthDeclaration4,

				CovidQuestion1 = form.CovidQuestion1,
				CovidQuestion2 = form.CovidQuestion2,
				CovidQuestion3 = form.CovidQuestion3,
				CovidQuestion4 = form.CovidQuestion4,
				IsEligible = form.IsEligible
			};

			//application.Status = GetStatus(application).Id;

			applicationRepository.Create(application);

			application.ReferenceCode = application.ReferenceId.ToString().PadLeft(10, '0');
			applicationRepository.Update(application);

			//await emailService.SendPrimeSecureApplicationReferenceCode(application);

			await Task.Delay(0);
			return application;
		}


		//private PrimeSecureFile ParseDataUri(PrimeSecureFile file, string dataUri, string fileName = null)
		//{
		//	var regex = new Regex(@"data:(?<mime>[\w/\-\.]+);(?<encoding>\w+),(?<data>.*)", RegexOptions.Compiled);
		//	var match = regex.Match(dataUri);

		//	//var mime = match.PrimeSecures["mime"].Value;
		//	//var encoding = match.PrimeSecures["encoding"].Value;
		//	//var data = match.PrimeSecures["data"].Value;

		//	file.MediaType = MediaType.FromId(match.PrimeSecures["mime"].Value);
		//	file.Data = match.PrimeSecures["data"].Value;
		//	file.FileName = fileName;

		//	return file;
		//}

		// 1. Quote
		// 2. Application
		// 3. Payment
		// 4. PaymentProof
		// 5A Complete
		// 5B Cancelled
		// 6. Feedback (Optional)
		//private PrimeSecureApplicationStatus GetStatus(PrimeSecureApplication application)
		//{
		//	Contract.Requires(application != null);

		//	// Ignore if completed already
		//	if (application.Status == PrimeSecureApplicationStatus.Complete.Id)
		//		return PrimeSecureApplicationStatus.Complete;

		//	// Ignore if cancelled already
		//	if (application.Status == PrimeSecureApplicationStatus.Cancelled.Id)
		//		return PrimeSecureApplicationStatus.Cancelled;

		//	// Quote
		//	if (String.IsNullOrWhiteSpace(application.ReferenceCode) ||
		//		String.IsNullOrWhiteSpace(application.PlanCode) ||
		//		String.IsNullOrWhiteSpace(application.PlanVariantCode) ||
		//		application.PlanFaceAmount == default ||
		//		application.PlanPremium == default ||
		//		String.IsNullOrWhiteSpace(application.BusinessStructure) ||
		//		String.IsNullOrWhiteSpace(application.CompanyName) ||
		//		String.IsNullOrWhiteSpace(application.CompanyAddress1) ||
		//		String.IsNullOrWhiteSpace(application.CompanyAddress2) ||
		//		String.IsNullOrWhiteSpace(application.CompanyTown) ||
		//		String.IsNullOrWhiteSpace(application.CompanyCity) ||
		//		String.IsNullOrWhiteSpace(application.CompanyRegion) ||
		//		String.IsNullOrWhiteSpace(application.CompanyZipCode) ||
		//		String.IsNullOrWhiteSpace(application.RepresentativeFirstName) ||
		//		String.IsNullOrWhiteSpace(application.RepresentativeLastName) ||
		//		String.IsNullOrWhiteSpace(application.RepresentativeMobileNumber) ||
		//		String.IsNullOrWhiteSpace(application.RepresentativeEmailAddress))
		//	{
		//		return PrimeSecureApplicationStatus.Quote;
		//	}

		//	// Application
		//	if (String.IsNullOrWhiteSpace(application.EmployeeCensusFile) ||
		//		String.IsNullOrWhiteSpace(application.AdminFormFile) ||
		//		String.IsNullOrWhiteSpace(application.RepresentativeFile) ||
		//		String.IsNullOrWhiteSpace(application.BusinessRegistrationDocumentFile) ||
		//		String.IsNullOrWhiteSpace(application.AuthorizationDocumentFile))
		//	{
		//		return PrimeSecureApplicationStatus.Application;
		//	}

		//	// Payment
		//	if (String.IsNullOrWhiteSpace(application.PaymentMode))
		//		return PrimeSecureApplicationStatus.Payment;

		//	// PaymenProof
		//	if (String.IsNullOrWhiteSpace(application.PaymentProofFile))
		//		return PrimeSecureApplicationStatus.PaymentProof;

		//	return PrimeSecureApplicationStatus.Complete;
		//}

	}
}
