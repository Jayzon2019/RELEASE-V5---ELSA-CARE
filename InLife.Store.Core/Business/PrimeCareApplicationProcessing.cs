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
	public class PrimeCareApplicationProcessing : IPrimeCareApplicationProcessing
	{
		private readonly IPrimeCareApplicationRepository applicationRepository;
		private readonly IReferenceCodeRepository referenceCodeRepository;
		private readonly IEmailService emailService;
		private readonly ISftpService sftpService;

		public PrimeCareApplicationProcessing
		(
			IPrimeCareApplicationRepository applicationRepository,
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

		public PrimeCareApplication GetApplication(Guid id)
		{
			return applicationRepository.Get(id);
		}

		public PrimeCareApplication GetApplication(string refcode)
		{
			return applicationRepository.GetByReferenceCode(refcode);
		}

		public async Task<PrimeCareApplication> RequestQuote(PrimeCareQuoteForm form)
		{
			Contract.Requires(form != null);

			var id = Guid.NewGuid();

			var application = new PrimeCareApplication
			{
				Id = id,
				Status = PrimeCareApplicationStatus.Quote.Name,

				PlanCode = form.PlanCode,
				PlanName = form.PlanName,

				PlanFaceAmount = form.PlanFaceAmount,
				PlanPremium = form.PlanPremium,

				Customer = new PrimeCarePerson
				{
					NamePrefix = form.NamePrefix,
					NameSuffix = form.NameSuffix,
					FirstName = form.FirstName,
					MiddleName = form.MiddleName,
					LastName = form.LastName,
					Gender = form.Gender,
					BirthDate = form.BirthDate,
					EmailAddress = form.EmailAddress,
					MobileNumber = form.MobileNumber,

					HomeAddress = new PrimeCareAddress
					{
						City = form.City,
						Region = form.Region,
						Country = form.Country,
						PhoneNumber = form.PhoneNumber
					}
				},

				ReferralSource = form.ReferralSource,
				AgentCode = form.AgentCode,
				AgentFirstName = form.AgentFirstName,
				AgentLastName = form.AgentLastName,

				AffiliateCode = form.AffiliateCode,
				AffiliateName = form.AffiliateName,
				AffiliateStatus = form.AffiliateStatus,

				Health1 = form.Health1,
				Health2 = form.Health2,
				Health3 = form.Health3,

				IsEligible = form.IsEligible
			};

			applicationRepository.Create(application);

			application.ReferenceCode = application.ReferenceId.ToString().PadLeft(10, '0');
			applicationRepository.Update(application);

			//await emailService.SendPrimeCareQuoteApplication(application);

			await Task.Delay(0);
			return application;
		}

		//public Task<PrimeCareApplication> UpdateQuote(string refcode, PrimeCareQuoteForm form)
		//{
		//}
	}
}
