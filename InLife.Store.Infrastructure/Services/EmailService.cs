using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using InLife.Store.Core.Models;
using InLife.Store.Core.Settings;
using InLife.Store.Core.Services;
using InLife.Store.Resources;

namespace InLife.Store.Infrastructure.Services
{
	public class EmailService : IEmailService
	{
		private readonly IHostEnvironment hostingEnvironment;
		private readonly SmtpSettings smtpSettings;
		private readonly EmailSettings emailSettings;

		public EmailService
		(
			IHostEnvironment hostingEnvironment,
			IOptions<SmtpSettings> smtpSettings,
			IOptions<EmailSettings> emailSettings
		)
		{
			this.hostingEnvironment = hostingEnvironment;
			this.emailSettings = emailSettings.Value;
			this.smtpSettings = smtpSettings.Value;
		}

		#region General

		public async Task SendAsync(MailAddress sender, MailAddressCollection recipients, string subject, string body, Collection<Attachment> attachments = null)
		{
			MailMessage mail = new MailMessage()
			{
				From = sender,
				Subject = subject,
				Body = body,
				IsBodyHtml = true
			};

			if (attachments != null)
				foreach(var attachment in attachments)
					mail.Attachments.Add(attachment);

			foreach (var recipient in recipients)
				mail.To.Add(recipient);

			using var client = new SmtpClient()
			{
				Host = this.smtpSettings.Host,
				Port = this.smtpSettings.Port,
				Credentials = new NetworkCredential(this.smtpSettings.Username, this.smtpSettings.Password),
				EnableSsl = this.smtpSettings.EnableSsl
			};
			await client.SendMailAsync(mail);
		}

		public async Task SendErrorNotificationAsync(ErrorLog model)
		{
			var emailSettings = this.emailSettings.ErrorNotification;

			var body = new StringBuilder(EmailTemplates.ErrorNotification)
				.Replace("#ID#", model.Id.ToString())
				.Replace("#TITLE#", model.Title)
				.Replace("#DETAIL#", model.Detail)
				.Replace("#SOURCE#", model.Source)
				.ToString();

			var subject = emailSettings.Subject;

			var sender = new MailAddress
			(
				emailSettings.SenderEmail,
				emailSettings.SenderName
			);

			var recipients = new MailAddressCollection();
			foreach(var recipient in emailSettings.Recipients)
			{
				recipients.Add(new MailAddress(recipient));
			}

			await this.SendAsync
			(
				sender,
				recipients,
				subject,
				body
			);
		}

		#endregion

		#region Identity

		public async Task SendPasswordAsync(MailAddress recipient, string password)
		{
			await Task.Delay(0);
			throw new NotImplementedException();

			//var emailSettings = this.emailSettings.SendPassword;

			//var body = new StringBuilder(EmailTemplates.SendPassword)
			//	.Replace("#RECIPIENT-NAME#", recipient.DisplayName)
			//	.Replace("#PASSWORD#", password)
			//	.ToString();

			//var sender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
			//var recipients = new MailAddressCollection();
			//var subject = emailSettings.Subject;

			//recipients.Add(new MailAddress(recipient.Address, recipient.DisplayName));

			//await this.SendAsync
			//(
			//	sender,
			//	recipients,
			//	subject,
			//	body
			//);
		}

		#endregion

		#region Business - General

		//public async Task SendQuoteRequestAsync(Quote model)
		//{
		//	// Send email to admin

		//	var emailSettings = this.emailSettings.QuoteRequestAdmin;
		//	var body = this.LoadEmailTemplate(emailSettings.TemplateFile);

		//	//body = body
		//	//	.Replace("#REQUEST-ID#", model.Id.ToString())
		//	//	.Replace("#PLAN-CODE#", model.PlanCode)
		//	//	.Replace("#PLAN-NAME#", model.PlanName)
		//	//	.Replace("#PLAN-FACE-AMOUNT#", model.PlanFaceAmount.ToString())
		//	//	.Replace("#PLAN-PREMIUM#", model.PlanPremium.ToString())
		//	//	.Replace("#OWNER-NAME-PREFIX#", model.Owner.NamePrefix)
		//	//	.Replace("#OWNER-NAME-SUFFIX#", model.Owner.NameSuffix)
		//	//	.Replace("#OWNER-NAME-FNAME#", model.Owner.FirstName)
		//	//	.Replace("#OWNER-NAME-MNAME#", model.Owner.MiddleName)
		//	//	.Replace("#OWNER-NAME-LNAME#", model.Owner.LastName);

		//	var adminSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
		//	var adminRecipients = new MailAddressCollection();
		//	var adminSubject = emailSettings.Subject;

		//	foreach (var recipient in emailSettings.Recipients)
		//	{
		//		adminRecipients.Add(new MailAddress(recipient));
		//	}

		//	await this.SendAsync
		//	(
		//		adminSender,
		//		adminRecipients,
		//		adminSubject,
		//		body
		//	);

		//	// Send email to user

		//	emailSettings = this.emailSettings.QuoteRequest;
		//	body = this.LoadEmailTemplate(emailSettings.TemplateFile);

		//	//body = body
		//	//	.Replace("#REQUEST-ID#", model.Id.ToString())
		//	//	.Replace("#PLAN-CODE#", model.PlanCode)
		//	//	.Replace("#PLAN-NAME#", model.PlanName)
		//	//	.Replace("#PLAN-FACE-AMOUNT#", model.PlanFaceAmount.ToString())
		//	//	.Replace("#PLAN-PREMIUM#", model.PlanPremium.ToString())
		//	//	.Replace("#OWNER-NAME-PREFIX#", model.Owner.NamePrefix)
		//	//	.Replace("#OWNER-NAME-SUFFIX#", model.Owner.NameSuffix)
		//	//	.Replace("#OWNER-NAME-FNAME#", model.Owner.FirstName)
		//	//	.Replace("#OWNER-NAME-MNAME#", model.Owner.MiddleName)
		//	//	.Replace("#OWNER-NAME-LNAME#", model.Owner.LastName);

		//	var userSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
		//	var userRecipients = new MailAddressCollection();
		//	var userSubject = emailSettings.Subject;
		//	var userEmail = model.Customer.EmailAddress;
		//	var userFullName = $"{model.Customer.NamePrefix} {model.Customer.FirstName} {model.Customer.MiddleName} {model.Customer.LastName} {model.Customer.NameSuffix}";

		//	userRecipients.Add(new MailAddress(userEmail, userFullName));

		//	await this.SendAsync
		//	(
		//		userSender,
		//		userRecipients,
		//		userSubject,
		//		body
		//	);
		//}

		//public async Task SendOrderConfirmationAsync(Order model)
		//{
		//	// Send email to admin

		//	var emailSettings = this.emailSettings.OrderConfirmationAdmin;
		//	var body = this.LoadEmailTemplate(emailSettings.TemplateFile);

		//	body = body
		//		.Replace("#REQUEST-ID#", model.Id.ToString())
		//		.Replace("#PLAN-CODE#", model.PlanCode)
		//		.Replace("#PLAN-NAME#", model.PlanName)
		//		.Replace("#PLAN-FACE-AMOUNT#", model.PlanFaceAmount.ToString())
		//		.Replace("#PLAN-PREMIUM#", model.PlanPremium.ToString())
		//		.Replace("#OWNER-NAME-PREFIX#", model.Owner.NamePrefix)
		//		.Replace("#OWNER-NAME-SUFFIX#", model.Owner.NameSuffix)
		//		.Replace("#OWNER-NAME-FNAME#", model.Owner.FirstName)
		//		.Replace("#OWNER-NAME-MNAME#", model.Owner.MiddleName)
		//		.Replace("#OWNER-NAME-LNAME#", model.Owner.LastName);

		//	var adminSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
		//	var adminRecipients = new MailAddressCollection();
		//	var adminSubject = emailSettings.Subject;

		//	foreach (var recipient in emailSettings.Recipients)
		//	{
		//		adminRecipients.Add(new MailAddress(recipient));
		//	}

		//	await this.SendAsync
		//	(
		//		adminSender,
		//		adminRecipients,
		//		adminSubject,
		//		body
		//	);

		//	// Send email to user

		//	emailSettings = this.emailSettings.OrderConfirmation;
		//	body = this.LoadEmailTemplate(emailSettings.TemplateFile);

		//	body = body
		//		.Replace("#REQUEST-ID#", model.Id.ToString())
		//		.Replace("#PLAN-CODE#", model.PlanCode)
		//		.Replace("#PLAN-NAME#", model.PlanName)
		//		.Replace("#PLAN-FACE-AMOUNT#", model.PlanFaceAmount.ToString())
		//		.Replace("#PLAN-PREMIUM#", model.PlanPremium.ToString())
		//		.Replace("#OWNER-NAME-PREFIX#", model.Owner.NamePrefix)
		//		.Replace("#OWNER-NAME-SUFFIX#", model.Owner.NameSuffix)
		//		.Replace("#OWNER-NAME-FNAME#", model.Owner.FirstName)
		//		.Replace("#OWNER-NAME-MNAME#", model.Owner.MiddleName)
		//		.Replace("#OWNER-NAME-LNAME#", model.Owner.LastName);

		//	var userSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
		//	var userRecipients = new MailAddressCollection();
		//	var userSubject = emailSettings.Subject;
		//	var userEmail = model.Owner.EmailAddress;
		//	var userFullName = $"{model.Owner.NamePrefix} {model.Owner.FirstName} {model.Owner.MiddleName} {model.Owner.LastName} {model.Owner.NameSuffix}";

		//	userRecipients.Add(new MailAddress(userEmail, userFullName));

		//	await this.SendAsync
		//	(
		//		userSender,
		//		userRecipients,
		//		userSubject,
		//		body
		//	);
		//}

		//public async Task SendPaymentReminderAsync(Order model)
		//{
		//	// Send email to admin

		//	var emailSettings = this.emailSettings.PaymentReminderAdmin;
		//	var body = this.LoadEmailTemplate(emailSettings.TemplateFile);

		//	body = body
		//		.Replace("#REQUEST-ID#", model.Id.ToString())
		//		.Replace("#PLAN-CODE#", model.PlanCode)
		//		.Replace("#PLAN-NAME#", model.PlanName)
		//		.Replace("#PLAN-FACE-AMOUNT#", model.PlanFaceAmount.ToString())
		//		.Replace("#PLAN-PREMIUM#", model.PlanPremium.ToString())
		//		.Replace("#OWNER-NAME-PREFIX#", model.Owner.NamePrefix)
		//		.Replace("#OWNER-NAME-SUFFIX#", model.Owner.NameSuffix)
		//		.Replace("#OWNER-NAME-FNAME#", model.Owner.FirstName)
		//		.Replace("#OWNER-NAME-MNAME#", model.Owner.MiddleName)
		//		.Replace("#OWNER-NAME-LNAME#", model.Owner.LastName);

		//	var adminSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
		//	var adminRecipients = new MailAddressCollection();
		//	var adminSubject = emailSettings.Subject;

		//	foreach (var recipient in emailSettings.Recipients)
		//	{
		//		adminRecipients.Add(new MailAddress(recipient));
		//	}

		//	await this.SendAsync
		//	(
		//		adminSender,
		//		adminRecipients,
		//		adminSubject,
		//		body
		//	);

		//	// Send email to user

		//	emailSettings = this.emailSettings.PaymentReminder;
		//	body = this.LoadEmailTemplate(emailSettings.TemplateFile);

		//	body = body
		//		.Replace("#REQUEST-ID#", model.Id.ToString())
		//		.Replace("#PLAN-CODE#", model.PlanCode)
		//		.Replace("#PLAN-NAME#", model.PlanName)
		//		.Replace("#PLAN-FACE-AMOUNT#", model.PlanFaceAmount.ToString())
		//		.Replace("#PLAN-PREMIUM#", model.PlanPremium.ToString())
		//		.Replace("#OWNER-NAME-PREFIX#", model.Owner.NamePrefix)
		//		.Replace("#OWNER-NAME-SUFFIX#", model.Owner.NameSuffix)
		//		.Replace("#OWNER-NAME-FNAME#", model.Owner.FirstName)
		//		.Replace("#OWNER-NAME-MNAME#", model.Owner.MiddleName)
		//		.Replace("#OWNER-NAME-LNAME#", model.Owner.LastName);

		//	var userSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
		//	var userRecipients = new MailAddressCollection();
		//	var userSubject = emailSettings.Subject;
		//	var userEmail = model.Owner.EmailAddress;
		//	var userFullName = $"{model.Owner.NamePrefix} {model.Owner.FirstName} {model.Owner.MiddleName} {model.Owner.LastName} {model.Owner.NameSuffix}";

		//	userRecipients.Add(new MailAddress(userEmail, userFullName));

		//	await this.SendAsync
		//	(
		//		userSender,
		//		userRecipients,
		//		userSubject,
		//		body
		//	);
		//}

		#endregion

		#region Business - Group

		public async Task SendGroupApplicationPaymentProof(GroupApplication application, string contentType, Stream stream)
		{
			var emailSettings = this.emailSettings.GroupApplicationPaymentProof;

			var totalMembers = "Schools and Universities".Equals(application.PlanCode, StringComparison.OrdinalIgnoreCase)
				? $"{application.TotalTeachers} Teachers and {application.TotalStudents} Students"
				: $"{application.TotalMembers}";


			string planCode = application.PlanCode.Contains(" - ")
				? application.PlanCode.Split(" - ")[1]
				: application.PlanCode;

			var body = new StringBuilder(EmailTemplates.GroupApplicationPaymentProof)
				.Replace("#REFERENCE-CODE#", application.ReferenceCode)
				.Replace("#TRANSACTION-DATE#", DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("en-US")))
				//.Replace("#STATUS#", "Forwarded to InLife's Corporate Solutions")
				.Replace("#COMPANY-NAME#", application.CompanyName)
				.Replace("#PRODUCT#", planCode)
				.Replace("#PAYMENT-PROOF#", application.PaymentProofFile)
				.Replace("#MEMBER#", totalMembers)
				.Replace("#PRODUCT-VARIANT#", application.PlanVariantCode)
				.ToString();




			Stream InputStream = stream;
			byte[] result;
			using (var streamReader = new MemoryStream())
			{
				InputStream.CopyTo(streamReader);
				result = streamReader.ToArray();
			}

			//var memStream = new MemoryStream(result);
			//memStream.Position = 0;
			//var contentTypes = new System.Net.Mime.ContentType(contentType);

			//Attachment attachment = new Attachment(memStream, contentTypes);
			//attachment.ContentDisposition.FileName = application.PaymentProofFile;
			//var attachments = new Collection<Attachment>();
			//attachments.Add(attachment);

			var adminSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
			var adminRecipients = new MailAddressCollection();

			var adminSubject = emailSettings.Subject
				.Replace("#REFERENCE-CODE#", application.ReferenceCode);

			foreach (var recipient in emailSettings.Recipients)
			{
				adminRecipients.Add(new MailAddress(recipient));
			}

			await this.SendAsync
			(
				adminSender,
				adminRecipients,
				adminSubject,
				body
				//attachments
			);
		}

		public async Task SendGroupApplicationCancel(GroupApplication application)
		{
			var emailSettings = this.emailSettings.GroupApplicationCancel;

			var totalMembers = "Schools and Universities".Equals(application.PlanCode, StringComparison.OrdinalIgnoreCase)
				? $"{application.TotalTeachers} Teachers and {application.TotalStudents} Students"
				: $"{application.TotalMembers}";

			decimal totalPremiums = 0;
			if (application.TotalMembers.HasValue)
				totalPremiums += application.TotalMembers.Value * application.PlanPremium;
			if (application.TotalTeachers.HasValue)
				totalPremiums += application.TotalStudents.Value * application.PlanPremium;
			if (application.TotalStudents.HasValue)
				totalPremiums += application.TotalStudents.Value * application.PlanPremium;

			string planCode = application.PlanCode.Contains(" - ")
				? application.PlanCode.Split(" - ")[1]
				: application.PlanCode;

			var body = new StringBuilder(EmailTemplates.GroupApplicationCancel)
				.Replace("#REFERENCE-CODE#", application.ReferenceCode)
				.Replace("#TRANSACTION-DATE#", DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("en-US")))

				.Replace("#COMPANY-NAME#", application.CompanyName)
				.Replace("#COMPANY-ADDRESS#", application.CompanyAddress)
				.Replace("#COMPANY-CONTACT-NUMBERS#", BuildContactNumbers(application.CompanyMobileNumber, application.CompanyPhoneNumber))

				.Replace("#REPRESENTATIVE-NAME#", application.RepresentativeFullName)
				.Replace("#REPRESENTATIVE-CONTACT-NUMBERS#", BuildContactNumbers(application.RepresentativeMobileNumber, application.RepresentativePhoneNumber))
				.Replace("#REPRESENTATIVE-EMAIL#", application.RepresentativeEmailAddress)

				.Replace("#PRODUCT#", planCode)
				.Replace("#PRODUCT-VARIANT#", application.PlanVariantCode)
				.Replace("#TOTAL-MEMBERS#", totalMembers)
				.Replace("#PREMIUM-PER-HEAD#", application.PlanPremium.ToString("#,##0.00"))
				.Replace("#PREMIUM-TOTAL#", totalPremiums.ToString("#,##0.00"))

				.Replace("#CANCEL-REASON#", application.CancellationReason)
				.Replace("#CANCEL-COMMENTS#", application.CancellationComments)
				.ToString();

			var adminSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
			var adminRecipients = new MailAddressCollection();

			var adminSubject = emailSettings.Subject
				.Replace("#REFERENCE-CODE#", application.ReferenceCode)
				.Replace("#COMPANY-NAME#", application.CompanyName)
				.Replace("#REPRESENTATIVE-NAME#", application.RepresentativeFullName);

			foreach (var recipient in emailSettings.Recipients)
			{
				adminRecipients.Add(new MailAddress(recipient));
			}

			await this.SendAsync
			(
				adminSender,
				adminRecipients,
				adminSubject,
				body
			);
		}

		public async Task SendGroupApplicationReferenceCode(GroupApplication application)
		{
			var emailSettings = this.emailSettings.GroupApplicationReferenceCode;

			var recipient = new MailAddress
			(
				address: application.RepresentativeEmailAddress,
				displayName: $"{application.RepresentativeNamePrefix} {application.RepresentativeFirstName} {application.RepresentativeLastName}".Trim()
			);

			var body = new StringBuilder(EmailTemplates.GroupApplicationReferenceCode)
				.Replace("#RECIPIENT-NAME#", recipient.DisplayName)
				.Replace("#CODE#", application.ReferenceCode)
				.ToString();

			var sender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
			var recipients = new MailAddressCollection();
			var subject = emailSettings.Subject;

			recipients.Add(new MailAddress(recipient.Address, recipient.DisplayName));

			await this.SendAsync
			(
				sender,
				recipients,
				subject,
				body
			);
		}

		public async Task SendGroupApplicationOtp(GroupApplication application)
		{
			var emailSettings = this.emailSettings.GroupApplicationOtp;

			var recipient = new MailAddress
			(
				address: application.RepresentativeEmailAddress,
				displayName: $"{application.RepresentativeNamePrefix} {application.RepresentativeFirstName} {application.RepresentativeLastName}".Trim()
			);

			var body = new StringBuilder(EmailTemplates.GroupApplicationOtp)
				.Replace("#RECIPIENT-NAME#", recipient.DisplayName)
				.Replace("#CODE#", application.Otp)
				.ToString();

			var sender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
			var recipients = new MailAddressCollection();
			var subject = emailSettings.Subject;

			recipients.Add(new MailAddress(recipient.Address, recipient.DisplayName));

			await this.SendAsync
			(
				sender,
				recipients,
				subject,
				body
			);
		}

		public async Task SendGroupApplicationThankYou(GroupApplication application, string[] benefits)
		{
			var totalMembers = "Schools and Universities".Equals(application.PlanCode, StringComparison.OrdinalIgnoreCase)
				? $"{application.TotalTeachers} Teachers and {application.TotalStudents} Students"
				: $"{application.TotalMembers}";

			var applicationBenefits = new StringBuilder();

			if (benefits != null)
			{
				for (int i = 0; i < benefits.Length; i++)
				{
					applicationBenefits.Append(benefits[i] + @"<br/>");
				}
			}

			decimal totalPremiums = 0;
			if (application.TotalMembers.HasValue)
				totalPremiums += application.TotalMembers.Value * application.PlanPremium;
			if (application.TotalTeachers.HasValue)
				totalPremiums += application.TotalStudents.Value * application.PlanPremium;
			if (application.TotalStudents.HasValue)
				totalPremiums += application.TotalStudents.Value * application.PlanPremium;

			// Send email to admin

			var emailSettings = this.emailSettings.GroupApplicationCompleteAdmin;

			var body = new StringBuilder(EmailTemplates.GroupApplicationCompleteAdmin)
				.Replace("#REFERENCE-CODE#", application.ReferenceCode)
				.Replace("#TRANSACTION-DATE#", DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("en-US")))
				.Replace("#COMPANY-NAME#", application.CompanyName)
				.Replace("#COMPANY-ADDRESS#", application.CompanyAddress)
				.Replace("#COMPANY-CONTACT-NUMBERS#", BuildContactNumbers(application.CompanyMobileNumber, application.CompanyPhoneNumber))

				.Replace("#REPRESENTATIVE-NAME#", application.RepresentativeFullName)
				.Replace("#REPRESENTATIVE-CONTACT-NUMBERS#", BuildContactNumbers(application.RepresentativeMobileNumber, application.RepresentativePhoneNumber))
				.Replace("#REPRESENTATIVE-EMAIL#", application.RepresentativeEmailAddress)

				.Replace("#PRODUCT#", application.PlanCode)
				.Replace("#PRODUCT-PLAN-VARIANT#", Regex.Replace(application.PlanVariantCode, @"\d", ""))
				.Replace("#PRODUCT-VARIANT#", application.PlanVariantCode)
				.Replace("#TOTAL-MEMBERS#", totalMembers)
				.Replace("#PREMIUM-PER-HEAD#", application.PlanPremium.ToString("#,##0.00"))
				.Replace("#PREMIUM-TOTAL#", totalPremiums.ToString("#,##0.00"))

				.Replace("#PAYMENT-MODE#", PaymentMode.FromId(application.PaymentMode).Name)
				.Replace("#PAYMENT-AMOUNT#", totalPremiums.ToString("#,##0.00"))

				.Replace("#FEEDBACK-RATING#", application.FeedbackRating.ToString())
				.Replace("#FEEDBACK-MESSAGE#", application.FeedbackMessage)
				.Replace("#BENEFITS#", applicationBenefits.ToString())

				.ToString();

			var adminSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
			var adminRecipients = new MailAddressCollection();

			var adminSubject = emailSettings.Subject
				.Replace("#REFERENCE-CODE#", application.ReferenceCode)
				.Replace("#COMPANY-NAME#", application.CompanyName)
				.Replace("#REPRESENTATIVE-NAME#", application.RepresentativeFullName);

			foreach (var recipient in emailSettings.Recipients)
			{
				adminRecipients.Add(new MailAddress(recipient));
			}

			//await this.SendAsync
			//(
			//	adminSender,
			//	adminRecipients,
			//	adminSubject,
			//	body
			//);

			// Send email to customer

			emailSettings = this.emailSettings.GroupApplicationThankYou;

			var userRecipient = new MailAddress
			(
				address: application.RepresentativeEmailAddress,
				displayName: $"{application.RepresentativeNamePrefix} {application.RepresentativeFirstName} {application.RepresentativeLastName}".Trim()
			);

			body = new StringBuilder(EmailTemplates.GroupApplicationThankYou)
				.Replace("#RECIPIENT-NAME#", userRecipient.DisplayName)
				.Replace("#REFERENCE-CODE#", application.ReferenceCode)
				.Replace("#TRANSACTION-DATE#", DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("en-US")))

				.Replace("#COMPANY-NAME#", application.CompanyName)
				.Replace("#COMPANY-ADDRESS#", application.CompanyAddress)
				.Replace("#COMPANY-CONTACT-NUMBERS#", BuildContactNumbers(application.CompanyMobileNumber, application.CompanyPhoneNumber))

				.Replace("#REPRESENTATIVE-NAME#", application.RepresentativeFullName)
				.Replace("#REPRESENTATIVE-CONTACT-NUMBERS#", BuildContactNumbers(application.RepresentativeMobileNumber, application.RepresentativePhoneNumber))
				.Replace("#REPRESENTATIVE-EMAIL#", application.RepresentativeEmailAddress)

				.Replace("#PRODUCT#", application.PlanCode)
				.Replace("#PRODUCT-PLAN-VARIANT#", Regex.Replace(application.PlanVariantCode, @"\d", ""))
				.Replace("#PRODUCT-VARIANT#", application.PlanVariantCode)
				.Replace("#TOTAL-MEMBERS#", totalMembers)
				.Replace("#PREMIUM-PER-HEAD#", application.PlanPremium.ToString("#,##0.00"))
				.Replace("#PREMIUM-TOTAL#", totalPremiums.ToString("#,##0.00"))

				.Replace("#PAYMENT-MODE#", PaymentMode.FromId(application.PaymentMode).Name)
				.Replace("#PAYMENT-AMOUNT#", totalPremiums.ToString("#,##0.00"))
				.Replace("#BENEFITS#", applicationBenefits.ToString())

				.ToString();

			var userSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
			var userRecipients = new MailAddressCollection();
			var userSubject = emailSettings.Subject;

			userRecipients.Add(new MailAddress(userRecipient.Address, userRecipient.DisplayName));

			await this.SendAsync
			(
				userSender,
				userRecipients,
				userSubject,
				body
			);
		}

		public async Task SendGroupApplicationFeedback(GroupApplication application)
		{
			// Send email to admin

			var emailSettings = this.emailSettings.GroupApplicationFeedbackAdmin;

			var totalMembers = "Schools and Universities".Equals(application.PlanCode, StringComparison.OrdinalIgnoreCase)
				? $"{application.TotalTeachers} Teachers and {application.TotalStudents} Students"
				: $"{application.TotalMembers}";

			decimal totalPremiums = 0;
			if (application.TotalMembers.HasValue)
				totalPremiums += application.TotalMembers.Value * application.PlanPremium;
			if (application.TotalTeachers.HasValue)
				totalPremiums += application.TotalTeachers.Value * application.PlanPremium;
			if (application.TotalStudents.HasValue)
				totalPremiums += application.TotalStudents.Value * application.PlanPremium;

			var body = new StringBuilder(EmailTemplates.GroupApplicationFeedbackAdmin)
				.Replace("#REFERENCE-CODE#", application.ReferenceCode)
				.Replace("#TRANSACTION-DATE#", application.CompletedDate.ToString())
				//.Replace("#TRANSACTION-STATUS#", application.Status)
				.Replace("#COMPANY-NAME#", application.CompanyName)

				.Replace("#FEEDBACK-RATING#", application.FeedbackRating.ToString())
				.Replace("#FEEDBACK-MESSAGE#", application.FeedbackMessage)

				.ToString();

			var adminSubject = emailSettings.Subject
				.Replace("#REFERENCE-CODE#", application.ReferenceCode)
				.Replace("#COMPANY-NAME#", application.CompanyName)
				.Replace("#REPRESENTATIVE-NAME#", application.RepresentativeFullName);

			var adminSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);

			var adminRecipients = new MailAddressCollection();
			foreach (var recipient in emailSettings.Recipients)
			{
				adminRecipients.Add(new MailAddress(recipient));
			}

			await this.SendAsync
			(
				adminSender,
				adminRecipients,
				adminSubject,
				body
			);
		}

		public async Task SendGroupApplicationsCompletedBatch(ICollection<GroupApplication> applications)
		{
			// Send email to admin

			var emailSettings = this.emailSettings.GroupApplicationsCompletedBatch;
			StringBuilder body;

			if (applications.Count == 0)
			{
				body = new StringBuilder(EmailTemplates.GroupApplicationsCompletedBatchEmpty);
			}
			else
			{
				body = new StringBuilder(EmailTemplates.GroupApplicationsCompletedBatch);
				var list = new StringBuilder();

				list.Append(@"
				<table style=""border-collapse:collapse; font-size:0.8em;"">
					<thead>
						<tr>
							<th style=""border:solid 1px #ccc; padding:2px 4px;"">Reference Code</th>
							<th style=""border:solid 1px #ccc; padding:2px 4px;"">Transaction Date</th>
							<th style=""border:solid 1px #ccc; padding:2px 4px;"">Company</th>
							<th style=""border:solid 1px #ccc; padding:2px 4px;"">Plan</th>
							<th style=""border:solid 1px #ccc; padding:2px 4px;"">Variant</th>
							<th style=""border:solid 1px #ccc; padding:2px 4px;"">Members</th>
						</tr>
						</thead>
						<tbody>");

				foreach (var application in applications)
				{
					var totalMembers = "Schools and Universities".Equals(application.PlanCode, StringComparison.OrdinalIgnoreCase)
						? $"{application.TotalTeachers} Teachers and {application.TotalStudents} Students"
						: $"{application.TotalMembers}";

					decimal totalPremiums = 0;
					if (application.TotalMembers.HasValue)
						totalPremiums += application.TotalMembers.Value * application.PlanPremium;
					if (application.TotalTeachers.HasValue)
						totalPremiums += application.TotalTeachers.Value * application.PlanPremium;
					if (application.TotalStudents.HasValue)
						totalPremiums += application.TotalStudents.Value * application.PlanPremium;

					list.Append(@"
					<tr>
						<td style=""border:solid 1px #ccc; padding:2px 4px; text-align:center;"">
							#REFERENCE-CODE#
						</td>
						<td style=""border:solid 1px #ccc; padding:2px 4px; text-align:center;"">
							#TRANSACTION-DATE#
						</td>
						<td style=""border:solid 1px #ccc; padding:2px 4px; text-align:center;"">
							#COMPANY#
						</td>
						<td style=""border:solid 1px #ccc; padding:2px 4px; text-align:center;"">
							#PLAN-CODE#
						</td>
						<td style=""border:solid 1px #ccc; padding:2px 4px; text-align:center;"">
							#PLAN-VARIANT-CODE#
						</td>
						<td style=""border:solid 1px #ccc; padding:2px 4px; text-align:center;"">
							#TOTAL-MEMBERS#
						</td>
					</tr>")
						.Replace("#REFERENCE-CODE#", application.ReferenceCode)
						.Replace("#TRANSACTION-DATE#", application.CompletedDate.HasValue ? application.CompletedDate.Value.ToOffset(TimeSpan.FromHours(8)).ToString() : "")
						.Replace("#COMPANY#", application.CompanyName)
						.Replace("#PLAN-CODE#", application.PlanCode)
						.Replace("#PLAN-VARIANT-CODE#", application.PlanVariantCode)
						.Replace("#TOTAL-MEMBERS#", totalMembers);
				}

				list.Append(@"
					</tbody>
				</table>");

				body.Replace("#BATCH-LIST#", list.ToString());
			}

			var adminSubject = emailSettings.Subject;
			var adminSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);

			var adminRecipients = new MailAddressCollection();
			foreach (var recipient in emailSettings.Recipients)
			{
				adminRecipients.Add(new MailAddress(recipient));
			}

			await this.SendAsync
			(
				adminSender,
				adminRecipients,
				adminSubject,
				body.ToString()
			);
		}


		#endregion

		private StringBuilder LoadEmailTemplate(string filename)
		{
			string basePath;

			// If Azure Function running locally
			if (!String.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("AzureWebJobsScriptRoot")))
				basePath = Environment.GetEnvironmentVariable("AzureWebJobsScriptRoot");

			// If in Azure
			//else if (!String.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("HOME")))
			//	basePath = $"{Environment.GetEnvironmentVariable("HOME")}\\site\\wwwroot";

			else
				basePath = this.hostingEnvironment.ContentRootPath;

			//var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			//var basePath = this.hostingEnvironment.ContentRootPath;
			//var basePath = Directory.GetCurrentDirectory();

			var directory = $"EmailTemplates"; // TODO: Retrieve this from appsettings
			var path = Path.Combine(basePath, directory, filename);

			if (!System.IO.File.Exists(path))
				throw new FileNotFoundException($"Couldn't find EmailTemplate file: {path}");

			string body;
			try
			{
				using var reader = System.IO.File.OpenText(path);
				body = reader.ReadToEnd();
			}
			catch (Exception ex)
			{
				throw new FileLoadException($"Couldn't load EmailTemplate file: {path}", ex);
			}

			return new StringBuilder(body);
		}

		private string BuildContactNumbers(string mobile, string phone)
		{
			if (!String.IsNullOrWhiteSpace(mobile) && !String.IsNullOrWhiteSpace(phone))
				return $"{mobile} (Mobile), {phone} (Phone)";

			if (!String.IsNullOrWhiteSpace(mobile))
				return $"{mobile} (Mobile)";

			if (!String.IsNullOrWhiteSpace(phone))
				return $"{phone} (Phone)";

			return null;
		}


	}
}
