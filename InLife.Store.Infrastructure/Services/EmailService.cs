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


			//string assemblyPath = Assembly.GetExecutingAssembly().Location;
			//string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
			//string logoPath = Path.Combine(assemblyDirectory, "Assets\\Images", "logo.jpg");
			//LinkedResource Img = new LinkedResource(logoPath, MediaTypeNames.Image.Jpeg);
			//Img.ContentId = "InLifeLogo";

			//Img.ContentId = "Logo";
			//Img.TransferEncoding = TransferEncoding.Base64;

			//var img = string.Format("<img src=\"cid:{0}\" />", Img);
			//var htmlView = AlternateView.CreateAlternateViewFromString(img, null, MediaTypeNames.Image.Jpeg);
			//htmlView.LinkedResources.Add(Img);


			//var imgLogo = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQH/2wBDAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQH/wAARCACQAfQDAREAAhEBAxEB/8QAHwAAAQQDAAMBAAAAAAAAAAAAAAcICQoEBQYBAgML/8QAaBAAAQMDAgQDAwUKBA0PCAsAAQIDBAUGEQAHCBIhMRNBUQlhcRQigZGxFRYjMjd3obbB8ApCUtEYGSUzNThWdHaztdbxFzZTWHJzdYKSlJWWtLfhGiQ0VVey0tQmKENGYoaTl6an1f/EAB4BAAEFAQADAQAAAAAAAAAAAAAFBgcICQQCAwoB/8QAaxEAAQMCAwUEBQYEDQwMDQUAAQIDBAURAAYhBxIxQVEIE2FxFCKBkfAVMqGxwdEjNna1CRYYN0JSVnWVtNTh8RckNDVXcnN0lJa21RklMzhTYmWCkqSz1iZDRUZUVWNmk7LCxNMnhIWixf/aAAwDAQACEQMRAD8A/P8A9GDBowYNGDBowYNGDBowYNGDBowY2VGpUqu1ilUSCEqm1ipQaVDCjhJlVCU1EjhRGSEl15AJAOBnXqfebjsvSHVBDTDTjzqzwS20grWo+CUpJPlj2sMuSHmY7SSt191tlpA4qcdWEISPFSlADzw9Dj+4TneETe6PY1PZqTtmV+zbYuO06xP8R4VJaqVFgXQ0ZhSGlzItyxag9Jho5VQo0+ngNIjPxFORZsa2ltbU8nqzAW2I86PWKrTZ8JlVxELMpb9PQQpSlErpMiAtThIDrpdWgJHqJlPbLs0d2WZvTl/vH5EKRR6VUoE14AGUHoqWZ6gUpSgBFVjzkJbFy00GkqUo+upjupYxE+FO2UtmNem8O1lpTEpXDuTcKz6LMQsZS5DqFfgRpTah5hcdxxBHv0jZiqJpGX65VRoaZSKlPB8YcN6QNNb6t8La8MLeWaZ8tZjoFHA3vlWtUunW4XE2cxGIvy0c48uJxxtzpeRclwokMJiyEVyrJfjJQG0x3kz5AdYS2OiEsrCmwgdEhPKOg0rotupsbjdFiTckWFjfnfrhGVfeVcAHeNwNADfUAdMaPXljxwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGHa8PfBJxA8UVNnVXZih2rczFLeUzVYr+4Vk0ms0shSktOVC36jXGK9EjSyhwwZT1NTGmhp1UZ10NOFMdZ42q5L2cqjDN0+dTETB/Wr6aLWJkR9YCiWW5kOC/FVISlKlqjh7v0oG+psIIJkXI+yrOm0ZMk5RgQamuHYymFVqjxJbCCQlLzkKXOZlpjqUoITILPcKX6gcKgRharj9kxx423Hdku7L/dhplsuEW9eFm1h9YCSopbiRq58rWsAH5gY5icBIUVDLQgdpPYxUFBDecmYxJABn02rQU3JsLrkwW0JHVSlBIGpIGuHlO7NG2qAgrXkt+SACSIFSpE5ZAF/VbjzluKNuASkqJuACcN6sbajcPaXiN2Yt7duwrqsOc5ulYK1QLtoVQoqpkNu7aQmQ/DXNYaanxEhQSqTCckMZUAHMkaf9Rr1GzFkvMVRy7V6dWoaqHWUtzKTNjzmO9TT5F2+9jOOIS6k23m1EOJOhSDpiP6bl6t5czvlyl5ko1Sok4V6iqchVeDJgSC0uoxwF91KabWppYvuupSptY1SojXFtz2i3B/T+MLZByiUpceFuhYq5lybbVN0Nht+oqi8lQtec+4nmZpdyssR47jqFoMWoRabOX4zURyM/mLsE2nvbLc2iRKStzLddQxAzAwgkqaaQ6oxaoy2Ad96mredUW90KeivSWkkOLbI1Y2/bHEbV8mmLB7tnM9BVIqGXH1hIQ86ttIl0l5ZUnu2KmhllAdvusymYzy95tDoVSnum1rism461aN20aoW9c1u1GTSa3RapHciT6bUIjhbfjSWHAFJUlQ5kLHM262pDzS1tLQtWssGdDqcOLUafKYmwJ0dqVDlxnUPR5MZ9CXGX2XWypDjTjakrQtKilSSCDjHyoU+dSZ0yl1OJIp9Rp8l6HOgy2VsSokqO4pp+PIZcCXGnWnEqQtC0hSVAgjHebCVyNbW+G0FfmFCYdI3MsifLU4oIQiKxcdOXIcWskBKW2QtalEgAJySO+kvNUByq5YzHTGk7ztRoVWgtJ19ZyVAkMIGhHFawOIws5MqTdHzflWrPK3GaZmOiVB5WnqtQ6lGkOK1BGiG1HUHCh8Z9hO7a8U++lrKYVGjDcOv12ltKQEJTR7rlquekpaAABZbgVZhppQHzkNg+ekTZnXjmfIGUa2twOPzaDT/AExfWexHRGn3HL+u2XjY6gEXw5NsOVjkvahnrLSWizHpuZaoIKDypsmQuZTdbC/9YyI9yBa4OGxafOI2waMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGMyn06fVpsenUuFKqE+W54UaHDYckSX14KilplpKlrISlS1EDCUJUtRCUkgx0RIcuoSo8GBFkTZst5uPFhxGHZMqS+6oIaYjx2UrdeecWQltttClrUQlKSSBhx9ucI28txMCQKZR6OlaErZRV6slLr/ADZ+ahFNj1ItqGOokeCCCCkqHXXM/LYjAl5e6ALmwKtPIAnFg6N2UdutbjIlR8mGGhxIUlqq1ej0yVYi4C4suc3IaVbil1tCkn1VAG4HD7g7Bbs7Yw/und1oTolHC0NqrMNbFTpTanVltkSJkBx9ETxl8qW/lYY5nFttf11Yb1zRKxTJrpjxpjDkhKSox97cf3U/OUGl7q1JTpvKSkpTcXIuLsfPuxTajszYRMznk6q0qmOPJjN1hKG59GVIcClNRzVae5JgtPupStTLDzzbzyW3C2hYbWUo5pSxFmDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgw6nZ/gj4q99oMSr7Z7I3tWrfngKhXNNpwt+25jZOC9BrlfcptPqDCT81b1PelIQrKVqSoEaj/NW1XZ3kp4xczZuotNnBIWaaqUmTVAhQJStVNiB+ahC7HdcWwltVjZRxIGVdle0POrQk5ZyjWanBKlIFSTFVGphWk2WhNRlliEtxB+e22+txPNOHpWh7GbjZZm06tVqwdqpcaK+iRJtS69x5DDFTaSTzQpz9lyGZ7Dbv8ZdPrsJ9Jxh9I5gYpm9qrY3uvRmcx1pDi0LbRMg5dnOLZUQQHWRNguMKUj5yQ9HebJ0U2oXTiVoHZU2yqWzIdy5RloStDioc7MMFtLyQQotPehzW30JUPVX3Uhl0AnccSbKEj22HCVbFlhujcQPsvbRepKEJZXfOzF+u7mBnmxzyJtrXLdTd3qjNgczkinzqtUgfwcemP5KhB+Ydp1RrXeSch9piQxIBKmaRnTK9Iy82sEk92K3DyxHgNrA0QZkJqOo2DsqOnecE6Zc2Uw6OW4+fOzK1IjFAS/V8kZoq2Y3myBbvDQ5eZ5FQdQTq4IU1ySlIUWY0hdkYc3QvZ78Fd3vIv3YJm+9ib9o0hLP3y7Z3felm3ha9QKESDS67Z12vTY1KW62tpcqlVS2oq5kNbaklUd1l5Ufz9uO2WhpNFz+xQM6UepsB5EWv0WizqTVYJVuplUyqZeTChTmSofg5bT0xLSxxS6myZRpOwDYzmJQrez6RmDJdZpb6mXJOXq3W4NYpM8JCjFqtKzEqZOp74SfwsN5iEp1sn5zarl8e3DO6VsRjbO41xUvcFqBCYTStwoVPat2s1XwstPMXbbjDjtMZqq0eBIRV7fdRTak6uahyh0BMWIKjDOYRlOpFNTy5AmZdeffV6blt59VRp0UKG8l+jVZzdlriFQUhcCotGTE3m+5nT21L9Hn3KlPzhTAqlZlnQcxR2GEGDmdhhNNqMrdVuKj1yjo3oiJYQUrbqFMeEaWA531PgOpSZO7vK17PvqAil3nbVBuanNSmJrMSuUyJUmo8yK4l2NMj/KmnPk8uO4hDjMljw32lJCm3E65KNUKtQ5CpNFqM6lyHG1MuvQZL0VbzK0lKmXu6Wjvm1pKgW3d5sgn1ddXPVMtUbMEZuJXKTT6tGbdS80zUIjMpDTyFJWh5gPIWWXUqSkpdZ3XBuiytNM56opSMJPQfNHU4wBg+8gdPf3OfLXrbik2vfXW1hxN/Z7+egw4mohPBNgOBPD6Tf6efDEZfHJ7PzbTi1iOXXTXoti7yU+Epin3lGihUK4GmW1CLSrwiMhKp8VpfKiNU2v6p05BKW1PxiqIuw2x/bTXtmy0UqWh2tZTddCnKWpwCTTVLUS9IpLrnqtqWVFxyC6oRX1puhcZ1xx9VeduPZhy9teYVWKa6zl7PLDJQzWUsEwqsltsJjw66w1660I3A2xUWEKmRW1FK25rLTUZFXDfThk3r4b64aRunZlRorC5bsWlXNEQ5OtSuLZ5lhdHr7LaYry1sp+Uphv8AyWptMEOSYLBylOhOU88ZXzvCM7LdVjzkoCDJi37qfCKwSlMyE5uyI5VZQQtSO6cKVd04sJJGU20HZZnzZdUk03OuXplJLynEwZ5R39JqaWiA4um1NneiS9wKQXW0Od+wFo79lorSC9fjJoauIPh32K4zaCz90a9HtembW77PwkJUY1yW84abTbhqrLePkxnT3HIa33E9WanbsUK8FMZTkZbNpX6UM6Zx2YzN2PFXUJWbMmocIbQ5Sau4uRNp8IEgLahSA8tptG8sFuoqUVJYUUTrtooZ2g7KNm23mkoXNks0qHs92mOsJU8uLmKgNoiU2r1QpClMPVSIqM08+8UNKD9FQgJcmNh2KvU84qRg0YMGjBg0YMGjBg0YMGjBg0YMGjBg0YMGjBg0YMGjBg0YMGjBg0YMGjBg0YMGjBg0YMGjBg0YMGjBg0YMGjBg0YMGjBg0YMGjBg0YMSpcNmz1PtG26fXJsVqRcldisVCTKdaQXYMeUyh1mnx1YUpttptaQ+ObL8nxXFEI8BtnydWlhkrVooi+vJOlraXF/puOGhxrD2Vti1MyflOmZ0qsNmTm/M8RuoNSH2QpyiUiUkrhQoRXctuy4qkS58hCW3Fl5MMFTMfvHn42zSCtTfzCR83193n6/Se/XGNRlmSrJaQ4SuxsfMe0ngPA87YvHSoRUUi17fWeoPXXzvhwdHtCm16C9R6tTotSpk9hyJOgzGESIsmO+gtvMusupWhba21qQpKkkKSojt01VHPOcJMJ8OwZTsaW0vvGX2HFNusrQdFoWgpUlQ5WIuDZVxoZNby3SKpRp1Or9Nh1Wkz4zkWdTajGalQ5sd0EKYkR3krbcbUACd5JKVJC0kLCSIB+OLhuZ4ct3E06hpeFk3pAcuW1EugqNPQZKmapQg8QA8ilSVtGOpX4ZECZCbfLjqTIfs/sb2iHaJlMTpQQmsUqUql1cNgBDz7baHGJraQAEJmR1ocWgAJRIS+hH4NKCcAu1TsYibFdqMyj0RTi8p1+N8v5YDylLehQ333WZNIedUSXlUuY24wy8oqW9CVEcePpCngGY6lrFa8GjBg0YMZ0KmVKpOlmnU+dUHgAS1CiPynQDnBLbDbi8HBwcdcH0OjHklC16JQpR6JSVfUDjJm0CvU1PPUaJV4CMA882mzIqcEkA8z7LYwSCAc9SDjtox+qbcT85taf75Ch9YGNQQR3BHx6aMeGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGDRgxm02I1PqEKE/UIVJZlyWY7tTqQmGn09t1xKFzJop0OoTzGjpJdeEKDMlFtKgxGec5W1eDiihC1pbW6pCFKS02UBxwpBIbQXFtthayN1JccQi5G+tKbqHm2kLcQhTiGkrWlKnXAsobCiAXFhtDjhQgHeUG21rsDuIUqyTPDwb3f7JnYB6lT6/VLz353efVGSivXBtDc1Qo1NqCnEpEe0LM+STYjRLykeDUKgipVhwJAZkRA85ENPdqVI7Ted0vxqQ1Q8l5ZCnLQKfmdhqszGrkIVVKu2ygIO4CVxIEmPFutSHlzAhDibg7LKv2ZckKYlVl2uZzzKQ3efPyw+7R4btgVimUlbqysBZsmVOjSJR3ErZRDK1tGyLthu9a+6FIRPtShX1Qae2y2WGby22vTb1S2AEpaMNi6qHSEyGSkp8IxS4hSEkt5QhRFBc0ZMq2VZZYq86hS5bjrgfTSMyUauvtvJJLnpqKbNlSI7iySbykNqUre1KgbaC5QzTSs2xEv0eBX4kNtposKq+Wa3QIzrBFkCE5U4EWPJbQAEkRHHEti37Eg4UZ2UACVK+s9/T3fb+jTfbjk2sn3j7OJtr1w+G446eYsPZc/ff38Nc7PAzj6ycdz5dc4+AGfLprsbijS+vhy9w8epHjjsQx0HPS308dfd44559cJMpU7wI6ZpZ+TqlJZbTJUwV+J4KngkOKaDh5w2pRQFHmABJypttvd0GAtfcBwuhnePdJcUkAuBF9wLUAAVhN1AAEkAW97cBnvjIDLfpBbDReShIdU0k7yW1OAbxQklRSlSikEkhNyTjXv1M9cHA8+vbPl3xn3dfQa6m4g8z/Rrw4deWuFNuGeY3fZc+duXuHDGjk1IZJUv6Sewx16dDj6uvb3qDUThYey3u6/TccfYpMw+Fk38Tqfq8dDra2NFJq2M4P6cD9uT6d++lFqHw0+jX4J4g6HlxwpNQr2uLnyudOHgPttjn5VWxnK+2egOB+/r0Hfvg6UWofDS3svy5eXMa2tphUZgk2sm3sueH0eA4addccHdLNAuelzKJclJplfpE1stS6XV4UeoQJTXRRS9FltusuJChzJKkkggKBCgCF+lqn02S1Mp0uVBltatyYb7seQjgTuusqQsA2BsFWNrG9tPdOyrSq9BdpdcpcCsU6QN2RBqcOPPiO8QO8jyW3WVEBRAJTvJv6vPDcKJw+bIWZRr/te17Lg0a0NzGVt3hZsSTNNqT31RvkS50ShPSnYFGmOQw3GeeorMAPpjxXHkuSI0d1qQpeeM31WRQ51QqZk1TLjxcpNXVGYTUmkqKFdy/Jabb9LYKkXKZSHlub7odcWh1aSycvdnbZdl6FnGj0XLZg5dz7FRGzJldE6a9l+StKXUemQqfJeeFLm926EpcprsVpruoyo7DTkZpaa3PF9wh3Rw2XbJmU9qXXNq6zNV961zhKnnIPihTiaDcSm20txarH5HUx3/wD0aqxW0SmVtyflcKJdvZ1tDg55pid/u4lditD5Sp2qQSCEGXD3lFTkR0lJ4qXHWruXSfwbjuNvaT7NOZ9geYVO91KquQKvKWnLOZtzfAKkKeFHrKm20NRaxHQlzdG6hmosNKmRAAJDEZmWpIxWLBowYNGDBowYNGDBowY7Hbu1UX1uBY1kLmqpqLxvG2bVXUUMCSuAi4a1BpCpqYxdZEhUUTC+lgvNB0thsuthXOO6mQ/lGpU+nlzufTp0SH3u53ndelPtsd5ubyN/c397c30b1rbyb3CVXal8i0SsVjufSPkmlVCpej953Xf+gRHpXc97uOd33vdbned2vc3t7cVbdM6g9iRQiAf6ICrdRn/WFD/zl1dj9Rkj+6Ir/NYf94cZan9E7AJH9Rk6f+/g/wC6OPP9JIoX+2Aq3/UKH/nLo/UZI/uiK/zWH/eHB/snY/uMn/Pwf90sM54u/ZkX3w12W9uZbN3NbmWNTFsoudaKG7RK9bTcl6PFjVF+C1OqseoUhUl4NS5rL8Z2nlbLj0VcUvyo8R7VezpmHZtSlZii1WPmSgMKZbnyWobtPm05b7qWWnZEMvzGlQ1vONMiS3MUpLziUusNpUlarGdnztq5M245hTkydQJmSc3SkSXqRBk1Fmr0ystRGFyZDMOpJi055uotR2n5CoT9PShcdla2JTy0qaTF/quuLp4NGDBowYNGDBowYNGDBowYNGDBowY+0dr5RIYYCggvPNNBRGQnxFpRzEDBIHNnAIzjGjH4TYE9BfE39N9ircFRp1PqA4hKO0J8GJNDR22mLLQlMNv+Hz/fqjn5PE5efkTzY5uVOcC5ELseV2ZDiTE51pKEyozElKDSphKA+0h0JJEoAlIVYkAAkXxmlU/0SfKdMqVQpq9l+YXV0+dLgqdTmCnJS4qJIcYU4lJgEpCy2VBJJIBsSeOM3+kk3D/tiKP/APtpM/z310/qM69+7ikfwTN/lWOH/ZNco/3LMxf5w03/AFfhId6PZB7w7cWdUrusW+qFuouixZNQqtux6HMtiurgRWvGedojL1TrcWrSW20uuLguSqe+423ywxLkrRGLQzj2Us85apUqsUyp0rMrEFhyTKiRESYdSDLSFOOuRoz6HGZIbQkqU2mUmQrg0y4rTEkbNf0QTZTnnMEDLdcolfyRLqspqFT6hUXYVRoipMhaWmGpk2KtmRCU86tLaHVwFxUE3fkspG8YjCCklKgUqSSFJIIIIOCCD1BB6EHqDqruL6ceGPGjBg0YMGjBg0YMGjBiwfbUGLKCZEEJXCfccehqQQpC4jrilx1JIJSpKmVIKVBSgtOCFEYygVqoBCV6hIAPO3DQc9OmnA89cfQfklmNIy7l12EEGE5Q6Q5E7sep6MuBGUxugcElopsOSQBxw4u1aN/W/mdeh/Fx3xj+cnsDqueeMxJYaeusADevrzF/aNfA9NbYm3L9MK3EerzHjx48tSfPzsTh0llUDAZWUZJ5TjHmcY8j6devn07aprmqtqkPPOqWTvFQSCbncB5fHmMOiry0Rmgw2oWQLHd0uoA/XwFtLDjriKL20bdHiU3h2iFCTX1P7kSG1JxzN0jwrKbeDgzzAPzDHLJUnC/k7wSrKFgWU7GTkp9G0h9e/wChmTldpkm+56U23XlyQm+m+GXYZct+xU2DewtkF+iKTIkip7LGUqQqe1Ezi7ItbvRDeey2iGVnjuF5id3YuQFB0jUqvA9q8OM2MdpYViVzcOvsUGiNpCynx5011KzFp0FK0IdlyCgEkJK0oaaThbzykNpKQVLT5ttqcUEpGp9wHU4eeQsh5h2j5kh5Zy3GS9Mk3dffeV3cSnwmykSJ0x2x3GGAoaJSt11xSGWW3HXEIMjVj7HWFZUVoN0lisVTlSZFXq7TcyU44EqBDDboVHhMjnWA3DaZLqSn5WuSpppTao1FaQBvJC18STqAbgiw0GnC549BwGomzrsybNMixmnZ9LZzdXtxBfq1eYRIYbcASVCn0lZcgxGwsEoccbkzN0lKpRQooCv+GgIS3yJ8NCUoQjAKEIQOVCUJPRKUDolIACR0AA10AAaAADoAB9WJ9h06n05sM0+BCgNDg1DisRWx5IYbQkeQGPdttIJ8NIRzYBKEhOcHIBKcZwcEDyPUY76/dNeHj/PjpWy2+Nxxpt1J0KXEJcTY6ahQUOduHO3PHymWbQriStNaotOqodSEOmfCjyytvmB5FqfbWVIyMlKjyEjqDg64pK2UpIKUX5G3O3ha/LX+a7OzDs3yHXKdOTVsnZbnkRJSw4/RoPfJX3LllofQyh5tYIulaHUrCgClQIuIVNJmMM8GjBg0YMGjBg0YMGjBg0YMGjBg0YMGjBjyAVEJSCVEgAAEkknAAA6kk9AB1J0YMTX8EfsfL63ugUzcriCm1ba/bSoRo9QoltQ2mm7+uyI+eduQ8iY24zalLeY5XY782LMqktLgKKdEaLclyqG13tQUTJj8nL+TGY+ZcyMOrjzZbi1ih0hxAUFoU6zZdUmNubra40ZxqOyrvA/NS8yqKu3uxvso5gzwzFzDnV6TljLMhpEiFEbbR8v1htZSW1oZeBRSoTrZU4iXKaekPJ7ssQVMPploshbM8M/D7w70aLR9pdsrWtdUZlLTlZagNTrnqKghKVP1S5qgJVbqD7nKkrXImqAACEJQ0hCE0NzbtEz1n2U7JzRmOoTm3SSmnpeMWlMI/YtR6XGLUFtKU2BWppTzlgt5110qWdDclbKMiZBisx8r5Zp8BxoDeqK2BKqz6/2TkiqSQ7OcUo3UEh5DDd9xhppoJQFtdn4GE8qEjoAB29O3QD68HtppNRUjQC/Q+Hhpb3DzxI6GCeXv4e77CfZjTv1FIz84qPrnOMnzJPTHoM+hxrvbik2uLfbp4XJ8/g9rUUngknpyH1fTp540sipk5HNy595z1/T9np27qDUThpf2X+i3suLi+FBqH1F7ckjj5k+fj10ONHJqiU83zvrye/p1/Qc4HXtrvah3tp16fHHmPDnhSZh6CybDhpYcOp+nT68aCTVu+F475yT2937D07emlJqHe1hfXoPi9/ZhSZhdE73iBppbieP2a45ybWm2UOOuvJbbQlS1uOLShCEpBKlrUtQSlKQPnKUeVIGVEAZ0qR4C3VobbbUtxaghDbaFLWtSiAlKEJClKUSQAgAlROgJOFRuGlCFOOKQ202krcWtSW20JTqVLWohKUhNyVKNgLknDYr14t9i7PkvQqluJR5k5hSkPQ7f+U3G604k4LTrlFZmxmnEkYKHH21p7KCSOk/ZU7Mu2fNTDUyn5DqsSE8lKmpVcMegtuIULhxpurPRJLrRFiFtsLSdCkqxDmZu0fsIyZIch1baFRpU5hRQ9DoAk5idaWi4U265RWJsZp0WKS27IQpKgQoDUYRqVx6bFrcKEVG6lpJ/rotp8M4BPX50pLuCPLws48s9NSaz2LNtQb31QsspVb/cjmFsuE/82Ipq/m9YniRyZSO3D2em3A38o5rUm9u+RlZ8s24X9aWl22mn4Inw4X2dJ4tNlLldRHiXxDgSHSEts12NNogUpX4qflNRjswgs9E4MrHx0hVjsv7ZcvNLkScly6hHaSpbjlDkwqypKUi5V6NBfdmlI47wjGw42xJ2Ue1d2d81SmYcbaNTKRKfUltprM0afl1tS1aBBm1OOxTUqJ4AzACqwBJthS1V4z2kyIb7L0V1IcaksPNvtOtkDC2nGlLaWhQOQpK8EdQcd40boEhh5TEmO8w+0stvR5DLjDzSwfWbcacSlxCxzQtIUDdJF72s7HlUlUZqZGlxZ0V5tLrMqG+1JivNq9ZLrUlla2Xm1JsUuNrUkixBtrhIr7uvaqVTqjbV91i2qrAqDK4tRoNQVHrAkNL/AB2ZNMZRMVjoCEvMghaUnGcamnJGxzajWHYtUyvlPMQ7taXI1UEZdMi30spmdOVFjuoIJSoNrcQtBKVhSScV12vdoLs5UWmVTKG0vO+Q5UOcwuLVcrSJLOYJTrRvduXSaUioTY7iSAtpS2mH2HUpdYW26hKxDLvnwc7ZyZEuu7C3wlhLjy3lWTc0StJitIUFLLVFuBUKQ+kIVyoYi1ZpaFpUou1VgNJS9cjLWzLbM6whrMOUGQ6lAAmRKxRCpwgamREE8BtR/bR1KSVGwYbSCrGH+2hvsyRJz9T2O7VXVxHHnFuZVzHl/NiDEbUbpTR6+ujOCW0kncTHq6GXkNo31VSW6otpZDXtkN0rdbcfm2hUZMdouFb1I8GsFLTSOdyS7GprsmdGiIT1VJlxY7KSCFLB6aUqpkTONFbU/UsuVViOgXXJRFXJitg3t3kqJ38du9jYLcSTyxXaLmOgzHe4jVeA48SUpZMhDbrhH/BtPFtxweKEHxwlakqQooWlSFpOFJUClST6FJAIPuI00yCCQRYjQg8QehwtAg6g3HUY9dGDBowYNGDCvcPn5e9kPzvba/rnRdLeWfxjy/8Av3Sv4/Hw1s8/iTnD8lswfmmXi8CnsPgPs1tbj5aVcT5n68edGPzCQ8QFNh1fYzeCm1BhuTDl7aXs1IYdSFtuI+92oK5VJPcZSD5EEAg5GmZtGiszcg5zivpC2nssVtK0kA8KfIUkgHTeSpKVJPJQBGoxKGxKfJpm2DZjOiOFuRHz3ldbawSPnViIhSTYi6FoUpC0nRSVFJBBIxR91jVj6cMGjBg0YMGjBg0YMGjBg0YMGjBg0YMZlO/shB/vyL/j0aMfiuB8j9WL2Fsf62re/wCA6T/2CPrbyh/2lo/710/+KM4+V/Nf405l/f8ArH5xk43mlTCBjwoBSVBWCkghQPbBGDnPTGO+dfhAIIIBBBBBFwQdCCDoQRoQeOPJClJWhSSUqSpKkqSSFBQIIII1BBsQRqDikDv5ToVI313ppNMjNQ6bS92dxqdT4cdAbYiwoV4VmNEjMoSAlDTDDTbTaEgBKEpAGBrE3NDDUXMuYozCA2xHrtWYZbHBDTNQkNtoHglCUpHlj6k8hy5E/I+TJ0twvS5uVMuy5TqrBTsiTSIbzzigLC63FqUbC1zhJtIWHXg0YMGjBg0YMGjBiXzgu34t666RTNuLpqEWmXdRGG4dIVNfS0zcFLioQiOYrrxSlVSjN/g5MHncecbaM6PlhT7MGO88My4sR2fHbcdjhG8+W0lZjkCxcUkahoi112sg33rJII1m7Ge36g1+iUnZZm2oxabmmjJTT8syJjoYZzHS0A+iQUPOEMiswEExW4xUhc+I1HUwl6UmRvS92dQw6tkhIKRyjtkd+/v6dcYxjHuBovn/ADIZDrjCXCUpJK7HXTlxt4m/lwxqNFaRToXeEWcWk7vXdtYm+vKwBvfjpbC61a67I2otKpXtf9w0u17ZokRcuoVOqSEMNpQgdGmEEl6XMfXysRIUVDsqXIcbjx2nHnUJVA7dNrucK1HoGXKdLq1Tmud2xFioKzqTvuPOEpajx2h670l9bbDKAVuLSBfER7Qc85fydRp+Ycz1aJRqRAQXHpct0ISVEWQxHbF3JUp5Vm2IsdDj7yyENIUo4qpcbXE9J4qN66lekWK7TbMoMY2xYVMfUVSG7ehyn3RVJyeVIaqNckOrqMmMjmTCQ5HgeLJVFVKf1J2LbMmdleSYlBW8iVWJjqqpmCY2Pwb1UkttpWxHNgTEgtNtxIylWLqWlSFIbW+pCcGNvW1qTtj2gz8yhpcWjRGxScuQnD+FYpEZ11bb8kXKRMnvOOzZKUkpZU8mMlbiGELU0DUt4hbEl3DfZqbZ29g1ORG8Cq3RisyVLbSl4wHs/cZJcGVOMO0ws1GMkq5GxUXVIShbz5WqQ2wlvfI9ZROvMAaDyvqfI3vY41M7H+SYtC2b/pscjIFWzhNlOmSoXfTSKZKegQowJ1baXJYmS91IHfB9pxZWlDIQ4LXZi2mPo22pZAAPXy9f/D3/AFa/CQBcmwx5JSSeHS/x93UY30CmrcUnKT9WB+n18+xPqOx4JUxLaSAQLXub3tbrb2WHX2HHW0zcjQ+0cTp79T9fQjHeU+jfMzynoAc98Y6/D45Pw8tNKfVQDbeA9YC97a3+zhYePs75EMmnTzun+wpdgL3/ALHc4E3udNOdsV8dLuPnjwaMGDRgwaMGDRgwaMGDRgwaMGDRgwaMGFi2S3Zj7K3gxf8AGsS173umihL9pC9USqjbdAqwDgRW5FuR3IrNdqETmQumN1KWqnw5CTLXBkykRXojdzRl85opL9FcqtTpMOb+DqDtHebiz5MQg95DbmqbdchtyNEPuxkokKZ32m3mgtSsOXKmYxlSsMVxuj0qsTYP4WnNVplyXToswEFqa7AQ6y3Ndj6rjtSVrjJe3HXGXihKRI3tnur7UTj8uCXAsrdS8aJbEF9CKpX6FMTtXYFuodwlEF6rWlAg1OrOoQCr7modr1TS0fEeaSytKzCFcy92fNitNadqOWaF6W8FKhRJUQZkzFPUkHfXHNWcmSGmbiyn1vRYDSylvvELWhBsJletdpnb7Vno9EzPmH0NgpTPnxJhytlemoUbIRKNGahRnn7G6IzTEypOthToacQhxxMx+yPs+1WEiFWN2uJXiI3ZusIaenoj7tbgWXbAlAIWfAj0C5mLgkNMuJKUKmV4h8FSno4SsMt1izZt4eq7j8fK+Rcm5dpxK0MuSqBSaxU1N3shxz0mGaYy4pNlFhMOShoko798ALxdXIXZaborcaXnLaJnvNFUAQ5IZhZlrVBpCHtCppr0ScKs+2glSBJM6It4AOejxyS0JC2HGKbCjQI7klUeGyiOyZk2ZUJSkNjCTInz35M2W6cfOflSH33FZU44pRJ1BTiXZch2S8G++fcU653LDEZrfWSpXdx4zTMdlGujTDTTaE+qhASABa+n0lmFGjxGEvKZjtIZaMiTJlvBDaQlPeSZjz8uQ5u/OdkPOvOH1lrUok410iqpGcK9e3f195+I6Ag511NQ/D7fq48OI1B46YWmoZ4EWtwFtPYB7/C2OflVbuefA9AcZH83v647HSi1DvYAX+NPbpxB6XF8KbMI6AJ94udD0628zoDjnJdZSnm+f19c5Pke/r69sjuPVTZhcNPo+4ceo5/SFZinHS4946k8Bbnf6sJVuPuBU7RtOuXDRrZq15VKmw3X4lu0RTAnznU5wAXnArwGSfFlGKzMnBhC/kkKW9ytKf2QsoU7NWaKNQatmOl5Sp9RloYlV6sd96HDaURvaNIKO/e/3ON6U7Dh9+pPpUyO1vOBHzxVank7KFbzHRspVjOtTpUNyRFy5Qwx6fPdCdLB1YV6OyPw0oRGZs70dLnokCU9uMqgg3e4iN1t3qg+i6q3LplHaccbZs+krkUyiw+VRBbmRAtL9SloI5VyKsuS8hQUlpMdB8IbQ7LthWzXZdBZXlqkR6hVXW0OPZqqiWKhWJm8kFK40ooLMCKoHebj0xEdlSVBThfX+EOHO1rtAbUdrFQfazPWJNKpDDjjTWUKSX6ZRoRSshTcuIFJkVGW2RuOP1Vch5CgUtJjo/BDM4TNipXE7xPbAcO0KoGkOb07t2Pt07VUhvnpVPuSuxIVWqbKXQWlv06kqnTWGlgpeeYbaIPPgyvVp4ptNn1FSe89DivSN039dTaCpKSRrZSt1JPIHEFLUEIUq2iUk2HgNBiXz2qHs3fZ8cFPGJXtgqJxY7z7WwolmWhf79u3XsAreaHS6Zd/3U+S0i277t3c606tWqmy1SVyFQrotOkRmETYja7unL+VGI0ssZir9ZpCJ66VCkqU88wHGp/ohUpnd3luMORnkoSSu123Vk7qiGki1/Sy664gLLYVqRcK3bkeBSQBryJ8hjbe0a9kzw+cNfs5+Bvja4a9xtydwKVv89Qmr4rW5tLpdsTaxE3NsWbuFYlVhWPSajW4llu0aHQKxRKlQ41x3MHnZcZ56uTlRjKeMt5oqNUzHW6JPjxmF08LUymMtTiUejSExnwX1IQp7fU42tC+7asAoBtNyB6HJyWS6HgAEHdQlHrFSrm6d6wvfjdVgAD1AxCbQJ9bo0B+k02u1uNTZZR8rgsVSaxCk8ucFyE08mMRk9i2Sey1KwNPFeWcvzqgxVZtCpEypxbiNUZVNhSJrG9YEMynWVvN3sLFLgI1AIucC9oedIFHlUCm5rzFSqBNIVMolNrdSh0yXuX3RKhx5TUd/wCcbpW2Uq/ZJ0Tbo4TGBzAd+pPfJPfPxz3PU+Z6dXvHbHqA8BYDyta3kNBpbgcRHPkW31HnvKUep8ep66XJueONxBmRJMmREjvJefhhv5UGxzJZW7z8ralj5hcwhRU2lRUj+Nyk404Kepl11xltQWtkJ7wC6gjeJskqsRv3SbgEkfsrE4Y2YGpsaLGmSWlssTi76KpwhK3ktbm86lsnfDYDiQlZSkLv6m8PWHeW/RF1aUhkoyykpLquUHlQD2HMkjnUeicj5uAo9urvitEbqQLqVoBbmdNeBtzsOWhF8Q1Xai2y0886vcZaSpa1E2skcLEkXJPDUDU4V+pcEW2O/FIMS4KH9x6qW1Cm3PQ224VbhvrBw4t1KC3UWCsqUuHUm5cUrcW+hpElQfSh5p2K5LzpGUZsFFNq26pSK1SkNxZKVKB/slpCUxp6N6xtJbLwA3Wn2QpW9D0PtH51yTUlyKfMTU6P3iWxQKmVSIryUGyfR39ZUBwp3gVxnA0SrvHo75Sm0OvFrwR7x8I1bhKvOkvVWwbilSm7O3CpsV80SrJZPiJp1SUUKTRbhRGUl16kyXVeKEPvU5+dFYdfRQ/aBs6rez6qGFUdyZBeW58n1eKlQizG0K0CkneMaSElKnYrilFN7tuPN2dVf3ZftVy9tSoiKlSg7AqDLbXypQ5qkemwHVp1KFJsmXDUsKSxMaSlLgTZ1th3eZSzjTAxJ2DRgwr3D5+XvZD8722v650XS3ln8Y8v/v3Sv4/Hw1s8/iTnD8lswfmmXi8CnsPgPs1tbj5aVcT5n68edGPzCYb2/kb3X/Nxe36uVLTUz3+JObvyarf5tk4kTZF+ups4/LjK356hYo3gFRwkFRPYAEk+fYde3XWMWPp8x7+C7/sTn/IV/NowYPBd/wBic/5Cv5tGDHz7d9GDBowY7O09uNw79eVHsaxLyvN9CeZbNqWxW7hdbRzcpW4ikwZakICuhWsBIIIJGDpRp9Hq1XdDFJpdRqbxNgzT4Mma6T0DcZp1ZPgBfCLWcyZdy5HVKzDXqLQoqE7y5NZqkGmMJSOKlPTX2GwnxKrYUyo8KXE3So3yufw/7xMxwMqcG3d1PeGAkqJeRHpbq2QEpUVF1KAkJVnHKrDhk7OtoENkyJeRs4RmAneLz+Wqy00lPVTi4SUJHiSOXUYZ8LbNsfqUhMSnbVdnE6UpW6iNEzvlqQ+tX7VDTVTWtZ04JSThD6lSqpRpTsGr02fSpzK1tvQ6lDkQZTLjaihxDseU2082tC0lC0rQFJUClQBBGmi6y8wtTT7TjLiTZTbqFNrSeikLAUD5jEiR5MeU0l+K+zJZWLoejuoeaWOqXG1KQoeIJxga9ePdjMp39kIP9+Rf8ejRj8VwPkfqxewtj/W1b3/AdJ/7BH1t5Q/7S0j966f/ABRnHyv5r/GnMn7/ANY/OMnGyemMsgkqBPpnp+/wGlErA+OHn0+zjrhJaiuukAJOvhc8/u440E2sKCHCg8qQlR69OwPw9cHJz26a5lyEggE3JNrDzt4/0cdTq56dluQ+QruzYC5JHHnz0HLTlyxSx4h1c2/++Su/NvDuYrPrm9a2dYuZuN815nPXMNaPvqUk4+mHZ8ju8hZIb/4PKGWkf9CjQk/ZhHtN7DvwaMGDRgwaMGDRgx92FSWXGpEYvtPMuJdZfYLiHGnW1BSHGnW8KQ4hYCkrQoKSoAggjQQCCCLg6EHgR0OP1KlJUFJJSpJCkqSSFJUDcEEagg6gjUHUYcvZXGJxQbe08Uq1917jZgpQW22qzT6HdbjDZSEhuNJuyj1uVFbbSkBluO80hkZDKUZOWNVdmez+uPGRVMo0OU+okreMFtlx0m1y6qOGi6Ta13Cs204E4mSi9ojbll6CimUnarnaPT2kBtmI9XJc5iO2NAiKieuUIqB+1j92m+trgHCWbibvbrbrTfl+5V+XXd7wcLjDVdqsuRT4aynlxTqVzopdNQBnDVPhxmgSohAKlErlDyvlvLTSmMvUGkURpYAcTS6fFhd7bgXlMNIU6rmVOKWonUknXDBzNnXOGdJImZtzRXsyyUklt2t1WbUi1fiGRKedSym2gQ0lCQNAABbCaaXcNjBowYmRtWMzBtqgwY6iqPApFOp8ZRKzmNT4jUKN+OSoAR47SUpP4qQEgAAALbAs02OFkj49uNwtlURuDsx2eRW20tpayTlcqSm1u9dosN59ZtoVOPuOOLUPnLUpRJJJPTNMKcIAGTkDH79z5417FKCRdRsMSClBJF+H146aDTFFJWUk4Cj0IGSBnv0AJ9ScDOB66R5k5KAbG1h9P83Pxt4Y72Y5J10sCT0SBzOh0sL9baW44jE3V4jtxajdtwU22bjkW/blNq0ynU1ujoYjyZbNOlvRkVF2pGOKmg1BLaX1Qm5LcRlBaaLTr6HpUjl3ErH4RKVkjXeAUOugNxz5ccY+7UO0ntMzZmiqOUTNVbytl2LPlx6LSqDOepCkQWn1tx3ajJp62JM+a82lLr6pDzrTTi1txW2WQEnTW7xV7524poNXkqqxm1BSolbpdKqAfwMJS/P+SNVgoT0PK3Um0qIAWFJyDwyaVAlJ3XY6eIIU2pTSgRwN21Jva3BVx1Bw38v9pLbflxazE2i1+e062tl6LmF5rMsZxlxJStsN15qoFjeSojvIqmHk39RxJAw3bSjiD8GjBg0YMbii29X7jltwLeodYr055xLTMKi0ybVJbrqgSltuNBYfeW4oAlKEoKiAcA416XpDEZBckPssNjUuPOIaQB1KlqSke046YsKZOdDMGJJmPGwDMVh2Q6SdAA2yhazc6Cwwt1O4SOKSrMCVA4d96HmD+ItW212seIcZw0iRSmlvEjBSGkrKsjlzkZaknaHkCE4WpmeMoRXQd0tyMyUZlYV0KXJiSD4EA4fkbZBtZms+kRNmG0KUwUBYeYyZmN1ooNyFJcRTVIUkgGxBIxyN0bDb42QyuTeWze6dqxW1+GuXcW3910eGF5ICRLqFJjx1EkEDldIJBxnGlKnZryvV/wC1OZKBU7mw+TqxTptz0/raS7r4ccN6q5MzhQSRXMqZkoxAuRVaFVKcQBxJEyKzYeOEpUlSFKQtKkLSSlSVApUlQOClSSAQQehBAIPQ6X+PDDb4cceujBg0YMGjBhc9gLc2Yrl9RZe/t8yLN20ovJOrUWjU2rVW67qKSos29bzFMgymoTktSP8Az2r1J+DEgRAvwXnJzsVlTYzdMzNEo74yhSmKpXpH4CF6bJZi06CpQN509bjqHXWWRcojxkOuvvbjau6aLjyHpkOn5NnZgjHPtck0TLEW0mo/J0KTOq9SQgi1OpbbLDjDMiQbJclTXWGIzHeOpLzyW2HLQPCXxf7Z7oiDtbwt7EXdStrLDjQ6fUbtq0ej2jZtDjfMCY7TiZlUq9cuOSyHJq4vyJ2ZLdJkVWdHTJTMXQLaPsrrVBTJzPtKz3TX8x1hbjsOmQWJdYqNSeFyUEuGmMU2nsEhrvkIciRUbjMZpay2wdWNie17Lub34eTNjWy2ss5RoCWI9Ur1Uk0/L9Ho0dRADie5TWpVZq0pIU96MtbU+Y4XJMt5prvpKZBpNW6H53X1PkevwyR54HXr8dQg1D1Fx9vjr9OhuRyxcJqFqBa/LQX6eYA8NbaY56VVgM5Xnv54HTywfL6vs0pNQyf2NuGv1XNvp5c8KjMFRtZNtRw1Nudz1ty8/HHNy62BnC/5uh8/f7yBkY66U2YN7aff/MRpz14+OFhincLp93iLceH169dMctLrncc/6fTI69e47/Z2zpVZg8PV+49R7eXjfjphYYp3/F4+FuPiRfloABfTHKTq8lAVzOAD0z36Yz5k9fj5kHSuxTyq1k3/AJvM8xx5nC3HpilW9S3sPmfd9nC+uOVk1t50kNZAP8dfQfQM5OOnXr16HyGl2NSSogFJ8rcgeens14eWFQRo0YbzqhcfsQQTfjbQ2GvtGh43w0Pf7h3pW50aXc1utRaVf7LIWHUJEeFcaWskQ6m2jDTc9SSW4tWKQ6V+GzNU5FS2qNbzYLttrmzt+NlzML8mqZIed3e6cJfmZeU6Qn0qmuLu6uAFALk0wr7oJ7x6GluQpaZFHe1R2XMq7XIczNmTYsOgbTIkfeD6N2NTs2ts3WINabb3WGqkpJU3DrYbD2+GY1SU9ES0uJHzs1uje/DZvntpvFarBp24Gye5NsX3R4dSbeYSiv2VXotVbptSaADyIsp2Eqn1BsDmXEffQAQrrpQsQK7Sldy+1Lp1WgnupMZxDzT0WWzdt9hxBKFhSFhbagbXscYlVKnTaZNm0qqRJECoQJD8GfCltLZkxJUdamX477LgStt1pxKkqSoAgjpY4tRcat0+yL9tvdO0vFZXOOqBwD7327t7RrG332s3X2+qtx1CvUGhz5dahNWjWGKnb9Bq1coLlbuSlUy4aE9cRrlMfooqls0iVShAfjeisZuya3LpLVDVX4bkhb8GVFkIaDa1pShReQUuOIbWENrU2sNhCwvcdWF7wRg45CQoLSlTQUClzeCQCdAD+yG9YG2hFidQb4a97Z32mHD3xIbacOHA3wTUysJ4UeFamUKLR7wrcKpUZ69avaNpL2+tCPRqLV40Stt0C2rWXPU5WrgjQ6pcVXrUiSqlwmac1Mqi9knK1QpcqpVutqR8rVZSyphtSViOh570h7fcQVILjju7+DbKkNIbAC1ElKW/KkB0k3uLqUpRFgSegOoA142+jEBtPY7EjqceWemfT3nJx5YyNSvFatY/fe/jbXxtY6+y7anP8dfP2DQcbezz9v2uOdIp1LS1BQ4uoVB1MOIlgKU6lS0qU442lIKitLY5UEdUuOIVkBOu6S45HjBLSVF99YaaCASsFQO8pNtbgCyeYURYWBxwUaJHqNSUucttFPp7RmzFPKSlohCgGm3FLO6EFXruAmykIWgg72N9ZlvKotODT3zp813x5ZCgrDigEoZSvrzBpOOdRJBdU4sHlKdOrL1LMGMA7q+8rvnzooJNvVbB4EISNbE3WVEXGuIw2jZqTXqkVRtIENv0SCmxQVthW84+U6bpeWbJTYFLKGkkApVh4e19qGS7DYDeVOqQ44SPfkDPcgDp1xjGPXUh0iPvEvKBsnRIN9T1ta3EajrexOt6i7TswFltNOZXYuArfKTqEC+hNzpzI143xLdsDtkKjKgoEf8ABoLYwEjqQR7uvQj4j3jSlWJ6afEUAQHFJueR4Hnx+nn1tivNIhOV+rhagTHZXuoGpBSFWKteJJHTgPG2JGd4uHjZvcjh/uvaDeO14NzWvfVEcgTYMj8FLprhbJg1ujzWyJNKrtLlBE2lVKI42/EkNJUCptTjaq+Zljx81tyqZUGw/CfBDgOikrsdx1ldiW3WlEKbWNQeIKSpKrTZLem5SlwapSHDFmxClTZAO4to2DjD6LgOsPp9V1s/OB3k7qwhSfzsOMPhjuThM30uvaitvGp0iNIcqlk3IhJDNyWfNkPikVBQCUpZqLTbaodYhjPyWpR30tqdirjSH6W5ry1NypWZFKmAqSmz0OQBZEuG4Vdy+nSwUQCh1P8A4t5DiNQAToPlLMsTNlEi1aLZC1p7qZGJuuJMbSnv2Fcym5C2l8HGVtrHzrBrmm3hy4V7h8/L3sh+d7bX9c6Lpbyz+MeX/wB+6V/H4+Gtnn8Sc4fktmD80y8XgU9h8B9mtrcfLSrifM/Xjzox+YTDe38je6/5uL2/Vypaame/xJzd+TVb/NsnEibIv11NnH5cZW/PULFVr2akGFUuNHZ6FUYcWfDe+/nxYsyO1KjO8m3d1uI8Rh9C2l8jiErTzJPKtKVDBAIzJ7PcaPL2xZMjy47EqO6/WA4xIabfZcCcvVdad9p1KkK3VpStO8k2UlKhYgEbwdsmbNp3Zr2mzafLlQJjEbLRZlw5DsWSyV5zy42stPsLQ63vtrW2vdWN5C1IVdKiDbD+8ayv7kLY/wCgaX/8rrU79LOXP3P0T+CoH8nxgL+nrO37sM0fw/Vf5Xg+8ayv7kLY/wCgaX/8ro/Szlz9z9E/gqB/J8H6es7fuwzR/D9V/leIq/ai8KG2lZ2OrO9FqWtR7Z3BsCVRXHpNu0uJTBdVEq1ahUSVSqrGgsstzZcVyptVCmS1NOTULirp7ayxMUlNWO09sqyycmP53otKgUesUOVARNNOiNRG6rBqE1qnluQxFQ207MZlTI7zUtaFPFlDsdSlJU0Gr/8AYQ7QWelbTIuyzNFfq+Zct5op9WcpgrVQfqD2X6pRqY/Vw9ElTnHn2KbIgU6ZGfgNuIjCU8xMQltaX+/Rfgo9lpSDSqNunxPU52TMmiJVbe2ndW/GYhxShuRHev1soYkOzXVEKdtYKTHjtJDNZVJcek02K0NjXZijzYcXM+0pl7u5KWZNOyshxyOssKAcQ/W3GlNvtl0FJRT2XG1pQf66cClKjokftM9u2ZS6lPyJsQkRlSIS5EKtZ8cZZmNIlNqU07Fyyy8HYrwYUlxLlXkNPNLcH9YNFKES3JtKRTbctOmR6La1DpFvUmG2lmLTKJTodMgx2kDlQhmLDZZZbSlIAHKgDAxnV24FNpdGiohUmnQKXDbSlLcWnxI8OOhKRZKUsR0Nt2HD5tudhrjLyqVLMWaZ7lTzFWavX6k+tS3Z1YqEupS3FrN1KW/LdedUVEa3Ub6X4DHpImleSo4TntkdfXuf37ZI7e1yQE8NTy56aez+boRhTp+XnXSklJSk+Fja/Am3MaW1sbeIwg28Ozm0e9FBlW/uTZlGuONIbKGpzsVEetU9fUok0qtRQ1U6fJbJJS7EktlSSpt3nYcdbWw835EylnmE7CzJQ4M3vB+DmhlLNTirsLOxak0ES2VAgFSQ6WnQAh9t1u6DPezXP2fNl1Qj1DJ+ZqrTu6I72mmS7Iok1u532J1IfU5BkIUFKAWpkSGFKLsV5l7dcTWw4yODyscNNwsVSiSplxbX3BKcZodckNJ+WUebyqdFBrq2QGVSS0hxyBOQhlue0y8Cy08wsLzl2x7HqlsvqbTzLj1SyvU3Fil1RTdnGHUpDiqbUty7bUtCSox3LoRUGG1yGUNrbkx42wWwPbzStslFdakssUfOVHaaNaoyHt9qSypSm0VekFzddegOrSkSWSHHaXIdbjSHXUPRJUtl9O/shB/vyL/j0ahbFglcD5H6sXiKHVibaoABCUiiUoZJ6ACCwP09Opz5HGtr6RISmi0kA3IpcAf9Va6/0aa8MfMfV8uPSs0ZiWEEIVXquSojjeoSL8eGh0/pxgTq203zEr5lde57e49c+oI6euPX3reWvTgL/HEnS/xawDipuWo8cJKkd4sWPC4vx48T4cOfHnw1VuT8G7+ExhCwB59EnqMdcgeePIA516gbEX6jX28ST/Th5RqUSjRKW0gWta3LgAOPh0PE4qGb9r8TfTelz+XuzuMv/lXhWT+3WN2bfxqzN+UFZ/OMnG+2SU7uTMop/a5YoCfdSog564SbTfw58GjBg0YMGjBg0YMTY8JFJpknYKyHpECI86s17mcdYbWtXLcVVAypSSTgAD4DX4OfgfsGNNezvRqPL2QZVkSqTS5Uhx2v94/Ip8R95e5mKqoRvuusrcVuoSlCd5R3UpCRYADDkvuFRv8A1XA/5qz/APBr9xNf6Xcv/wDqGi/wVA/k+MSo2PalehvU2rW5R6hDkoU09Gl0+LIZW2vAUlbTrS21pOAVIWhaFYTzpIGNetxYQLk2t8e0np7emPVLyNlGsxHoFSyvQJcWShbTrLtJgg7q0lJU24hhLsd5IUS3IYcafZVZbLjbiQoQZ8Q22sPajde5LRpanFUZr5FUqMHnPEeagVOI3JEVxZWta/kUoyYTTjq1PPsR2pDvz3VDX62sOICxwNx/0SUn6RjKnazk5jIO0PM2VIjinYVNlx3YKlqK3EwKnBi1WEy6tWrjseNNaYdcP+6ONqXYb1sSNUm26Eunw8UiAMMpSEojtoQlKSUpSlCAlCEpSAlKUpAAAAHlpaZab7pHqA3BNzqdVE8Tc+XQaDTGquzXIWRpezvIcqVkvKUqVJydlmRJkyMt0Z+Q++9RoTjrz7zsJbrrrri1LW4tSlrUokkk47WJCzyIQgJQAEpCRgAegHx7k9T5dSTr2uOJbHLQcOAAHl8fReaI0VmO0zGjtNMMMNNsR47DaGmGGWkBtplppsJbbaabSlDbaEpQ2hISkBIFuzptIKik8uR0JyO5HkP2enrpAnVEJCtR4a/X9dhblfTTCvHjKUQBztxHDxHste/v43UelUMr5QUZBwMEZyD3znp8c9COmcaYlUrKUBRKhcDn91+XEa+PLVxw6cVW9W4PUcRxtzvx8r9bWxsaZw/7UyV+LJ21sd9x1ZccW9atEdWtalFS1LK4JUtSlFRUtWVKJOfUxlXM8zoyVhqpSmrA6okOJtx4WUD4crWHM3wiQez3sdkK33dlGz51S1FS1LyhQlFSlG6lKJg3JJNyo3ubk3OuNLuvwQ7UbkWTV41Asqg2ndcOnTpNu1e2qZCoTpqgYK4zFTTTmI7dUiSHUNsLbnpk/Jm1rXELC8r1HNN24Vul5gp8d6a5UqbJnxo02PLPfKSy+8llbjD6gX2nW0r7xACy0op3VtqB0Y22rsbbGszZDzFOoeU6VkzNtModRn0Op5ZZFIjLmQIrspmLUaTFLdKlxJS2Qw+4qIiahLm+xLbUCDXT1dHGC+Nxb9v1u663S7btulza3Xq1MZp9KpVOYXJmzpkhQQ0wwy2CpSicqUo4Q22lbrikNoWseiTJjw470uW+zGix21OvyH3ENMstIG8tx1xZShCEgXKlEAdcd1NplRrNQh0qkQZdTqdQkNxIFPgR3Zc2ZJeUENR40ZhC3n3nFEJQ22hSlHgMTxcLfsmqBGhU66+I+Q9W6w8WZbW3dFnOxaNTkg86Y9erEJ1EurSFfMMiNT3YcJohcdT9QbUpeqmZ/wC0U+lx2BkZCGmUBSV12awlbrqtQFQYUhCkNtgWKXZrS1rJt6K2lO8vUzYr2AadGhsV/bhJclT3dx1nI9FqCmYkRuwUUV2twHA/JkrVdCotFktMMoTvCpyVOlEebTbzaqxbCp0elWVZttWrTmW0ttxKBRKdSmghIwAUw47IPTJJVnJJUepOah5mzZWay89Jq9XqFSfdJUpc2W/IAJJNm0OLUhlsE+o00lDbYslpCU2Ti+1FytlDJkJqnZTyzQstw2G0ttsUWmQ4G9upCd551hpL0l5QALsmS47IfWCt5xxZKiu9KpgwkBAAGOycDHw/09+vU41E1TqNgolXXn5/R489bXGuPGZL43Vc9L8+g668/PXS4USn0dl5sNvx2nULHKpDjSFoWggAhSVJKVJI6EEYIxnUaVWsLbcK2XFNuJN0rQooWk8ihQsUnncHqeow15z6XULbd3XG1ghbawFtqSeSkqulQ8CLcToACGzb++zd4VeJymuIvXbSl2/c+FqhX5YkeNal2RXlpUkLlS6aw1ErrCecqEG4IlUiNqKlsNsv4eDpyR2qdrey2QE0fM0itUcLSXcvZncerNLUhB+ZFVJd9PpQI3hu0uZEbWVFbzTpSg4rrtH2DbLdoLa3allyLSatuqDdcy62zSKjvqvdUpMZoQ6mq9rKqUSU6gJCWnGkqUDVc49fZk7zcEVR++SQXNwdkqrU/kNA3LpcJbJp0h9PiQ6Pe1MbW/8AcCqugOMxZYddo9WWyVRJLEp001jVXs+dqXIe3uIuDBWMvZ4gRWn6tlGfJaW+tFil6dQpBDXyxTG3BZ11tpuVDDjHp0ZgPsLezQ2u7Dcz7KJffvn5ZyxKfW1AzBEZWltKr3bi1VgFz5OmqQQUIU45HklLpiPvd06luNLVnMQlg0YMOI4XuHi5uJnduhbcUEPw6atQqd33ChpLjNt2vFdaTUKivxMNKlulxuFTI6iTInyGQpBYQ+ttnZ7zlT8iZbm1+cEuraAZp8HvQ05Uag6FejRG1ELUAopU4+4ltwsRm3nyhSWyDKGx/ZZXNsOeqVkyi77CJKlSqxVe4U+xRaLGKTOqUhIU2glAWhiIy48ymXPfiw0uoW+ki37tht/Y+yNg0Dbfb+ksUe2rchpixmkJQZMx4krlVKovpShUypVCQpcqbMdBW8+6tZIJxrNXMNZrGb6zMr1ckqlTprm8r5wYjtDRqLFaUpQYisIshplJsnVaip1bji97sgbOMu7PMs0zKmVqeiDSaayEJ9Vv0mZIVrIn1CQhtCpk+U5db77ibk7jbYbYbaab6KXXMZwrt6keWR6+XxwfXGuRmDwNunn/ADkW4/04kVinDT1dT4Ecbe0g/RfHKzK53wvrj1x6jHfoP0Y7Y6nSqzA4er9HO2tvO98LLFOJtZOnS3t4DTpxve+uOSm3AkFSQ4VE5+anr39wyOh9579B16LUemLVYhBt4g8unPXXlhbYplgCoBIHEq9UWB5k26aW62HDHOSKnJfJwoNpPvycH9AGfd0z1zjS/FoxNvVJ5ai3X6gettOWmOlTkKIk3IcUBfkE38+Y46D2XBtjWKXkkqUVEg5Kjk9fU9hgknIxk9PPTliUUeqSnpytoPrGgPsOEaZmBKAUoUlIsQAmwv4k31Oljc6dOGPkpweZ9fPzPfyx9WRjJ9xckWkAWsjUW5dPcNPPW3DjhoTa+o39ewuefUcOI8RfQgW88fFT2ff6nt088n3+fl26aX49LAt6vQCwvx18rHkfDTrhqy60okjfOpOhJtqePK9xfkOuvHDB+L/a2OuOzunRo6kym3I9Nu1tlI8J5lwoj0ysKSlPMHm3VJp810lQdbcgrVy+AtS7kdnPOshhTmQ6k6FRih6bl5bivXacSFOzqcFE2LS0AzIyBYoWiWBvB1CU5u9s3ZjEkIZ2r0WOUTEuRqZm9tlI7t9lZRHpdaWhIul9p0pp014kh1tyApW6WHFLZdb8UIZclqA5nVFtvp1DaMcx/wCOvI/4g1b1AsL9dPYPvP1YzTq75U4iOPmtgLX4rVew/wCamx/53u6qO0XFj0z+/wAfs10so3lXPD466fT1w3pLoQki/Ik/dx5/0a466CxgAgdgPeMdvq7d+uPqK9HaFwNRr9d7+dudsNSfIsFG/Um59w4/GtsdEywgqQooSpxIIQopTzI58c/KSMp5sDmAIBx105YcdJKCUgkfNJFyAeO6og2JtY2OttcR1V56wl5tLjgQ4R3iAohCyi5QVpBAUUAndJBtc7tuOOwt+H8pnxkEZTzjsM/NGM9x2z1IPXA89OdhvdSLAbyiAAeuvLTkNCOHA4i2pywVuK3rJQlSiSeAGl79dbg3GgPTEgeyFvJkPtPFsnK0IRnt3xjr3z7unkPM6fVPZDTLY4BKd9WnQE8Pq9l8Uzz/AFVcubPcCjdx0sN8rJ3raA9Nfd5Ynf4X7HjsRo82SykNMtCQ6VJH4jaeYjJHdWMeZGR0x2jTOlTUVqbSSTcpHmbcB93nyF3Vs9o6WorLik2KgCSRrbiOdut+GoFiMHEFuCWfliG3eVDYcQhIUAEIQCEgdfIYHqCO+mfT2LAEjU2KuPPz+m+nDwGJmYRwNtNOA4Dh0tYcfdirL7WG06dunt39+jMMP3TtnKcqEWa0Frkm3J7rLNehLCOYOREBEaqulaCuN9zvGQ4ywqaHo623ZWZqGVEV5pq06gPIUpxKbFynS3EMyG18yGn1R32yonuwHwkWeUROexiuu07MLlGWseg1lkpCCSAmoRUKdZcRe4BWwH2nANXD3BJ/BpvXH1T3FrMK9w+fl72Q/O9tr+udF0t5Z/GPL/790r+Px8NbPP4k5w/JbMH5pl4vAp7D4D7NbW4+WlXE+Z+vHnRj8wmG9v5G91/zcXt+rlS01M9/iTm78mq3+bZOJE2RfrqbOPy4yt+eoWKsnsx/7drZr/8APf8A3cXbrM/s5fr0ZJ/w9Z/0crGN1u2t/vYtqX+K5Y/02y1i3FrWPHzy49FLSgZUcfbr8KgPgfHTTjjyShSjZIxzddYpNZipg1WBFqUREyBPTGmMokMfLaXNYqNOkFpwKbU7DnxY8uOVA+G+y24MKQCE6fDhVNgRqhGYlxg/FldxIQlxrv4UlqZEdKFXSpUeVHZkNEiyXGkqsSkYcdClViizDPo0+XTpy4k+B6VCdWxIEOqwpFNqMdLqClaETKfKkRX90gqYecTcBWMSROWvJKiB6efnjPXp19+R6415OSEjhxPHh9Fr/VbCxT6A66RdG6OluPHS9tOJB4kHx0xoZNSbaz1BPp6+8ZySc+g69fnY1yLdUrmdePL6vtw94NDZZAuneV5XF+ZJ58AeXLQ45ebWycgKx6df3AI75JPTuQMa9R6nx+LYc0eDchKU8ALADhy8vdoRxxyM2sgcxUvPfsftJ+g+f1dvEnTQE8PpsefgemnPTC0xT0psVgHwt9FuPEdeHuwgO+VpULd7be69v682l2JXKY+1HfSAXqdU2gXqZU4qiRiTAnIZktgnw3vDLL6VxnHm1s7P+UYmeMo1rLkwC86E56G8UgmLU2Ul2nyk6Xs1KS2XEixcZU60FJDhxJ+zDN0zZ5nbL+aYO8E06c2mcwFFImUqQoM1KIs8AHoq1htSgoNvJZe3SppOKszlNlUa410iajw5lLraqdLRhQ5JMKcYz6cKSlQCXWlAcyUnA6gHprIl5h2LJdjPoKHo77jDyDxQ604W3EHxStJB8sbbMSGpcZmUwsOMSWG32Vi4C2nmw42oA6jeQoGx1F9cXGqfcXLb1ESXAnlo9NAGeuRCZHbPTqMHOPLPUa2Xo5/2opQv/wCTYNufCK19Xu6csYA1Wlk12tEpCE/LFTPCx1mvn7Qb8fPHMVO50o5sOZPl1GfPPft6eXbOTnBUdfZ7QfD6OvP6PcxCbbtupBOmp62v/T0B4ccJ7U7ieeQ5yrIHKvue4x6e7zzgDqc6/CtKTqefu5ezxtwwtxqe68UkJ3U31J004acevS3jfFYPe9XPvRu8rOebdC/1Z9c3XVjn9Oscc2G+acynrmCsn31GTjcLJ6dzKWV0/tcu0RPupkYfZhL9N/DiwaMGDRgwaMGDRgxODwg/2v1jfG4P1jquvwcVef2DGo/Zv/Wayl/hcw/6SVbDm0JJOcfAft/f46/FKCR4/Zidm0FRH0ff93v0443kKIVKBxnPr5+71PwHT46Q50tKEnW1vHj9lyNfLnhdhxSopsNbjl42+vn48ucJ3Ha34XEDVkHyty3j8Mx3zjp9v+nXZSHe+gMr6qfF+PzZDo+zGVPanb7rbpnRu1t1nK2nnk+gK+3D5bRQ1VKHSZ0ZQdizYUeXHdSOjseU2l+O8M4yh1h1t1tXZba0rTkKGnJ6QlphF9Du31NtCTb45a+BxqPsl7t/Zhs6dZWh1tWSMrALQQtG8iiwm3E7wuApDiVtuJOqFoUgjeSoBU6XRzlPzP2euc469gP9Gm7Pqe7f1vbe/na/XrfQE4lWPEKraWv4anxJ5cjY8efipVHoKlFBCB2Hl2+jsfgPLp0HTUf1auJbCiVgadR428ut+utueHRCpxNrJNtABr5a9By8dL+Ks0a3wAj5mT0z069PX18xj07dNQ1mTNiWkOXdsBfnYeHAjQ6crDjxF8Pql0YrKfV6AaEWty+w+N7cRhYKDbKnC2S2eXI6YPbzxgeuRnzz5jVd8yZvclKWhpwpburUE6jwvyPH6Dzw8koj01FzuKdCQdQkpSoA8epvwF9DxwscC3EtQnzyD5rCyeg6YT09fTOO/c59IxarJNYpQC771UgDje95bPG3j5+XHEf51qpXl7MQKuNDrA4nnT5F/IctBik2ASQACSSAABkknoAAOpJPQAd9bF4+YvFmH2cnBhB2esynbqX1SA5upelNZlsMVGKEyLKt6aht+LR2WZCfFiViY34ciuOLQzIYcUilraR8icXIpBtv2nu16oyMtUmQpFCpT6mpK2XDarT2FLQ64soIS5CjL9SM2Sttx5BlkrPo/c7WdjPs5QtmuVoe0vNkBD2f81U9uTTGZkZHeZSy/NaadjsMIeSpyNWqkyQ9UpSQzIjxHkUgJaT8o+ly60qm55PmjqRjpn7fU+p+OOoFWalUAlKje5105e3mfH3cL4udMl8deF7nqT0+PDQcFJpdNHzQE+YB+3v9PXyz8NR5U6lcqJVrqePs/mw15csWOtuPPr0/m+q11FpdPSAgEAdgMkJz7+v29eudRnWauElQ3xcg8Tw6211+jS1rDUNmVIUbkX58iQOg9nT+e6i02n/i4HUgdcDt7vQdu3xPcYi+q1PRVlX4jTW+vAG9rdTwNugNm7KkgXueZJv18fg/f39Np/LykpwB7vUfb8c/Dy1GlUqRUVAKuTcDW/8ASB9PO1zhvSZBJNidfE63/p1PIdTjGvuwbP3Ms24tvr8t+mXRZ910mZRa9QqvFamQZ8Cayph5txp1KglxAV4keQ2UPxn0NyI7jb7aHE82Wc01/J2YaVmrLVTlUivUWcxUKdUIjhQ6xIYWFpChqh5h0AtSYzyVx5TC3I8hpxlxbam5WaPTMw0udRqzDZqFMqUd2LMiSEBTbrLqSk2Oim3EXC2Xmyh1h1KHWVocQhYoE+0O4NazwTcRVf21L8iqWHXW13btdcD7akOVGzqjKeSxTZqypaXKzbUhDlDqryVJTMdis1RDMZqotRmvo+7OG22nbeNmVKzew2iJW4x+R810tKkkQa/EaaMlbIClKEGoIWifA3/XTHkJZcJdZcOMidr2ziXswzlNoDi1SKY+PlChTVAgyaU+44GUukgD0qIpCosq3qqdaLqAG3W7sV1PWIvxZl9mLs7B2o2Bj33NbT99m7rqLhmPrbCXYdtxlOxrcpbSvxiypgPVh5RCFmTVHGFBbcVhZo3t8zG5mHOAojJUadlloRgAbpeqchKHpz4GgHdIUxDSCN4LjvqCt10AbadhHZI1lLZQjO81pCq7tGfNSSooAch5cgOuxKNDKiCSZbqJlXUtJ3FszoKCgOR1KVIPNroAUS4B7yr3dPUevb6hnUNsQDcWSb+V/PXS3I8h9t8o9NJIsk+djzPM8eZvw+jHJy68pwkNFSzkjOcJHU469s/Dv6dNLkalKVb1bc+FzwN+XO2vwcLDcBpkBTqkoHQkXNtTYX8dCRpyvjRPS33+rjhAOcJTkD1GT1Jwe/XOfXTiiUY6ep0vpckcPC3Dlbpx0x4PVKJFFmgm403lgceVk66H2+7hiFYT27n39+p7nuT0xnPTtjz05YtFAtdN+HK+osb+0nTiR9GG1OzETvfhLgeQHu6cje1vr+SnR5n3dz0+jGfhjHQ+R0441KAsd2/stqNdL/Z1IvphoTK6o73r9NL+P3HhpY9NMY63j3zj7fQ4Hv8APA0ux6YBb1bnTy0+mxubH2Ww2JVYKr+tpwuTb33PAi2nLr1x1PAeeT6E5P24H1+R6aWWKeBb1emg8eFzrx14XGvLDdk1W9/Wv4k8tOHP3DxOoxjLf9+cdvf+jABHwPv6Y0rMQQOQ5fHX776DCFJqZN/Wvx0BsNDrwJJ6i50PLHO3LS4tyUCs2/NSlcStUyZTHwoBQCZbC2Ur65HO0paXG1dShxCVAgpGHJQ3nqPVKdVYxKHqdMYmNkFSSTHdS4U3FiUrSFIUn9klSkn1ScMzNEWLmShVnL80JciVmmzKa8lQSUhMplbSVgG4CmlqS6gixQtCVJO8kWiOYp70FaaSU+JLjOOQ1tsguKcksuKbeDSUcynAXkr5eQHIwR31pNHcD7DDzYO4+y08gEWO682lxAI0sd1WoIFje+MJqq2uPUaiw8U78SZKiuKBBRvRXlsKKVcCm7R3VcCmxwt9n7Eb0XWGl21tHuVXW3ccj9Mse5JcVQPUH5Y3TTFCTnOVPBPmDgHSzFZVpZJPS3M9NLedjxPDDGqtdpcXe9JqUFi194OS2EqBA0TulwHl09ut8OApXBHxUS2kOjZe5oSFAKBqsugUheDjHMzU6vFfQQDk87YKexHTGnHDjLNtANdblI4W1vfXlyve+hB0jOs56y20FD5WZWdSe6bkvC+osC2ypJHE6E66Xsddu7wYcScBAXJ22eQOX8Vu5LQdWO38RqvLOfQYzkHHQE6dcOIrSyQeB+cj3cdPv4DliKKxtCy164+Ulcxcwp489fRrcdNOF/PHzpewu7tuVALrW3txRW20kFxmMzUGwR0OF01+YkjB6HOOmM+YcLcZzeZsgkb4JIG8BqNTbQcD0HXEfVDOeX3YlQ7qsRO9U0sNNuLWytZssWSHkN3J00GuvmA/LYi35MV6nRpkR+I8XUc7Ephxh0K5sEKQ8lCgQPUD06adhPdxXlDiG7A38OHHh4i+ngMVRrLol1GKhK0rDkha1EKCxqoADQnqb9RwxPDtfFRRdup0tKQhZjNMJPn89PMrHbGUpA7fo1A+YXi/Ud0m/wCEUo+8fR4cPqxYfK0cMwGwAB+DbT7xfw0uD4a6HjiOHiOuNxH3Q/CHoXPPHr6E9e3l16dsZ12QUA7ot7/s8Bpfnz6DD6jpAF9RYX+jTmeYA93XEN26jUa56LdVKqaDIp9ZpVVplQZPXx6fOivxJscE5I+URXnmcgE/POBnGeyu01FWoFZpbgCk1CkVCJ61jZciK622vgAFNuKQ4k6FKkhQN0jDkoUxdMqtInoJCodRhSjYkFSWpDbjiDaxIWgKQoc0kg3HGrzJjuRJMiK8OV6M+7HdT35XGXFNrGfcpJGs1+GL48cKvw+fl72Q/O9tr+udF0t5Z/GPL/790r+Px8NbPP4k5w/JbMH5pl4vAp7D4D7NbW4+WlXE+Z+vHnRj8wmG9v5G91/zcXt+rlS01M9/iTm78mq3+bZOJE2RfrqbOPy4yt+eoWKsXsyVBPGxs2pXQD7+8/Ttzdo+06zP7OZttnyST/w9Z/0crGN1+2okq7Me1FI1Ji5Yt7M7ZaP2YtoyKi23kJOT1+n9/wBB761eLh1PAfHx7cfPoxBW4RcE3toL+H340UieteTnA+I9PXr5fHzxjXK4+lN9ddR4cxw4Wv8A0WOHTAoDrxSSggaHhr115Dhf29caKVUm2gVKWDgHJJwB3yck9h1J79z21xreWvTgLnTnqT8adMPeBQmWAklN1C3kON9Txvy5acekae+vtM9mNqbmqFm0iJXNwK7SJC4lZVbiYrVGp05vJcgqqk96OzOkx1YbkmnJlsx3VKjreEpiTHZrXnftM5MynU5VHp0GdmidCdUxLchPsQ6a3IQbOsonupfU+tpV0LWxFdZ30qSl5W6Ti7WzXsaZ+znRoVdq1RpuTKbUGUyYLNQjSJ1XdjOC7MhymsuRkxUPos42iVNakBtSVLYG8BjRbNe0b2k3kuWJZ0uDW7FuGrOfJ6M3cHyNymVWYUpKILFShSHmo8t4laIrU1EdMtaQwysyXY8d3zyJ2lcnZxqsWizYM3LFRnOhiEZ0hiXT35CzZqP6e0iOWnnjZDQfitNrdKWw4VrQlXdtA7H+d8h0mZX4FTp+b6VTmlSJyafFkQapHjI1dkfJrrkpL7DCSXHjHmOuoaSp0tBCFqS8GfXgArCx169PTr5j8b7CMHVirE8evO2un0cjx1OthpatzbITolNj4Dnp9/iQARjjJ1aWvmwvOPq8+/l+k9vIDX6SEi5/p+PjTHezFW4QEpJ+rTqrlwPDje1r2xzEmeVE8yifd5dsDqftz9PYD0OPhN7HQcz7fjy1vhwQ6MpZBULj2gAjTzUb29otriu5xH0JFu8Ru4MBvPJKu6LcABIOPvsjU66SlOAkBCTWSlCQMIQEo68uTkhtLiNwtoudorQCWms1VrukgWCW11F9xCQOiUrCR5Y2Q2ZyXZeznJD7yit5eVKIl5arlS3Wqcw04sk2N1rQVHz58cWM4ledVR6W2lWAmmwkkknriM0D5E9Po9c61ipC0oo9Kva5psHhx/sVrj7/AGYxpqtOderlYISQn5WqNlEHW0x61hxPEcBY+YsdPJqKlElSiT6n1GD28vTPr369+hyRbgbDzF/b92vlphUhUMJIUpOunzhc+wWtwvx1PUXw3LdXeIUJEuhW442/Ww2pMucUpcj0vKDzNtjJS9PAxlKgWopI8UOOgtIg3aLtVTRFO0bL7jb1X+ZKm2S6xTrjVtsElL04C1woFqOdHErcu2i5mw7s4/pmRFzVnNh6LltX4WnUsKWxNrm6RuvvqsHItJVruqQUSJyQQypqOUvuQV7suLd3U3MdcUVuO7gXm4tauqlrXcdSUpSj6qUST7zrN2quLeqlSdcWpxx2fMccWs3Wta5DilrUealKJKjzJJxexlhqKy1GYaQyxHbQwyy2kJbaaZSG22m0jRKG0JShKRoEgDlhP9cGPZg0YMGjBg0YMGjBicPg/Tnh+sb0Br+f+sdW143A3r9fsGNSuzYne2N5SH/tcw3/AM5Kv92v0YdbEilagSPh6D6PP/RjOkuZKCAdeHH7h4D6+NhqbFQ4xUoC2ptbkfO31DkL8zjs6dBzy4HU+7qP9P0Adh11H9ZqgQFnetbe4+J8ba6dPPD1pVN31IsDyNyPGwOvl94xBhx+t+DxG1tvGOW3LZ6fGG4f26d2TZAlZdgvg3C3J4uP+JUJSPo3bYyL7XrPo/aEz2za24xlDj/xskZbX/8AVjh9puJ+6ds6WxQJtIi3VQoaHEwGH5rlNqcMFRUyw3UjFqTbkFgqUER3oDjzbQajx5ceMy2yHI62XE7u+pPS1iPcR92PVso7T2fNlNHby5Eh0fMOX47rz0OBWUTA9TzIdU++3BmRJTC2mHX1uPll5uQ2h111baUKcUTNXY8NVcotJqyo4YNQhRpZaC/E5PGQFcgWUN84GeiihOfJI7CIq/XExXJDRc/3Jx1Gv/s1qTryB0ueV+vDG02R3Xcx5YyvmB6O3Gdr+XqHW3IzSlLaju1alxagtltarLWhlUktoUqylJSlSrKOi40a3/xAG+vTyz0z0x0+ce/19/LUGZnzghlLn4UA2IAvqbE8r31tf3g87S5SqKSElSbAC5PLlzsdPHz4aWWOg2wcBxTZJCebBGMlIz3H7+WNV5zDmd2UXFKdIQN4hOuvE66c+nXqcOFySxCQGmrbx9VSxYm1wPVvbThfrfiOGIoLt9qzcNgXveFoNbK0SoptS6rhttM5y9JcZUxNDq8ymCUqOm2XwwqQIvilkPOhor5A4vl51Wdi9lqg1imwJrub660qbBiS1JaiU8oQqRHbdUlG+hR3QVkJ3io2AuSbk4/5m/REc9Qa5WqW3s/yo41TqtUae065UayXHG4ct6Mhxe46lG+tDaVL3UpTvE7qUpsBjp9tDdaI7rCdg7c/CNLaClX3UDy86SnmITa6CrGe3Mkn+UOmOdnsdZaamRZv6dcxLXFksSUoVDpm6tTDyHkoJDYISpSADbUAm2trMeqdv7O1VhTYLuQ8rtImxJMRa259YK0JlMuMrUnedIKkhwqANwSLHQ4ZT7PjY9O+HEnacCpRkSbXshDl93M26nmbksUZ5hFIp2ChTa1Ta7IpynmHCnxabHqJQrnbSDO21/Nn6UcjVSY06WqhUN2kUxSSUrTKmpc7x1Kk6pMaG3KkJULfhGm0BSVLScQt2T9mSdqG2rLVNmx0SKBl4uZtzGl1KVsrp1GcZMaI42shLyKjWH6ZT3WrKuxJfcKFttLGLeNKpeAhKUAAAJCQOiQAAAAB0AGP2dNZn1GfupUb2tfThfz+72Dlje2ZLJ3vW8SSfi599vE8VJplNxyYT16YwMH4Afs76j6p1K+9dXXpb+jTlp9GGtLlX3iTprx6c+J48fqN9cM09oJxqU3gu2rhSaLEh1ndm+1Tadt/RZqFO0+GITbZqV0Vttpxla6ZSTIjoaiIebdqVQkR46FJYbmvxpB2E7IJG2bNMlE956JlGgdy/X5TCtyVKcfKzEpEFzdUlD8nulrkPqBEWK2tSQXnY4NU+0jt3b2P5aYFNbZmZwzAX2KBFfHeRYjbG56XV5zYUlS2Y3eobjMXSZUpxIUe5akWqlX7xlcVG5NxSbmuffzdMz33S61Fol6V62qLTxzcyWqVQrfnU2k05tHQZixG3HCkLfcddys6g5f2W7OcrU5FLoeSstw4obDbpNIhSZUsWAK582Uy9MnurAG+7MfecUAAVbqUgZI5i2obQ811Fyq17OeYp0tbhcbBqsxiNFJJIbgwo7rUSCygk7jMRlltJJITvFRMlPs9va+7wbP7iWtYXEhetW3K2Trs6LRajcd1Ov1m8rAMx9tiPcTVwuF2r1yjU9xzxKzTqqupzPueFu0t5t6M3Ek1X7SHY1yXtBy1V65s3odPyptChx1zIMakNsUuh5jcYCnHKZPpzaWqfFlzU7yI9UYRFWmWWlT3Ho5cKZt2PdpnN2VKrBpGdaxOzHlGQ6mPIfqbjtQq1GS4QhE2LOdLs2RGindU7AeW+n0cLTDS06EBV0aG9GkxY8qG81IiSmGpEaSw4h1mQw82lxl5p1sqbcadbUlba0KUhSVBSSQQdYKym5DMl9iW04xJYedYfYeQpt1h5pam3WXG1gKbcbWlSFoUApKklKgCLY0tQ8h9CHmnEOtOoS4042oLbcbWkKQtC03CkLSQpKgSFAgjS2MnXPjyxCp7dPh6hbscHkvdKDSUSrx2ErkK6ok9hgLqCLPrcqJQ7xghxKS59z0tvUuvTkZ8NtNARKXgMHN/f0O/aVJyntncyO/KKKLtGpciGuMsgNfL1EjyanSZIJHqvGKmqQUgKSl0zEoUlbiWd2rvawyexXdnQzI2wFVLKE1qSh5I/CfJdSeYg1Bg2+c33yoMk3BLfoylJKUqd3qTWt28Zk4s38KG/cPdul3NSLFpaabtXtcxbG39ly5SHk1SurpdIBnVWS07yKiMKjGmtw4TrRkhBXLlPB6V8jhUu2g5JVSJUObPdW/XcwzKtWKkhCkqjQ2nn2zHhNEXLqmS44HX9/dcVZDaEoQFL377JG1qBtFp2Y6Pl+E3D2e7L6VkzJWWH3WVoqtfmRaZLFVr80OBHobE8RoqodNDPfRWip2U85IkqaYdc44pwkuLKz/+Lt169E+Q7gg/+GmpEo17XTbyGttef0+w2OLbSq60wFJa3UADiAN73+PA8b9Da4+Kl4xj06Dz88eWMg59CPjpyxaQE2G77hbmNeHt8Pow0ZuYCb2Xe3G5108xx68QdNddfgp0du/fsfpHp29evb4guCPSwN31beYPTpbl18LebUmVsneG+SBxsfO+viOQ89OGMdbvqcdfI/afX1+jppcj00Cw3dR1vy8B9nMHxw2pdXOvr2vyB1PkONrE35X6Yx1Pd8fv69f2gfT30sMwNB6vv0sLaae7U8bWw35NV42PXXiePnYcT9vTGKt7vk56k+nY/Tn18/LSq1CAt6vQfZw4X6348jbCHIqRJvvcOpB8DbkOR0AuepxjF4rUlCApalkISlIJKlHolKUjJUVHoE9ySABpTZhElKQnVRCUgAkqUTZKQADckmyUi6rmwwiSailCHHFrShttCnHVuKShDbaElS3FrUQlCEoBUtSiEJSCoqsNFwsjh33KvdDcxVOTbdJcILc+vJcjvPIIB541MQlU91OCChx5qMw4DhD6j1E9ZI7Pefs3NtzFwEZdpS7FNRr3eRlvJOoVFpyUqnvixulxbMeOvQJkak4z528/okvZv2LSJFFYzDJ2oZvj76XcubO/R6rGhuJuO5quaHHW8uwXN8FDkaPNqNSYIJdpybAKdPa3CJZ1NYbcuCRVLnnAczhW4ql0wHoSGoUNZlYz3L9Re5x1KUgkatDlfswbP6Qy0vMcyp5nnpN3QXfkmlXH7FqJEUuaRe4KnqksKHFtJ0xkvtZ/RaO0nnWZLY2Y0XKmyPLyt5MRSYSc5Zt3dQHJVbrTTVDSsixDcLK7BaVoJLtgsSr7d8IvD/tBSobu2Wze31pzJMSNKkVqn2zTXrhmPSGEPOPyrjnMSq7JW6tanD4tQKQT81KRhOuhuUll1bCLIQwtTDbd9EIZUW20DUkhKEhIub6cTqccs2oViuxmJ1SqMua9NYZmPlx1QbefktJfedDTe4ykuOLUshDYSCo2SABjf1q2cBY5MDqAMEDp09AB6YOOnTS5FncLH6b68OuoHsI8cM2XTgq4KbE34AeOnt46c78hbCQ1u2QeceGemeuM+ZHp07fzYOl+NNOlj42vx53Fgfd7SDhqTaZe/qDieI4i30/HTCNXDbLSEuLdCG0gKJUspQkD1KlYxjI5iSB79OSLUdz1isBI4lRsLdVXPC/16c8MipUNL1wlslaiQkBO8ok30AFyeGlh7OWERrduR3UrWytl1HQhbbqFpKTnBCkqxg4IBzg9cdjl00+qJdA7taXBpqhSVDlpcE/TrrzBGI0reW1tb++yps8gtCk2vfgFAX0HDTXTxKQVm2loKlISpJBJC05BBBOCCMEEZ6EHOeoOdOmLNBFieNhqR7iOFrDUHx4XxGNToZSVKCCCDcFINx43FvDW+lvDTNpG7+7dlQXKTSbuqT1GWRzUmrFFXhAJyAlpM5Dz8dIGRiK+wMHzAOvCVQKDVVh2RBZD+tnmCY7tzfiWiEqP9+lXDHJCzTmrLxCIlRecjo4R5QTLaAB4JDwUtAtxShaDzBvbCT7l3jOvCkSn6hGaYqKG3FPGIFpjPAjJcQ24ta2SBjmbLjif4wUnPIGxUsrqpZ9IhrXIhpsFpWEl5jX5ylJSkON3sCsJSU3AUCDvmbsi7SouYVopdUbbhVVSSGFNFQizSkglDaVla2ZATqGlrcDoB7tQWA3iOi7ipuLUSvyZlDqcfOIWkAnHTKiAPs8tJUh1DEKW84oJQzFedcUbWShtla1qJJAslIJJuB1NuE2RG1OyYrSASt19hptPNS3HEoQkDXUkgcNbjjisRWJTc2rVSa1nwpdRnSm89/DkSXXUZ79eVYz79ZjHUk9ScX5GgA6AYU3h8/L3sh+d7bX9c6Lpayz+MeX/AN+6V/H4+Gvnn8Sc4fktmD80y8XgU9h8B9mtrcfLSrifM/Xjzox+YTDe38je6/5uL2/Vypaame/xJzd+TVb/ADbJxImyL9dTZx+XGVvz1CxVV9m0st8Zm0Sx3Avj3d9vbqH7dZl9nhW7tjyWo8nqz/o7V8b0dr+OZfZz2kxwLlyPlsAf3uccvL/+nFqaTUW2wSVZPn1GM/b1wMd/Ma1NW8pXAkD6/P3nGIMCgMshJUkKVpp7xx0OnDlf6McvNrfcBQx1GM/s+nzzg+Q16deeHRHgjRKEAC/ADh01tp56EHQ8sNV4r9yqrYWwe6NzUSYuHWYdszY9MmNOFt+FNqHJAYlsOIUFNyI7kkOsLQoKQ8lC0/OSNRltkrsnLuzLOFVhOLZlt0xMRh5slLjLlUmR6UHW1psULb9MK0rBCkqSFJUCLidtgOT4mZtrmRaTUWW34S6uufJjupCmn2qPClVnuXUHRxt1UBKFtq9RaVFK0qSSDVFJKiVKJUpRJUokkkk5JJPUknqSepOsnOPHG2nDhj2bccZcQ60tbTrS0uNuNqUhxtxCgpC0LSQpC0KAUlSSFJUAQQRr9BIIIJBBuCNCCOBB5EY/CAQQQCCLEHUEHiCOYOLLm0d7zb02q2/ueourdn1m06JOnLXgKdlvU9hUh1YCUAqcdJcPKkIJVlHzMHWt+zavv17Z/lGsS3C7Lm0OGqU8o3U7JYQYz7iiSolTjzC1qJJUSSSAcY57SMmx6FtHzlR4bKWYMOvTRDYQndbZivr9JYbSEgBKENPoSlCQAhICU8BjsX54/lDrkgDp3z38v36+mnW5I8b8tToOPX6OJ5EYTYVGCbepf2EAcORH18OFwbk6Z+bnPXIIJ8unu6/p+GTrgdkW56/BHHidPuBw64lJ4XT7LKAHst9Guug1viC7izVz8TN3q/lf6nZ+uwLOPu1lrtVO9tLzsrrmWpn/AKyrGnOzJstbO8nNkWKMvU5NvKOnE58Cd/UqnDJ6QYg6kf7A326/bnt5a1Cpci1IpevCnQeB5+jNeWntA4Wxl9NpA+V6oe7GtRmknUnWU75kaXGnlwthIN1txl27C+5FKeArlQbJLqfnGnQ1fNVIx2TJf+cmLkktkKkFPzW+eL9p2flZfhik0t4Csz2yS6PWVT4ijuqftwTIfAUiMDcost8j1WwuyWwXY2zm6o/phr8UqyzSXwlDCyUprNRRZaYtrXXCjHccnHRL283ESSHHi202PEcmqdKipQKVqWpRKlLUQSSSSVKJJyonJOepydVInTBHbW64olxV1byiSoqJuVEm5JJ5nUk3xpLQKMuoyWWm2wlhsoRupSAhKU2SlCUpACUpAACQAAAAABiN3dtPJutuaj+RuFeif+TclSH7NQVLVvy5Sv20h5X/AEnFH7cM6a33MyW0NO6lSG7dNx1afswnuufHLg0YMGjBg0YMGjBidDg1YLnD/YpwT1r/AMMi5Kt7v2/RrglvBveF7Gw18LfGtxbXhjVjsxM95sayker2Ybaa6Zlq3D7jz14Xs8mnws8vTPX6/L06j08j5jAOmPVqkEBR3rCxt7L9PZwxaWmQFLUm6eJ42vYePhfja3hqcd9TYGOU8pBOPLOfXyPTr38+4B6ahDNmZER23T3gFgb62Ol+GvHh/RiWMv0QuKQAgm5Gtvbyvr7+XmICvaHtKZ4mbgbUCCLbtVWDnOF03nB69RkHPXU07Hpvyhs9oky9++frdj4N16ptD6ED7cYsdt1lEftNbRGUEFLcfJIuOFzkDKxVb/nE4Y7qTcVRxad2hoJfsSzFhvJct6krJxnouIyrv1Hn65+AGqM7QM3pi1atR0Oes1Uqi0E31CkS3kajlbd+yx4Y+mbY3SkDZZs0kOgBJ2eZIWSdL72WKWqwGt735dePRzVvWsByLUgeRJwM+oxkD457+QwB1rnW8wuPqUtx0niQL2A9h8+nPyxIk6pNsILbPqpAtpa6vFXA6a2HUXvfXCy0q3glogNdChXTHY4OM9PPHoMZ1GFVrClpcsq3qquT5W08+WgPnhkTaoS4DvH5wOp4j4uNdPZilxvsnk3v3kR/J3V3DT/yburA/ZrabLJvlzL560SlH3wI5x812bzvZszQrrmKtn31KScJVpbw3cT9+xNsdD8He6/HWEqccqFq2lBf5SXEiLGqFYqbQURgJPy6lK6HPckEAap72r6wYzGUaXvEJdNWqC030JZEKM0SOHB94BRBsN4DiQdOP0OilNoXtTzEpI71trLVEZWQLhuSuqz5KQeIBVEilQGhITvcBawtS6Z+LhOD0+A7dc/HzPn17kYoRU6lcqurTXS/x8aaC99Ipkrjc8OXTz6kdB9FsKPSqZjlGMYHU46/+Hfp8cdc4MbVirABdlXvcAXt4Xt8E2PDk15cq97nnprxP1H45AXhu9oH7MTiN4zt849/W5f23Fv2Rb1pUi1bToldk3D90GUNLk1StVCYzCpMiI1Km1eoSI+WXnS5T6fTi4oOJU23abYP2q9lexjICMu1WhZqmV+bValWK9Np0amORJEl90RoKWFyKmw8Go9IiU9pSFNoSJIkuJTZ0qVQLtBdn7aNtbz+9mOHXMtRqHDpdPpFDhzpFQRKjxWG1SZqn0MU95ouP1aXPeStLq1GOqMhR/BhKWWR/wCD28UUjHJuvs0nP8p+7Bj44oBP7+/UqP8A6I/sZj/Py3nz2QaOfd/txqfDj78QI52Ntord717KRt0lVT7aYMZ3/k8HFJ/7XNlf/wBe7/8AN7XJ/slexX9zOf8A+D6P/rjHp/UebRP/AF3lX/Kal/q7FqPhvse9dsthdo9utxatTK7eti2Fblp3BWaO5KdptUm0CnM0wT4q5jEaSUSWIzTqg8whSVqUnBACjkBtdzHlvN+03O+asoxJsHL2Y8wz61TodQaYYlxU1Jz0uQw41GdeYQluW6+lkIdX+BDZUd8qAvxkSk1ag5Oy3RK6/GlVakUmJTZkmIt1yO+qEj0dl1C30NuqK47bKnCtCT3pXYbtiVr1HOHZhK99LMg7i7K7t2DU2G5MC89tb3tiWw6ApDjFbtupU5aTkHBxIylQHMhQCk9QNPnZhmB/Km0jIOZoxV31BzllqrISlRR3gg1iHIWyopIPdvNtqacSfVW2tSFApUQW3nKlN1zKOaKM8AW6rl+sQDcBW6ZVPkMocAOm+0taXEHilaUqBBAOPzSpUZ2HKkxHxyvRX3ozyf5LrDimnB9C0ka+p5KgpKVJN0qAUD1BFwfaDjEwgpJB0IJBHiDY4ne9lqhDew95yM/Pd3crjKhnpyR7OsJxGB6kyHM4HXAyTgag3alEMqtUwkEpbppsNSAVSnr6cNQlIPXTGtX6HxNRB2Z56UCErfzwhK1CwJQzQKcWwo8SEl5zdBuAVqsPWOJKFPd/LyznA+r0+PrpjR6YBa6NfAa/Vx93HqcXel1lSir1uR1v4crddPPw4HHU8OvXPr6fUPo8hpbYp1rWTpx0Fz469OPtOmG5JqxJPr35aGwvw48NR08dOuOt7yzj9Pr5D7Tnt3640rMwAmxCRwGtr8OHsPUa68cIEiqE3G97B0Fxx4/V58sYq3++SPTv19/TsO2T5dDpUZhaiyfbr9t9ffxtbXCJIqPEFQ1Otjfj/P1J1530xiqfz2Pwz5/zdvTPTPbrpSahjmBx5Ai33X15j2YSHp5N/W+kHl9HLqOvQKZtntDee608sUGIY9KZWUz7gnodRS4ZSfnNIWlJXNl9fmw4wWsdFPrYay8JQ2fbKszbQZndUeKmPTWV7s6tzAtumwwACpHeBJVKlWI3IkcOOm4LhZau6mqnaS7Xeybsz0T0vOtWXUs0zWe8oGQaE5Hk5prJVcIkKjrcS1SKSlSbP1ipKZjWBbhonS92IuS/arhssvb9EWW1C+7dxttguXBVWm3ZCHlJHiKpsXlVGpjXUhBaDksN/Mdlu5JN7ch7Hsk7PkR5bMRNbzA0kFddqbTbjjT+7+EVTYh3mKegEkNrQXJQSbLlr1x89HaK7bm3ftJvVKkVGsOZF2bS3lJj7PcqSpMWJKgoXvMNZoqyVNVDM753UrkNyDGoynvWj0eOEjDlIdtgBOGx6Doev6Prz59umpPdnm5urieN78uPt+8aHhUuLQG0gANAWI5D2aDgfZc+GN8zbqSnq3nIIIwfPuPIfH6D5641ziL6kHqeB4anpfyGFlui2AG6ADfgkeIt9XgLeWJD7PpwrVhWtP5edw0WHFf6ZJk09oQJHN36+JHUT8eo1ANZdMOuVNg+qPTHnUcQO7fV36NL6+ouw4HyxebJ5FSyhl6Ve7nyVFjvHj+GhtiI94i7jCidDoRx583W7ZBC8tnz8iD27e8fHtjA666I069vWHn8ceugvw4465UEKvdPU8PpGmvs6aWxF37R/jA294Bdg6tu3eMZNcuSqTfvZ2ysZt8xpl53lKZdkMw1PpbeMGjUqIxIq9fqbjfJGgRTGY8WpzqbEl+FezexlqlrmuAPSFnuoUYlX9cSCCRvqGqWW0guOruCEgICu8WgY9NHyk9mCoohpJajpBdlSd0HuWAbEpCrguuKslpOvrErUChC7fn+cSXHTxQ8Vtfn1fd3dS4ZlJlyXJESw7fnzrf29oyFqy3Hptqw5hhuBhP4NqbVl1SrLbGJFRfUVKNbq5mqvZieLtUqL7yAoqbioWpqGxfkzGSru0m1gXFBTqgBvuKIvifqLlih0BoN0ynsMrKQlyUtCXJj1ubslSS4oX1CAUtpJO4hNzhuVv39fdpviVa163bbUkHIk2/cdYoz4OCMh6nTIzgOCRkKzgkdtIrUmSwpK2ZD7K0m6VtOuNqSeN0qQoEG+twcLDsaM+gtvx2HkKFlIdabcQoa6FK0kEanQjmcOPtLj34xLM8FNL4g9xqg0yrKWLprS7yQpP8Asal3UiryA0c9UIeQM9Rg9dPCm7Sc+0ndELNtcShAASzInPTo6QOSY84yGUjwSgeGGPVtlezmthfyjkzL61uElx+NT2YElajxUqTAEZ9Sud1OG543w7CwPbEcRdCfYY3EtuxNyqUlJEhQgybRuJ05HKW6tS3ZdIbCRzBQctl5xw8v4ZHKSqTqD2lM/UtbYqTVIrscWDiZMVUKUUi1+6kwXGmkLI033okhI47hxDmZuyXs0rbTppTtay5LUFFpcWWifCCzexeh1Bt51xsHUoYmxVH/AIQHXD7dtvas8ON/vs02+Kdcm1VRlcrXymsxk1y2lOOKSgNOVWjpdkxkKUolUifS4kJtoKckSmccpnnLfafyTVVoi5gg1HLinh3a33ECq05G8LEOOw2kzNxV92/ycpI4r3EgnFYs29jfaJQ9+oZRqdLzOYy0usssOfIlXUptQUhbLM51UHvWyAoA1VC7p/B76yE45niu3Htu1dmr3vi0a/Tq3S63SZMOy63RpzUqJLmV0qp9NnQJbHitOrpMmQmYsdQXIK2V8iiSPTtQzPTIOQa/UaNPizodWYdpNLmRXkPMSE1Mriu904g2U4zG9L3gPXbdYWFpSpCwmZdkdIrFUr2W42YKbMpdWpoZqFbgTWFx5EaTTAlwl5pSRutyJYjuNKI3HWX2ihSkrSo1zNUHxdrCvcPn5e9kPzvba/rnRdLeWfxjy/8Av3Sv4/Hw1s8/iTnD8lswfmmXi8CnsPgPs1tbj5aVcT5n68edGPzCYb2/kb3X/Nxe36uVLTUz3+JObvyarf5tk4kTZF+ups4/LjK356hYqiezwe+T8Xe1T2cciL0P12Fcyf05xrMjs9/rw5M5fhqx/o9V8b99qdHebBdoCLE70eg6DjpmqhH7MWZZ1YIySruOxPzvPuPo+s+mtSd4XsNfEcOQ8D999MY3sU7gV6DQ252424fVa3PkMcdProTzYV3Pr6+7P6OnkMaOP845XP2dfaOOFZmOhAshNracOPO3gSfPx5XZVxtVYyuG/cxnmJ5qfTzgHp0rNPV+w+/1A1CvaJ02O5wPhQv9JKONfLTj0tx4WK7L7JG2zJq7fMTXzfXictVdHlrvcL/biuVrLrGuODRgxYT2Bl8mym2CM/iWHZ4+A+9qkk56+fN28vPyzpzsVe3dlGSwTwp0v3fK9R9/h7+N8ZtbYKcHtqecXd2+9UYpH8FU8dNNPb04G6nvTM5657/6T6+/OARg58tSM5IJ5kefH2Acfd0Jw0otMAAunhbl9H0cfs1xqXpgH8bJ9Aeg+J9fT3eZ1yKcJOnvNvq4DC2zFQ3YFOljp8X58bA28NRiFHipVz8SN0r/AJSNuT//AF/Z2sytqRvtHzmeuY6if+sKxoJs9Fsi5VFrWoUAW6fgE6Ymbk1+PRbaRVJbgQxCpbDpGQFOrSwhLTLfqt90oZbwMcygTgZ1o05Wo1HywzU5S91iHSIrpANi4sRmUtMo4kuPOlDaAP2Sxe2uKK0nKcrMebU0SA1vyalWH2Ek6pZbVIcU/Jc6NRmErkOHkhtQTc2GGbVCfOuKrSqjMWp2VNeU64f4raeiW2kDqEtsthLTSfJCEjrjJp3W6zJqk6ZV6g6XH5TqnVknRI4Nstgn1WmWwlptI+ahI46nGmOU8rxKRTqZl2jMBqHAZRHbCU+ssg7z0h0geu/JdUt95Z+c64o6CwHZ0ulBDKgBjDas9/5Kj54GM9TnpnpqJK/WblfraWIAuNNLaa+Hq6X+vFstn+Td3uPwVySi53b3JI8/Zc66HEUe8Qxu7uoB5bj3wPqueqDTHUreUpX7ZRV7zfFT6+33VdrTVrd3Vqi3bpuTHk29lsJxrxwk4NGDBowYNGDBowYnu4JYhc4eLDXjPMq4vpxc1XAH85PvGmbX5oYedRvAEJRxPVCT9v1G+mmu3ZSiF7Yrk9QF7vZj/wBKKyOtr+rpz52w96mQPxcp/Qfd08+/w6fTqFMzZgSw27dwCwPMC1vt11GLmUGjFakDc5jlpbz/AKORwq1r285UH2+ZBDSSMnHRRBGPoHQY9e3TGqp51zX3y3UhwhtJUD62h8BrqLcb+I44lZIZo0O2gfWnwugEa8uKgR4gcibkV5PafQxA4ubuiBPKGbYsj5uMY57fjOdv+PnV2ezjJMvY7lSQTffezJ7k5qraB/8ALjAntnvmT2kdobxN99vJ+v8Ae5Gy0n28OOI+NThiruLiuxduJVtpt+8W+r1o2+5kjqrxKZGX6Z658u3Qde+sl9olZKM3ZsSXL7mZq+3qRoEVaWkAcTpYWHQ4+l7ZxVdzZRsvbBsEbN8iJsNBdOVaQNBpzF/Prxw6Ci0ADkHh4Ix5dAcDOD5n3+X0E6hip1cr3rrsLczqeXsF9OP8/un1I3X6xN/H4+/w42VCm0QJaPzT+IodvcT3+HkfifcwanVbpcF+RsAfA8bfRa3PXDQlTyVjXUEa30FiPZz+m+upxRU3+HLvtvWn+Tu3uOPqvGsj9mt7sqG+VstHrQKMffTo2PnpzWb5ozIetfrB99Rk4SXS/hAxZi9h0GJWyu7kVBQZcfdVD7yQR4iWJdpW83GUsAcwbU5ElhvJ5VKQ5yjKVZoL2yXVx61ktw6Nu0erITx+ezNiqcFv715u2uuo00I1N/Q+pbaMobSo4P4VvMVCfWNB+Depk5DR6/OjveVtOOJ76XTM8gx6ZOPIef7/AF6z/q1VCAs72pvbz6dPotflbF6Zcq97nTmBz1PL4v77KLTafgIPLj09enbGR9XTt179NRbVqmSVkrNzfifr8Pdc66ADDckybk6293Pl56/A4qDTKcDy/N74GMfH3fp7dO+oyqtU+d63U35Dx8Tfhw5cALYb8qQdRe3t4/cOpPsvc47JhhLKAAOuBnTHkPqfXc/NBNh9vmfjkAjKUVHX4+Pox99ejHjg0YMGjBjAqyUqpdSSoApVT5iVA9ikx3AQfcQSDpRpBIq1LI4iowiPMSWrY5pv9hy+f9bP/wDZLx+ZNfCUovS70IACE3RcCUAdglNWlhIHuAAA92vq7p5JgQidSYcYk9SWUXxhxLFpUkDgJDwH/wARWJl/ZbVlpe1m4lCSr8NAv8VZaD5NVi3aLDaWBy4GV0N4KIUonCcpGAVMHPEMPToDtibxnWyeXqO71uH/ALQn2accaRdhmshnKWfKZvAKZzDTZ4TexAmU1ccqtrcEwUjgACNSb2EnS3/PP7/Hy6+nrprMwQLaeNgPjQjz5YunIqZN/W48LnT3e64N+vTGKt/Hnn1x8P098j45z20ptQrcreJGo+NedraYRX6je/rX6a6WtqeGmo00GvLGKp5Rzg4A+A/0fSfPy0otxBpceXEe7xv0B8MJT00m/rXv49dOWvu169cY6nRnoeY+ePs93n5Y69xruRG8LedxppwHE26e4YTnJRNwT15+Xlrrx879MOp2C4b6nuU9Gua6USaZZaHQqOwkrYqFyBGSUxVlBVGpXOAh6cD4kgc7UEj58pixOyLYhIziWcw5jD0DKjTiVMtjeZmV0ouVIikp/A08KSEPzNFu3U1EusLfYzJ7a/b+pOwxuds02WrgZk2wyYy250xwNzqDs7S8AlD1WbQ6lM/MpaWXoFCuWYig3KrZS0WafOlhtSy6XQadCpNHp0Wm0yAyliHChx0MR2GkEnlQhAAKlqKluLOXHXVKddWtxa1G8ENin0WnxaVSIcenU2E0GYsKI2llllF7ndQiw3lqJcccVdx11SnHFKWtRPz8VuqZlzzmKq5vzlW6nmbM9dlKm1eu1mW7NqE2QoJSC4+8pSktMtoRHjR2wiPEjIajRWmo7SGkKjT6EMJ+ZnOMAD6x+k9z5dOmuR6Zx1t4D2+Omv1m4thSh0oC3q3PXl7up4eY56nHYQ6ASB+DwBjOQTnH7f0+Rxg6THZmp1t0HD2a8NPf78OKPS7aBP2dB9OnEjr446Fm3QR1R09SD6n1x9Xw6jXEudroSb8x8dOf3YVGqWeSbcgLe/Uj4N8O22BeEmlVSz5AHjQXF1amFRHMuLI5G5zCUnPSPIDchIGTiU8SMJJ1F2eGgiTHqrV915KYsi3AON3UyskDitG83rp+DSOYxYLZLMvCm5fftvRlqqEI3uSy9uolNAaf7k6EPAAX/DrOoTfCk1u2Px8tDz/i9fLPqc9ME/X0014061tb6389dOVvZ4jjiTpMK9/Vv42+/n9J4XNjehf/AAre56+zxHcMu2bi3m7Xo2zVdvmIyOZMeRXrovaqUCoOqH4jj0Sn2fTkIOOZpExwdA71j3aHMcfn05gquy1BLyRw/CvSHULJ6ncYaA42F9dTd5ZIiIYiTnrDvXZYbUdL920y2tA/6bzh8RboMVR9R7h7YNGDBowYNGDBowYcbuluwiq7RbI7K0GoqmUHb6gTq5cT7aVIZqN6XZW65cjkYlWPGRaUC4F0Fp5IShUpdTCVPNJjuB0VTML0vL2XcttuL9CoyZsp1NyG3qhUZj8hSwnQqEWM43HQVD1XVSy2e7d3lN2m0NqLXK9XnEI9Lq64bDarAuNQYERlgIKtQDIkodfWE/OaEUOeu3uoblpr4cWFe4fPy97Ifne21/XOi6W8s/jHl/8Afulfx+PhrZ5/EnOH5LZg/NMvF4FPYfAfZra3Hy0q4nzP1486MfmEw3t/I3uv+bi9v1cqWmpnv8Sc3fk1W/zbJxImyL9dTZx+XGVvz1CxUu4DX/k3FNtq/nHht3ec9sc1k3EnOf8AjazI7Pn68GTf8NWP9HqvbH0DdpkX2H57HVih/wCk9FxYYnV1SiQF58sA5PXv1x8M/AYGNak6JHTlf499hzxkQ1HUogBJJ8Bfx8QPr9+OXk1FaySpZ7Z6E+ePP6D07Zx669S3kp4WPx7LfX4YXIlIW4RvA620A8r3OvuHjhp/GFMC+HzcJsHouDDHL37VGIoHPc/ijv8AHUG9oV4L2R5uTe+lD8vxko/xytyGuLIdnGmCNtbym7uW3flsXAsBfLtWGp87fViv/rMfGoODRgxPhsZKDezu2ySTlNjWcMZwOtr0cg/p+3r2GtJ9jayNl+TgOVOla/8A8rUOH1ey3LFCdqMdB2iZqWbXVPjki2ptTII6eV+JHG2FHemE9Mge4HAyP09O3r8dSTf4OuGSlAGg4cvL6b9Re/8AzTjXLeUrPXp5fD4Y8/3HofHnjzAH3+PvufLpiHPik/tjLl/3nbf/ALv7O1mdtSsNo+dOgzHUvcJCsX22fa5Fyt+8UH/sE4ko3Kra3aZQqCwoqL0dmdKAPdDaA1FQQOmFLLzigexbbOrabT653VFy7Qm12DtPiVGaQbWbbjttRGyLm4UsPOKFvnNtkYjTYFlZUit5mzS63vFqY/RqYN257150v1B4E63Q16Kykg23XnhpqMchRKVy8iiMqOCSR+nzI9E+/wCOqu16rhIUlBskAjQ21ANwT9JPWwHA40UyFlBSlNOuN3WoglRB4mx6aW+7ywpEWGEMqSE9fCV0HckpIwBjOSfpzjGe+oZrNUKiuyyePO3Xhp1uLW9tycXVyLlMJLBLY0KOItYXGp5DSx8r4iw3Y2n3Jqe6W5VSp1lXBNp9Rv8AvGdAlxoDjrEuHLuKpSIslhxOQ4y+w4h1pYyFoWlQODroRPhd22VS4ySUJJ3n2kkXSCbgr0PUdb9MZ15n2Z7RJGZ8yORch5xkMrr1XU08xlqsOtOtqqMktuNuIhqQtC02UhaSUqSQQSCDjhG9k923Thvby6nD6IpT6j9QSTrxVVKYjVVQhJHVUpkD3ldsIp2V7TRx2eZ3HnlauD64OM1vYHet3+tbXXq5ntyUGarPww2fXXoVXqGj59Ypaf76fFT9box6zsw2kDjkDOY88s1kf/ZYzEcOG/Tn9b2h3Ac/3Fs1NX/usHXoVmjLSPn5goiP76qQU/W+MeB2abRRxyHnAeeW6wPrh49pPDbxARIz0x/ZncpEZhBcddTZ9bcCEDuohqGtRA8+VJPf01605tyqtxDSMy0BTriglttNYp5WtSjZKUIEjeUpR0SACSdACceh/Z9nyM04/IyVmthlpKluuu5fqyG20JBUpa1qiBKEpAJUpRAAFybYRd1p1h1xh9txl5lxbTzLqFNutOtqKHG3G1gLQ4hYKVoUApKgUqAII04ePDDQ4ccWEeBSH4vDdt85juq5OuCfxborI6+XceZwfMdNQjtAqyYVRmNlQBSiPpe3zozSvDS5B05+ONqOxvTTJ2FZJc3Sd6Rmfx1Tmytp+zjbw63ftQKK5NfbaQn5mRznrgDvj1yfM9T9fSoud82FZdaQ4dN7eN+Hhp4W56ak4vVT4bNKi+kPJHebp3EkDU8lW6C1ufDgRfDqrJtJKAyA1gYT5DJx59emO+e+Tn6Kv5lr5WXDv+qkqABJ1JvrYezjz88Myu1krK/Xve/PgNOHjwsAeV9Rirx7W+KIXG5f0YDHh2xt70Hb59pU1f6ebWn3ZQf9J2C5Je/bv5s9yc55hQPoTjC7tau99t/z05+2Rlb6Mm5fT1PTEaOrFYrji8BsHRAvaTbBZRgrsS1HD0yTz0OErPw6jPl9gxK2l1PdzxnZJV8zN+Z0hN+G7XJ4ufEeI5+WPoe2fVAjZls4Tf5uz/JSeOlk5ZpYtrwA4cDr1w5mlUUJ5PmjPwzjsemfo6/DUN1GqklQCj530HEWFul9LeNsdcyeVb1lHU9dSPby4i+oPCx4DvYdMCUElI/EUPMeXu+vGe3fGdM6ZMKgu6jwOl/Dn5WGnLTXmG8/MusAK13h9fHwHHU/bpQH4gxjfve8DsN3tygPovOtDX0UZQ1ynlc9cu0T82xsYJ5n1zLmE/8ALlW/OEjCQ6cWEPE+/sHb6jR9xt7NrpEhKJFyWxbt4UxhbuC6u2KhLpdSLLZOCsNXHCLpSOdSUIyClHSjvbhpbqcpZRzM20pbdMrMykyXEpKg0irxEyGFOED1G1O0stbyiE9640i+84kG+3YPzK3CzRnnK7rwbVWKHAq8ZtSgnvnKJMXHeSgHVTiWasXd1N1d026u262SLTNMpuAnp6fA+nfOfpzrJ2rVUkrJVa1+f0DTpxNvAcfW0gkydTrzPn/Mfjl6qgUynZ5SU+fn8f2ny7/TqMqtUyd+yrjUnUffwA4WvrpqLlTflSTrbr/P8dOPQYgW9tpV+MPYqTt5vxsFu3unZ+1kulCy9wqXZ1cmRaNbtzsT5Uug3BOisBRhtXFDmuUl2dgREzKRAjOqZlTo4laGdgZzYxtAp+Ztnef8i5Ar2coNQcr2X52YstUKoVSr0KRHYbmwGJU6G7Ilro0uOuV3RccdREqF20iPEX3dEu1U/tHy3NpGb8qZpzZS8uyoqKXVotIrdViQINUZdcVGmOR4slDMdNRjuoj74QlCpEMb6i9ISFV7f6Y5x2f7avef/rhO1pH+p52Df3Gdl/8AmLlv/VuKc/1XNqf90bO/+dFZ/lmD+mOcdn+2r3n/AOuE7R+p52Df3Gdl/wDmLlv/AFbg/qubU/7o2d/86Kz/ACzG1ovH97Qi5aizSLd4kd/a/VpAWY9MotfrNVqL4bHMsswoLL8l0IT85ZQ0rlHU4GueVsD7PkJhyVN2RbJ4kZlJU7IlZLytHYaSOKnHnYCG0JHMqUB449rO1XazJdQxH2gZ8fecO62yzmSuOurV0Q2iWpaj4JBOPaucfftC7YqDlJuXiP3+t6qspQt6mVyvVqk1BpDg5m1uQp7MeShKx1QpTYCh1SSNEXYJ2e5zDcqDsi2TzIzou1Ji5MytIYcHVt5mAttY8UqIwP7VNrUZ1TMnaBnyO8jRbT+Y6406gnUBTbktK03GuoGNK57Rjjpebcad4qN5ltuoW24hV4TuVaFpKVpPXspJIPuOupHZ82EtLQ43sb2YocbUlaFoyPltK0LQQpKkqFOBSpKgCkggggEa49KtrW1FSVJVtFzspKgUqScz1khSSLEEGZYgg2IOhGGZyJD8uQ/KkurfkyXnZEh91RW48+8tTjrriz1UtxxSlrUeqlEk9TqXkpShKUISEoQkJSlIASlKRZKUgaAAAAAaACwxHxJUSpRKlKJKlEkkkm5JJ1JJ1JOpOJM/Zk3w1Stwr5seS+G/vlt2LWaehxxKG3ZVBmBiUw0knndluxqqiShKQQmLAmOKICOrfzHE9IjsOgXLDtleCHBYn/ppQOuvsxbvse5obpWda/l15wITmOhh+ID/AOMqFFf79LSddCqnyai8Tz9GA0JGJqFvHzOP0Y88+vbzAx6402G4oHAa6Dhr4cD5639mNC3ZhN9eFzrrrf48fqx8FOny8+xJwMH6zn4E+8DOddqI45i1vePqAtcePTnjgcleOpPC9+B18PLn48sfFThJ75znHXH1Dv8AUR8NdSGQP2PtP8/EcOWh544VyDqb7vDxN+Z4i1787HjpoMOp4bdhXdyqi3dNzR1JsqmyuVmIvnaVck5lSuaO2UhP9S4jiQKg6FpU+5iEyc/KVsWF2K7Iv04yRmOvtFOVae/uoYX3jaq7LaKt6M2oAf1hHWkCc8FpU4spisneL62czu3x22BsMpStluzWYh7bFmSn97KqTQjyGdndElpR3VTkocKknMdUYcWvL0RbDrcRhC6zNCUCmsz5gaBQWY7MdhhhDLDDTTLLLTaW22mmkJbaZabSAhDbaEpQhCQEpSkJACUgau4642y22ww220ww22ywy0lKGmWWkBDTbaEgJQ222lKUISAlCQBYAAY+fWNHlT5cmoT5D82fPkyJ06dKdckSpk6W8uRLlyX3VKdfkyZDjjz7zqlOOOrW4tSlqJKrUqjZ5Ry9R1PoO3TJGD65PTp7hhCkygL+t116/wBHHhzPHW73gU4AJsnjbTzGvjbp9fLCjU2iJ+aSgfSk+nl5n1yff18tIUiXxsdLdb+HtP189CMO2JTtBcchbTXTiNBz6A+/jjs4dG6D5nbGemT5d/L+fv30kPS/Hn949nLqRhwMQQLWSAfEXPj4AHjp446FmjDp8zPmfmnz7EZI/Rn+bhXLPXhpe+vx5jTzOFFEHkE352t9R6fFsdJb5m23WIFap2USoL4dSCcIdbIKH47uMksyGVOMup65Qs4+cAdcE0Mzor8R8hTT6ClXDeSrihxJsLLbWErSeo10JwrUpcmk1CLUYnqPxXQ4n5qQtJBS40uxuW3W1KbWLapUba2w9ynmm3hRI9cpg/AyEEPx1lK34MpKUl+HI5SQHGVEAKGEuNqQ6jKHBqIZKH6ZLciPn1mzdC0ghDrRuUOovqEq42N91V0nVJxZSnTItbgM1CHfccFnWlEFyO8LFbDoGgUg39a1lJKXE+qrWn7/AAsXgvuG9dg9nuLq0KI9U3NhrgqFk7luQ2uZ+nbe7hyISaPcMvGFLptGvaDTqQ9yBxcd27mpJQiKiY+02s2MmVGjzUDeMYlp23ENOEFCjx9VLmmp0Lo8bOPLqxHffiq9UPgOtg6AuIBCgNeKkWOnEIvYaYoJ6YOHfg0YMGjBg0YMGjBg0YMGjBhXuHz8veyH53ttf1zoulvLP4x5f/fulfx+PhrZ5/EnOH5LZg/NMvF4FPYfAfZra3Hy0q4nzP1486MfmEw3t/I3uv8Am4vb9XKlpqZ7/EnN35NVv82ycSJsi/XU2cflxlb89QsVGuCZzwuJTb9eccrV1dfjaFdH1devu1mN2flbu1/Jyuj1Y/0eq2PoR7RTJkbGs6Mi5K2aLoLa7uZKOrnpyxPS/Pxnr65+GPd6jy92tOnZPje3Th9l/HhyxmFCooSANziRcAED2k6kDny9uNM/Ozn53fp3/b0+IPTPXprgck8bm9vq199+elj1w64dIH7Q8OAB8fs0+43s13i0k+JsPfiM5zDjeeR0mMn169uh+g9cahTb28V7Kc2C5sU0Xp+6Kknx5+3nci2J32HQO42l5ZctYpVVuVuNBqg0ve3zuH8+IJdZt40GwaMGJ1NlXVf6k23aR5WPZo//AIpRfL9+vca0k2N/rYZO/e+X+dqj7vg4optQ/XAzR4zY/wCbofx8HCmkk9TqTMMLHjRgxDzxSf2xly/7ztv/AN31nazN2p/rj50/KKpfxhWL67PfxFyr+8UD/sE4kHnoVVK27JIKkR2osFkZJATFjttOcvcYU8HVH4/TqTdo+ZESqu+ltf4OHFhU9FjYf1jDZYetrzkB5XiTcHExdnzILkXKlIcfa/CVF2ZWHNDc/Kcx2UwVaD1kxFxka/tRyGOypsENhPTqepOMD0/R2HxyNVzrlVKiv178dAddeR15k3t7tcaHZEyiAGT3X7XkNdfDl14jyx0raA0kDoMgYz3Hv6Z64wMefkRg6jCoz+8URc878NR0+PHzxbrKOXEoS3dGiQCTYceVj0018vLH1aaXJdS0jrkjmI6+7rnHQefTr19DhpyZAAUVGwGvn95t93PE6U+GiGwlagAbC3Lp04eI5ceNhhX7Vt7lDalIyo9MkZ7+ZODnzz8euCdMSsVLRQCuAOnQDXroT8dRxT5e8VC9hrw5D6fDQXAFgL6YcRblEA8Mcnp1wB5dgQO5yfq76jKq1Eeud6x6fWTr93vOGxIe1Ub2PTp7j16ezW113t6igBv8H6Y6YHl8cY9B07fRGVWqXz/WPPnrb7uB5WHW2qHIevfW3AHjfiR5eGnXjhc7eoeQkFsEEYUCkkEHuCnrkY8sY9Qemolr1WADl1CxvYGxB66c/aCbesRcgYSXXbG4Ub3Go4i2uh4g3seOnW/Gtx7ZjZ20Nud+LIvC1qXGo8rdG0J9VuiLDbDMabcFEqiYjtdU0n5iJtThzojM5TQSh92AmU4gy5Ep9+/fZBztVs27PKtBq0pyb+lTMS6NTZDqt95NLcpsCdHiOOElTghuyJDbClH1IpjsJ9RlOMqu2BlOj5d2kU+pUiM3DOaaEmr1SOykIZXVm6jNhyJjbaQENqmMsxnH0pH4SUl+Qq631HDufZ/U9czhm22S2klS3Ln6gE4Au2tpyT2BGOmm1t2zH6BmmrQ0OWWhqBpf5oXToi7+AurXre54jGnPYdgtJ7OORZ74AR6Rm219CvcznXxYX6lNja2luJ0MqVj2gltLWWiVHl5jy9VZ+3Pn9PpqkmZ8wFanB3uhuVG+pOpNz7eXAHpixNfrJUVAKAAuEgGwA4WAvpb+Yk6gOtta3Q2lscnoD78dOwz7vXp5agmuVcrWob2gvzJ8uPt8dMRHVaiVFR3uvPh7zrw06cPEVC/bJsfJuPXclnGOS1tten+6smjr/TzZ1sT2OHe+7OmQHL33ns46/wB7nrMqfsxjR2ol97tyzo5e+8jLP0ZSoSfb83jiLTVncQBi+xw9UoHZfaVfL1XtxZSzgdTz29Tld/eD8M+7WBG1KoqG0LP6AT6uds2pte1gnMNRA9w5253443tyFMI2d7P072ici5QTbyy7TQPb4dDrxw4yFTgkJ+bnGP2dR5Y9/p2HU6iaTLKidRf6AONrePvIsPJSflFRsDrrrx49T9Y4fZvfCS20sAdfDUCfoPb6e/me50kuLK94nofj4+8niBJUCdSSL+/H57vEJ+XzfD87+5f66VrX0iZQ/FLK/wCTtE/NkXGFOZ/xlzD+/lW/j8jCQacWEPDh+FLfmp8NHEBtrvLThKej2nXWzcFOiLCXKxatSacplyUotrWhl5UmkypKoqH1BtE9qJICm3GG3UMLafkeLtIyDmjJUpaGhXaY4xFkOAqREqTDjcylzFBIUoiJUY8aQoJBUUtlIBvbD32b50l7PM8ZczjDC1rotQS9IYbVuqlU+Q25DqUQElIvKp8iSwN4hIU4CSLXH6DG2ty2xuRZ9s37ZVXh3Bal2UeBXbfrMBZciz6bUWESIz7RUlC0KKFhLjLyG3mHErZeaadbU2n5y87w6xlat1fLlfiO02tUWdIp1TgvKSpceVGWpt1sLQVNuN3G+0+0tTLzRQ80tbbiVHbCn5hpuYKTArlGlonUuqxGZ0CW2ClL8aQgLbWULCVtrAO64y4lLrTiVtOJStCxhY4cUMIGR18vd2/f+brmJpkpT61AH1b62/ZHx8Pg9ByuOFZOtx8H404+GNVd9n2tf9sVyy71oFKui1Llp0ik16363DZn0uq06UgofiTIj6VtutrHUZHMhYS42pLiEqHTQK/WsrVmnZhy7U5lGrdJktzKbU6e+uPLiSWyd1xp1BB1BUhxCgpt1tS2nULbWtBS6pS6dWqfLpVWhR6hTZ7Ko8yFKbS6xIZXxQ4hQI0ICkqFloWlK0KStKVCAHfb+D1bLXjcM64Njd2bk2liT3XZC7Nr9Iavi3YDrmCGaJPM+j12BBSrmV8mqUuuuJKilmSyyltlGlGzz9EvzdSKbHp20bI9PzXJjNhv5eoc40KdLShKQlc+nux5sBySpQUp1+GqAwoFITCSoKWuoOa+x1l+fMdl5SzJLoTDyis0uoxRVIzBUSSmLKS/GlIZTwQ1JEp0a3kqFgEksz+DjU9uqx3dwuJuZMojboVLhWbYTNOqspgY5mo9UrdbqkSA6rrh9ykVFCemY6/J5139FAjehuDLOyd8T1JUGnK7mRsw2llJ3XHGafTkvPpSuxU0l+OVpBAebJ3g3aZ2L3O/SaznpBihQK0UyjqD60g+slLkqYW21KToFlp0IOpbWBYzo8L/AAWcO3CBbbtA2VsSJSJs9mO3X7vqqxWL0uRUcEoXWLgko+UKZDinHm6bCTCpMZ11xUWAwFEaz22v9oDajtwqKZmeswOPQI7ynqdlymJcp+XKYpW+kKi0xLrgckIbWpkTpzsyoKaJbXLWkkG1uQdl2TNmsNUfLFLSzJdbDcyry1JlVickWJTImlCClpSkhZixkR4YWApMdJAIxeLbgr2K4yrClWfuxa8VVYjwpjVpbg0qLDj3tZM6SgFEyh1hyO64YpkNsPTqNLD9JqYYbRMiOKQ0417tinaA2ibCswMVXKVWedpDshDlaynOfkOZfrjIshwSYaXEpYm90N2NU4wRLjqCRvuMd4w54bRtl2VNptKcg5ghITObZWim12O20mrUxw3UgsSFJKnY3ees9CeKozwKvVQ7uPIpC8bfARvVwP34ugX5T1V+w6rIV95O6VFhyvvXuWOUl1MKQ44gmi3JFbBTUKFNX4oU25Ipz9Rpxbmr3z2EdoPIe33LSKxleX6FWojSRmDKc91kVqiSAru1LW22tSZlMeXZcKpx7svNONIkIiTe+hs5c7TtlOZ9l1YVArLHpNNfWTSq9FbcNOqLRTvhAWtIMea0m6ZMJ2y21oWplUiN3Ul1j2p2xGWFS2U3CXtXupZN+Dxfk9ArTDtRSwnneXSJaHIFXQ0jmR4jqqbKleG0VoS64EtqWlKiR6ZDIfZcaPBaSAeiuKT7DY/Rh1ZIzM9k3NtAzMylbhpFRYkvMoUErfiXLcyOlR0Sp+I480kq9UKWCQQCMWcqdVIlXp8GqU99qXBqMSPOhymVh1mRGktJeYfacGEKbdaWlaFjotJCgTkaa4ZKSUqG6RcFOhseY8dep8tMa5xanGqMSLUIchEiHOjsS4rzdwh6PIbS6y6m43rONqSrdKQRexsRYZZJ7qOPdnJ+v+YfTr2pQOQv8aeA+jAp0nhpyv4fX7yfLXCp7O7ZTt1LwjURoPx6NE8ObcNTaRkxKcleAy0tSS2J09aTGhJXzEHxZBbW3GdAkfZns/l7QsysUpJeYpkYJmVue0i/okBKrFCFqBbEuWoGPESoK9creKFtsOAVb7W3aUovZk2UVDOL4hVHOFXW5Rdn+WpTxT8tZida3hIkMtrTIVRqI0oVGsutFsdyliCl9mVUIhVNtZVsU+i0ynUmlw2oVOp0ZqJCiMo5WmI7KAlptI6dsZWpWVuLUtxZUpSidE240GkQIlKpsduJTqfHRFiRmgEtsstAJQhNuKjqpajda1qU4oqUtRPy6zKtX855hrOb81VKTW8y5lqUmr1qrTFFcmfOmOFx95ZOiUj1WmGUbrMeO21HZQllpCErpRaWCUDl6nHYYP6ev1f/AA6Q5T/E34/V7vcOmp0Jw8abCACQE8hyP8/DnxHK3LCs0elDCfmdseXfGPQDA9+M9PM4Gm1Lk8db3+Lcfafr54fUCFbdJSfbzPL2Dn7teaj0+lpwk8vX4dO4+rHl6nHc6QX5J1AJtzN+nADr1+vQYdMaJw01+oe2/t8NL3x2MSl9BlI+r7B27Z6+7qc9NJLsjjY/Tr7SeXLX2DS+FtmIANR9FgLefTS3Pjpa2N8zTU+SM59U+7y9w+v3DOuNb/j7z9p+wYUER7W9X49gv1PL78wU04/rYHTp8w9fq/f3a9PpKR+y/wD7H7se0RxroT7D8fb446yz7iqll1EzISQ9DkciKlTXCUx5jKchJPQlqS1zKUxISCpCiUqC2lLbUnVODHqrHduncdRcsPpIK2lm19DbfQqwC2ybKGosoBQW6FV5dAl+kRx3jDlkyoq7huQgXA11KHUXJbdGqToQpClJKqbqbX7VcVmzW4W0t5U9u4LF3Ns+s2betuvlDNQZpdfguwZKT/XDGmRisSKbUWQ42zNZjyWFqcawI5nQ5EMuQ56CWnUrbDib908gi282s6b1jfcUApJNiNMTZS6pBqzSJlOdT3je6tyOuyZEZdz6rqAT6pN0hxBLaxeyuIx+Qv7RLgb3J9njxXblcNO4zE+S1bdQVVdv7vlQFwYm4e2tWfkqtO86aPnMLROjMPQKq1FdeaptxU2s0hThegOARrLjLiPrZVrum6FWsFoN91YHjwIubKBHEYf8d9MhpLidL6KTzQsWuk8OoI01BB54ZBrmx78GjBg0YMGjBg0YMGjBhXuHz8veyH53ttf1zoulvLP4x5f/AH7pX8fj4a2efxJzh+S2YPzTLxeBT2HwH2a2tx8tKuJ8z9ePOjH5hMN7fyN7r/m4vb9XKlpqZ7/EnN35NVv82ycSJsi/XU2cflxlb89QsVD+DVZb4ibFWPJq5/PHe1K0P26y/wBg6gnaxlFR5PVbrxNAqoHDxOPol24NB7Zbmto677VK4eFdpavsxOK9Mznrk9D0Px+nz+vv01pI5I6ePlz4c78+vjpjP6LSwm10gWsLbvLlrrw6XuNBxxqX5mAfnepwD5fVjPX6B6DGeRTpN+fj/N9pwvMw20AXAB0HDW/jfhw9vC174bTxTyi7she6M94bOQPdIbIz+5z8NRBt0N9learm/q0bjf8AdDSfj+fErbIEBO0TLthYA1O38C1L3+2xHTEIus6MXkwaMGJz9lfyU7ef4EWb+qlF1pJsb/Wvyd+98v8AO1RxRTah+uBmf/HY/wCboWFR1JmGFg0YMQ88Un9sZcv+87b/APd9Z2sztqRttHzoemY6kf8ArCsX12e65FyqP+QoH/YJxJNRoWW0PqGVOfhCfUrJUe4z/GyT556ddNfMFaU8484pd1OuuuKN9d5xSlk3IPHe4G554012X5LSxSqKwlkBDNMpzKbJFglmIw2PDgnTgLajiBjtY7IQkZA9/T6PqHbPbAOMjOomqk/eKrKsTfnr4dOXPT7roZOywG0sju9LD9jYpA6Hr16c8fU8yyEJ6qJAHTrj1x1+j7Mc2mbJkFV78NTx+/TS3hc6DTFiaLS0RmkKUmwSAbkfz68ufQcbYUS1qAVrQ4ts5JyTj0x06jyz1ySPUdTpm1aohKVJSdBveNzwvw1+L6Y7p0sAbqTYDQDjbqdD48hyHK1nF21Q+jZ5P5Pl3Hwz26jv0+OoxqtQ1Wd63G4J4fVz+k4akl+5Jub3PG/Gx1Ol+fEdepGF4t6i4CCU/wAnHT3evTufoHv8o0q1Svv+tbQ35dT7fHrrblhCkv8AGxte/TU9T4fHIYXS3qKctkt5zy4PToPLPxHrkZ7dcaiquVUJSv17AXvqePEj3aW8B4YR3XBrr79L3JAPu93AcrrlQKN1bAQPI9sk49fd26/UDjOoRzFW9HBv6EHgTpxuPE89fDgbWTnF8za/K3P46aW4aHFc/wBvdHTG3Q4fW09/vDu8q+P3dpXp6dtaEfof8hUnIWfnFHjndq3l8gUsfTa/AYza7bBvnTJt/wByr/55nH4+Dh1nsxbdE3hS2xmlHN4r13DJH8Vu9K+2fLOMp6/UOuo87TdYMbahmWKFG6WaNpf5oVQacvQX0uVE9LnXGgHY/qncdmPZ7H3t3u3c4HTnv51zCvUXGuvPlpytiYG0raDaWstjOBgcoz+zB6de+qUV6slwrQlenra73E9Tbr7D9so1eplRX61tCON+Pn8GxHLDgqDRw2lAKeuAOgz7+/rgdSfXPTrqK6nUCd4bxI1NgeNr8+Y6W59cR3UJu8VetpfqfG/n/N54pb+2sa8H2g+6TeAOW19sOg7fOsGhq6e45zrb7sUL7zs0bOlk3u9nXXyz/mkfRa2MlO0qrf20ZvVxu3l3j4ZWoo+zEUOrVYgrH6CvDxCT/qGbOLKQAra+xFZ7nrbFMPn657dupz16a+d7apJP9UraIkcs+ZwHuzFUhz93Dy4m25WQ3lKyBkRIuLZKymCeHDL9PGn3/fhckpCRgDH7fjqOSSo3Jw5sern9bc/3Cv8A3TrxVwPkfqx+p4jzH14/PZ4hPy+b4fnf3L/XSta+kfKH4pZX/J2ifmyLjCrM/wCMuYf38q38fkYSDTiwh4NGDE53sm/amDhbqMbYrfefPnbD12pA27caiuY/tLVZzy1yn1MJQ5Kk2bUpDgeqUON4j1HlLdqkGO4h+cw7Qjtjdkk7ZIK895BbjRNo9LhlMyAoJYYzpBjNpDER18lLbFbiNN91Tpb34OS1uQJbjbaIr8W03Z+2+ubPnE5SzS6/IydMk78WSCp1zLcl9ZLrzbVlLcpkhxXezI7XrsOFcuOhbi32n7k1tXNbt5UCk3TaVcpVyW3XYTFRo1dok6PUqVU4ElAcYlwZ0Rx2PIYdQQUrbWodwcEEDDGs0Wr5dqk6iV6mzaRV6ZIciz6bUI7kWZEkNmy2n2HUpWhQ0IuLKSUrSVJUCdJ4E+DVYUao02XHnwJrKH4kyI6h+PIZWLpcadbKkLSdQbHRQKVAKBA3mkzHXg0YMGjBg0YMcdf+4VkbV2jW793FuijWbZ1uxFTazcNfnMwKbBYCkoT4jzyk87z7q0MRYzQckypLjUaM06+622pfyxlbMWdK5Ay3lSjT69Xak73MKmU2OuRJeVa6lbqRutstJu4++6pDDDaVOPOIbSpQTKzWqVl6myqvW6hFplMho7yTMmOpZZbHBI3lG6nFqshppAU46shDaFLISaUXtTfacVbjSulvbjbb7o0Dh3surPSKVEmNtxqnuHXojjsZm8a4ykuOxKc0yXDbVFW4lyNHlOTqq0mpPIi03evsk9lKnbBKKvMGYvRKrtNr0RDdTns3di5eguBDqqDSXFWCz3gSalUEpSqa82ltoiI02HMwdu+3GXtQqCaTSO/g5MpchS4cZyyH6vKRvNpqk9AvuAIUoQohUoRm1qW5d9xe5D7q52K8YNGDE1vABv4m7rQVtHcU1r747MjFdvLdcUJFWtfxQG2yFkhyRRXXkQVeFygwDAK2y8mRIfS5jASvvgLheihyCuv/ADh9Ivzxezsz7RU1SkLyJVZKPlGioW9Qu8IS5LpCipx6Igk3ddprqlqSn54hOtpQktQ1lEjzbbjzjbTSFuuvOIaaabSVuOuuqCG220JBUtxxakoQlIKlKIABJ1yoQta0NtoU444pLbbbaStbi1qCUIQhIKlLWohKUpBKlEAC5xad99iKw/KlPsxosVl2TJkyHEMx40dhtTr8h95wpbZYZaQt111xSUNtpUtaglJImF4edr2NvbRg091rNbqXh1O4ZGE8y57rYKIYI/8AsKa0oRGk9QpSXpHRT6saSbL8itbPcnxYLqP9uqkG6lXniBf01xH4OGkjXuaayoR0C9lOiQ+NXyMfKD2uu0TN7T23KsZmivKGQsrOSMr7OIIKi2mgRJJ9IrriVaenZnmNGrSFEJLcM06AbpgoUXo0GCMI+b6fYCe2fTocYI6e4OCY8dbnqfu9nK+pBxFVHiiySBa1gBp7PZ48h7bLPQoAISSnqfLHbOD5DPw+GO4GmtMe46jifj7xxHHEkUyLfdFultOFudvq5eFjhW6TAHzPm+mPcfTyHTvk+nXI02JTxuRe2v0Drx1vy8tDh7w49t3TpYD324+et+fgcKJToICQcZ7Z6YJPl/N6egzpCfev5cuY8/Hw9500w5I8cJsLanibfFvs1J1x10SF2ynH0f6evbvnHTGTpLdeOtuvHj15cz48/AahWaZ4C3t+L2HU8/G+N8zC7YTj6Afd164HT17fTrhW7fx66n48rcuJOO5DIHADzOn0c+PE3xmiAMfi594x1/m/Tr198eRAHn/OMe3ub8r/APNvj5OQeh6foBx9XXp8CPXXkl7W/E8yDf44dceKmhyA91j8eeMeO7PpMpudTZUiDLZJ8ORGWWlgHuhRScLQr+O2sKaUBhaFDt5rSzJbLL7aHmlWBbcG8Da9iL6pI4pIsoHUG9hjxZckwnkyIjzkZ9u+640ShQB5G2ikn9kFApI4pI4Vy/4UPZnD5ufwO29uzvHOhWpxBbVXhS6LsNctIjRzWtxHbmf8O6dsaxTVgF+gvUdmVer1ShuxPuBULYZktJWxUZ1Oqkc5zoNOhwPTm5SmXA6lEeK4O8U6pZ9dppdwoNoRd0lzf3Qi2+VKSky5kbNdVqlQTTZEJL4DK3JM9pXdBpDY9R19qxbK3HCllIZKN4r3g0EoWpP54GotxLmDRgwaMGDRgwaMGDRgx3e11zwLJ3N26vOqMypFMtG+7RueosQUNOTn4FAuCn1WYzDbfejsLlOR4jiI6HpDDSnlIS482glaVGkS26fVqZPdStbUKowpbqGwkuKbjSWnlpbClJSVqSghIUpKSoi6gLkI2Y6c9WMvV2kR1ttyKpRqpTmHHipLKHp0F+M0t0oQ4sNpW6lThQ2tQSCUoUbJNi8e2b4ZQADY+9PQAf2As3/PrWgv6sHZ9+57OX+S0T/XmMaj+hrbZSSf05bM9Tf+2GaP+6uPP9Ob4Zf7h96f+gLN/wA+tH6sHZ9+57OX+S0T/XmD/Y1tsv7stmf8IZo/7q447cT2vXDjdtgXtatOszeFmoXJaVxUKC9LoVooiNS6tSZcCM5JW1ezzqI6Hn0KeU2y64lsKKG1qASUXMnavyHWcvVykR6Dm5t+qUio09lx6NRksodmRHY7a3SitOLDaVuArKG1qCQSlCjYF05G/Q89rmWM55UzJNzbs6fh0HMVGrEpmLPzKqS7Hp1Qjy3m2Eu5ZaaU8ttpSWw462grICloF1CCDY3cGm7W7m2/e9Whzp8CkN1dD0ampYXMcNQo8+nNlpEmRFZUEOykLcC5Df4NKyOZQCFU0yBmdnJubqPmWREdnM0xcxS4rLiGXHvSafLhAJcWlaU7qpIWSUm6UkDUg41czrl53NeWKpl9mS3EcqCYqUyHW1Ott+jzo0tW82hSFK3ksFAsoWKgToMP+Xx8beL7WteyQfSHQs/X98Y/c6s2e1HRj/5p1QnxqcS3s/rb7BiAE9nippFhmSn/AOQSf/z29nDw43w18d23i+1sXqPd8ioeCff/APSP9/q1+fqoqN+5Kp/wnF/kuPMdnyqD/wA5KefOBJ+j8Pp5DCXbu8Wdl7h7f3BadMoN0xZ1WjttR358aktxG1B1ClF5cetS3kgICiOSO4VKCUnlCitLN2gbeqZnPKNXy1Hy7OgvVMQQiU9OjvNteiVKHOVvNoYQpW+mKpsWULKWFG4BBdOS9jk/K2ZabXX63DltQDLJjsxHmluekwZMQWWt1SUhBkBZuk3CbC17hgeqzYnzBowYkT2/4xLFtOy7Wtudbl2PTKHb1Do8l2JGo7sZ1+k0iDTHHWHHq3GdU08qGXkeJHaWhLgQpJKeZVockdoGl5TynRMuP5bnzHaTGeYXKanx2m3i7MkygpLa461JAD4RYqOqSeBxX3NuxioZjzHVa4zXYcVuovtvJjuw33FtbkZljdUtDyUqv3O9cJFt63K+Ox/o5tu/7mL0/wCZUP8Azi06v1UVG/clU/4Ti/yXDd/U+1X90lP/AMgk/wD58H9HNt3/AHMXp/zKh/5xaP1UVG/clU/4Ti/yXB+p9qv7pKf/AJBJ/wDz4YrvDuDTdxt0qxfdMhToVOqCbXbZiTwwmaE0C2qHQnlupjvSGEGS7SnJDaEPuhtt1CFLUpKjqq2bq23mXM9dr7LC4rVXqcqeiO4tLjjKZDhWG1rSEpWpN7FQSAeIAxYnLVJXQaBSKM48mQumQI8Nb6EFtDqmGwguJQoqKQq1wkqJHU4ehB4xtsYrDLa6JfXO222lQRRaApIUlICsKVeCCsA5wooQSACUpPQMqbBflFVpCUgk23kKUbE6A+v0tfqemL3ZX7UeWcvxYbDmSapJXGYYaUtFVhtha2m0IUoBURRSklJIBJIFhe+uNieNPa//ANSX7j/gS3z0/wCufx+jp27tt/Kcl659OaF+rK+PsX9VvC3OYaV28so01KQdm1edKbaivU5PDwNPPxqLWFsqDxsbURnvFdoO4LvUdBRbcT2Plm8T8PPSS/kOY6gpTU46CeZjuHy4Ojl5faHOv9EWysWw2jZjX0AC34w046+fyfw5cufM6KVSvaJbOU0JCrN3GcCcdE022U9Bj1u047e/TXmbI6lKJIrsVN78Ybx+p7rfCQ/+iCZbeJI2cV0XJJvXqeSegv6B7za5166KNTPalbKQOXm2+3Md5cfiw7VTnB991H6z1+3TXmbAqtK3t3M0BF78afIUddDwkj4v54TXe3tl1y//AOndbF+P+3sD+QfFzpjv6b7X7Y6Dy8+2G6TgTj8Vm0k9RnP/AN5/2/Rppzey/XJQVu5xpqL3AJpUpVh/lg+D5DHA526svLJP9T+tC/L5cg+wX9B4cz7uWFApvtsdh4ISFbRbsOcuMcps9Px73J0z+j6ejJqXY1zJOBCM/Uhu/wC2os1WvjaeL24jxOOVfbhy8on/AMAa0L/8tQelgP7C4dfqx3kD28OwMJOBsru6pWAMh2zAPf3uLPr3/YNMGf2Ac0TlXVtMoqR0/S/PN/M/KI+NOGPQe23l8kn9IVZ/hqD7v7CxEz7SbjcsjjZvLbS5bJtC6rRjWRblbos5i6V0hb8x+qVKJNadifcmfPbDTTcZSXPGW2srWOVBSCTbHs3bDZ+wrLeYaFPzBEzC5W66isIkQ4L0BEdCafFg9wtt6RIUtRMcub4UkWVu2uL4rDt32uwdr9codXg0WXREUmkOUxbMuWzLW8tc1+WHULZZZCEgPBBSQo3Te9jYOO4N/aibU8NexNk7U3NtlflyVm2F3AuXVKLJt5qnSfuvctXrbHycTqixJHgx6g0y54jKPwyHOUqbKSY72x9lzMG03PFazXT840qkRqoimobhSqXMkvM+g0uFT177rMppCg4uKp1O6kWSsJNyLmdNj/a7o2zHZnl7IMrJ1Wqsiiqq6nKhGqsOMw/8p1uo1VG4w7FccQWkTksqKlq3ltqWLBQGHt0z29mxEAJCtit1nCnzTUbQT27HrVO/bzxqCZf6H9m6UVH+qXQE360Gon/7/Dtldt+gySf/AAErKQb2vWYJtf8A/Z9Ppx28P+EMbBxQn/6v+7aselWs1OfrqR/m+jTdkfocOcHySdqWXRf/AN3qmf8A/QHx9CE/2x6G6Sf0lVcX61eH9kTly+rEAnH7xOW5xf8AE5eO+tqW1WrSoty0i0KbHolwPwZFUjuW5bVNocl192nOvRCiQ/CceZCHFKDS0c4SvKRoPsE2YzNjuynLGzqfVo1blUBddW7U4kZ2JHkCsZjq1cQG47zjrjfcN1JMdW84rfW0pYslQSKibTs4sZ+zvWc2RoT1OYqiaaEQ5DyH3WfQaTBpyt51tDaFBxcRTqbIFkrCTcgksw1MOGFizJtr7czZex9u7DsyZstufMmWnZ1t23KlxahaiY0qTRKPDpr8iOl2podDDzsZbjQcQlzkUnnSlWQMzc29grMmZM1ZnzE1tEokZqv5hrdbbjOUOe45GRVqnKnojrcTNSlamUyA0paUpCikqCQDYX4y52xqDQ8u5fojmSKvIco1DpFJckIq8NCH102nx4S3kIVEUpCHVMFaEKUSlKgCSRjtv6frsf8A+w3db/pK0P8A/U03/wDY8Mz/AN0qg/wDUP5dhZ/VtZe/cFWf4ag/yLHqr2+ex6kqT/qHbrDmSRn7o2gcZGM/2V0H9DwzOQR/VLoOv/INQ/l2P0dtvLwIP6QqzoR/5ag/yLFZ3cy6Y187j7gXtCivwod4Xvdd0xIUpTa5MSNcFen1ZiLIU0VNKfjtS0NPKaUW1OIUUEpIOtP6LAXSqNSaWtxLy6bTIEBbqElKXVw4rUdTiUkkpSstlSUkkgEAkkXxn7VpiajVKlUEIU0ifUJkxDaiFKbTKkuPpQpQAClICwkkAAkEgAY4nSnhPwaMGDRgw93hO9oTxPcHFRio2rvhybZCZi5dT2uu5DldsOqB4qMkN05bzM2gyX1q8dc+2qhR5jkhCVSnJTCn470HbX+zrso23RHEZ3y40qsCN6NCzTSlJp2ZaekX7os1BDa0Sm2SpRbiVOPPggqUTFJIIkrIO1vPOzd9CsuVdYp/fd9Ioc9Jl0aWVW7zvIiloUwt0ABciC9ElEAWfFrYsE7J/wAId2UuBESn78bRXrt3UVhDb9fsh6Fe9sJXyDxJEmHKfolxwWfEyEMw4Nfe5cEqznWbuef0MnN8Rb0jZ1n+h1qMFKU1Ts1xpdFnhsAkITOprFUhyHjokd5Hp7R+cpaAbC3uWu2Tl+QltrNuVqnTXSAFy6G7HqUXfNgVGLMdgyGWuJ9R6W4BoErOuH8UL2wfs8a9FalNcQdNpniJSVRq7al7UiU0pX8RxqZbrfzk/wAYoK0Dvz41XOf2F+03AW4k7PUTEoUQl2BmXLEpDoBsFoSmrh4JPEBxptYHzkJxLkXtKbGZSUqGbvR1KAJbl0euMLQbX3VKNOU1vDgdxxab8FEa4+df9sP7PG34jkpziAgVUoQpSYtBtK96xKeUnH4NtuHbqwFqJwkurbQe5WB115U/sLdpuoONp/qftQkLUEqeqGZcsxm2hexW4j5VW+UjiQ0y4sjVKDcX8ZfaW2MxELV+mwylIFw1Do1beW4bX3UKNPQzc8PXeQkHQqGI+98P4RBtLRW5tO4ftnLuvmooC241x7hyIdn24XOUckiPSaZKrVfqEbmPVuZ978g8pHKnmChZPIf6GRmSS4zI2l7QaTS4+8lTtMydGk1WYpq6SWzVKtGp0WM8U7yCW6dPaQohaVvAFKofzP2yqMylxrJ+VKhOdsQ3MzA8xAjhdiAr0KA9MfebvYgKmRXFJ0IbJuK8nFPxy8SPGJXV1LeW/JUygMTVTKHt9QUrolg26cFDIp9AYdWJclhtS0Iq1ckVatFLjqVVEocKNaV7J9hOzDYrTRAyFlmNT5TsdDFQr0smfmGq7u6pa59VfBe3HXUh5UOIItObcCfR4bKENoRT3PW07Om0aX6TmisPSmEOqdiUuOBFpMHeuEiNBaIb3m0Hu0yJBfmLRfvZLilLUpompfwwMGjBg0YMdVZN51/b26qJeVsS/kVboM5qdDdIUplwoPK9FlNoW2p6HMYU5FlspcbU7HdcQlxCiFp8VJStJSoXBFiPjgfHCpRazUsvVWDWqRKXDqVOkIkxJCAlW44g8FoWFNutLSSh1pxKm3W1KbcSpCiDa19nPuBbHE67Bv2IWGZVkMRX7nt9a+eTS7qe526ezyLSC7CUWZNUhzEpLbiGGEKKH/GZamjs85GNfzyaxLbSul5RZbqd17qg9VHlrapDe6ebS25E4qI3QuG2nXftj2/ogfbApsLsvM5Ry3JXAz9tkmv5Nq9OYU427RcsQo7cnOcxD28VLiVWO/T6HDIWpbsWtzAvdfhPJTPba0X5rZx16AduvTqO3fp9Prq8dRcJvfmfPhw49b28OJvwxg9lmMEtoIFhYeQ1056aA9NfC+F6oEcHw8jvjzx0x7veCft76ZsxfzuPE3+73c/LEyUlnRNgOA4+OltbaG/HnfXC20KKAlHTHXA93QZz6j19O+fINKa5cq19uvD7x9vtxI9NZASOthw00GvmDbTwtoNcKxSYvRIx2GPX9Pbscd/XI02ZLnH238h8cOVxh5QmtQTysRxOp16ey415a4UKBHBx07Yx6+X0Zx9GSCeukN9w62OuvvF7+732vbDgYb0A4XHD22PhqbeWOuiRh06Y+oY9O/mfpwD6nOkt1zjroNP5vv8AsAwqttjp0t4n7h04fTjfNRxjsAPh9nT9J/RnXEtzX7OQ+8/HhjsSjwufo+PPGWGU+hPr0z+w416Ss81W9tse4Nk8/cL4+a2Ae31H+f115Bwjj7xofj4vjxUjqAR9I+PDCV7vbl7ebIbc3ju1utdNLsnbywqJKuC67nrL3gU+lUyLypU64UpU69IfeWzEgxIzbsudOfjwYjD0qQ00v2OTGYrLkmQ4lpllJW44rglIsLG1ySSQlIAJUohKQVEX8WYT819qLFaW/IfVuMtIsVqXYqI1sAkJSVKUSEpSlSlEBJOPzD/a4+0xuz2kXEVIuiEKjQNhtujULd2RseYhMeQxRnZRVPvS447bz7arvu9TUZ+ehLzjNJpkWlUSNzqhSpk6GMxV12uTS76yIjN0RGTpuoJ9Z1YBILzpAUs3O6AlAJCbmwOVsuM5dp4a9VybI3XJz6bkKcAsllokAhhkEpbFgVErcIBXuiKPTfw58GjBg0YMGjBg0YMGjBjKgw36hNhwIwCpM6VHhx0qPKlT8l1DLQUrBwC4tIJwcDrjXsZZckPNMNJ3nXnEMtpuBvOOKCEJuSALqUBckAczjxWpKEKWo2ShKlKNibJSCSbDU2AOg1wp26O0FxbVKoSq1IgzYlfZliHLgqc5BOpjNNdqsJbbyUO5iJq9PW2/yhuS1JQpIbdQ+wy9s97Pa9s8nQIFdXT3XajBE6O5TpDshkIDqmXGnFPR4y0vMuI3VgNqaNwW3XE3IRaFmCDmBh9+Cl9CY75YcTIQhCyd0LStIQ44koWk3SSoK0IUlJ0wk+mLhcx3G3th1Xca5Y9t0l6LFdWw7LkzJinPAixGVtNrdKGUOPPOLefYjsNNowp99svuR4yX5LLlyhlSqZ2r8LLlHMZM6cH1IcmOqZistRmHJL7z7jbbzgQ200s2aZdcUqyUNqUoDCbVqpFo0B6ozO8LDG4CllIW6tTjiW0IQlSkJKlKWNVLSkC5KgBjJ3H23uHbOuGjVxCXWnkrdptUjIeEGosIKefwXHUJCZMcONCZE5lrjF5lXO5HkRn3+nOmSa5kOsroldbZD5ZRJjSoq3HYU6K4pSEyIjrrTDi0BxDjS0uMtONuIUhaEkC/ro1ag12GJsFaijeLbjToSl5h1IBLbqEqWkK3VJUClakqSoFKiMdAxsrc8jbV3cxqTTVU1pLsg03nfE8wGFlt6UFFn5N4iEpVK+T+Lgw0qUH/AJXywlKadmmZFZBc2jA0/wCQW5AZUyZS/lLujORTRLEfuO4Mf05xMfd9K9Jvd30fuB3uOY5kpwrqcvWf9OUjfCw2PR94MKkloub+/v8AcJLl+77v9h3m/wCrhHtR9hfx9WGHpLzMaMy7IkSHW2GGGG1uvPvOrDbTLLTYUtx1xakobbQlS1rUEpBJA1+gEkAAkkgAAXJJ0AAGpJPAYOHHDrrb4TLmqdKan3BcUK35cxlD0GmR4KKs9lacpZqDz9Uo7UZ7JBIgCrN8ikq8XxOZpNhaL2ac/wBVgNTpUih0RbyEuJgVORPM5tKgCn0puFTpbUdZBG80t4vNm6XWm1gpxH8zaRQYr6mW25sxKCUl+M2wGFEGx7tT0hpTiei0o3FCxQpQN8I7uTtFdu10iMmvtxJMCctTcGrU1x52E+4hAcUwsSGI0hiShGSpt1kNrKHTGekNNl0xnnjZ1mjZ9Kjx8wxWEtTC8IM+HITJhTO43O97pzdbdQpAdbKmpDLDwStKi3Y3w5aLmGmV9pxynurK2dzv2HkFt5nf3t3fTdSVA7qgFNrWgkEb18Jdpi4W8dzt1YVU3IueNbNJfjRXnWH5ciVK5y1GhxuTx3eRsFTrg8RCWmstpW4pIW60jmcS58n5Tqmdq9Ey7SFxG5stL7iXZzrjMVpqMyt91x5bTMh7dShBslph1alEAIOpCZV6rGosB2oSw6plkoSUMJSt1anFBCUoStbaLknUqWlIAJJxrr0tKqWLc1VtWsFhVQpTkcOORVlyO+xNhx6jCkNKUlCwiTBlxnw24hDrXieG6hDiFJHBmChzstVqp0GphoT6VLdhye4cLrCnGz89lwpQpbTiSlbZUhC91Q30IVdI98CcxUoUafGKyxKaS83vp3VhKuS03ICkkEEAkXGhIsT3dQ2Suqm7axdy5D9P+5z7UOY5TUrdM6PTakuOinTFr8P5OtUpMqNI+TocKmo0lkrc+U+NFYc0nZvmGLkSHtDdVTxQ50oxmWEyXDUgn0p+EmSuP6OGAwuVHcaSBKU/81amA2rfCa3mKnuV17L6RI9NZb7xSy2n0cnum3y2lwLK98NOJUSWg3xAWVAgI5pg4XsbOiUmXX6zSaFTw2Z9aqcCkwg8sttGXUZTUOP4rgSsob8Z5HOsJUUpyQlRGD1wIT9SnQqdFSFyp8uPCjJUoISp+U8hhlKlHRILjiQVHQA3Ogx6n3kR2HpDpIbYacecIFyENIK1kAcSEpNhz4Y7Xc/bKsbW1yJRqvKiTvl9NaqkOXD8RLbrC3n4riFtugKbcakxnkYysLb8N3KSstodGesi1nZ9WW6JW3IL0l2EzPaepzzr8ZyO+t5pJCn48V5K0OsOtrStlOqN5JUhSVFModch1+GqbCS+hpLy2FokIQhxLiEoWQQhx1JBS4hQIWdDYgKBATfTMwsYNGDBowYcPbHDldVz2A5esWay3JfjrmUahCKp5ypRGivPizBJbXGlyQg/c+NHgVBuQtxhEiTCUt3wJdouxXOFdyY/naIqnIhIjypcWnPOyRVJ8SGVB9+M03EcjAHu3iwh6S04+GlFKAFsl1pzM5UiDWEUV4SC8VtNOSEIbMVh14AoQ4tTqXLjeT3ikNKSjeAKrpWEN41EWHZg0YMdTZdn1a+7jp9r0RUVFQqKnvDdnOOtRGUMMrfeefWwxJfDbbbaiQzHecJwEtq0uZby9Uc1Vun0CkpZVPqTxZY9Id7lhG6hbrjjzu6opbbbQtailC1kJshC1lKTxVGoR6XCfnyisMR0b7ndp31m5CUpQkEXUpRCRcgAm6ilIJDkP6DbcH+67b7/AJzeX+Zepr/Uy7Rf/SssfwpM/wBWYZf9UrL/APwNT/yZj+V4RLcTaW8tsn2k3FEYegSHQzFrVLW/JpMh4tqdSyHpEaJJjvLS0+WmZ0WK7ITFlOR0OssOOJizOWz/ADTkKTHj5jgCOiYHTCmMPNyoUvuSjvQzIaJAcbDjalsupaeQlxClNgKGHRR6/TK6045Tny4Wd0PNOIU081v724VtqF91W6oJWkqQSlQCiQcJnpl4WcGjBg0YMGjBhYdutlbo3Ko1brdHkU+LFpK3YzCZq3QupVBhmPJkRGQyhzwEsR5cVTj7wAU5LjtstvJEpyK/8m7N8xZ5gZgqNFVT0R8uRm5EwTpLjDj6nWpTzceGluO+Fvrbhvqu+qOwmyQt9JUMINYzFT6I/AjzBIU5UXVNs9w2laUBKmkKcdKnEWQFPIFkBxw3JCDbCQLSpC1IUMKQpSVD0UkkEfQQdMDC9j10YMGjBhYq/spdNv7eUrcaS9BfplRagyn4bC3PlVPhVXwBTZTqnEIadS8uTGZebaPiMOyGeUPteM6w/qzs4zBQ8m0PPE1dPNIr77bEVpmQ6uewp9mU/GVKZVHQ0hD7MN9aCzIfKLJS8ltat0IMPMUCbWJtEZEj0uC2px1a20hhaW1tNud0sOFZLa3m0nfbQFXJQVJBOEd0wcL2NhSaXMrdVptGp6EuT6tPiU2E2taWkLlTX24zCVurIQ2guuJCnFkJQnKlEAHXTDiSJ8yLAiNl2VNksRIzQKUlyRJdSyy2FKISkrcWlN1EJF7kgXOPW862w06+6rdaZbW64qxO622krWqwBJskE2AJNtBh0zfBzuEtCFm67AQVJSooVJvDmTkZ5VctmKTkdjgkZ89T6OzLtGIBMjLKSQLpNUlEpPNJKaapJIOhKVKTcaKIscMM7ScvAkBqpEciIzIB8QFSgfeAeoGOG3E4drx22t9y5KrWbVqsFl9hl9qiya4qWyJLzcdt9TdWoFJZW0JDzLSwy+48lTyFBktpdW2086bF845Eo3y7WVUd6AJbMNxVPnOPutOyAsslbT8WMVIWW1Ju0XFJNipITdQVaNnGkVyZ6DDEtt/ulvJEhlCErS2U74Spt10BQCgbK3QQDYk6FAtRLh147CxbJq+4NxxLZorkNmbLbfe8ee661FZajtlxxbimGZL6ir5raEtMOErWkueGylx1txZVyvVM412Fl6jJYM6cXdxUp0sx2m2WlvPPPOJQ4sNttoUohttxxVglttaiElPqlTi0eC9UJhc7hgJ3g0jfcWpawhCEJJSCpSlAXUpKRqVKABOHAHhCv4HBr1qgjuDKng/UYGps/Uv7QeVSymR++FW/1Fhl/wBUyg/+jVT/AOBG/lmMVzhC3VWs/c9+1agynCVPIrRi8ruAotKblxWVlQQpC+dAW2QsAL50rQhq1vYVnyhy24jqKRNLkdEhL0KpJDO6tx1vcInMwX99KmVFVmSixTurUd4JVIWeKHOaU6ky2QlwtlD0YlVwlCrgsreRukLFrrCrg3SBYlrOobw8MGjBha9geILdDhp3FpO5m1Vfco9cprjaZsJ7xH6JcVMDiVyKJcNOS60ioUyWkFK0c7UmOsiTBkxZbbT6F/LWZqzlKrR6zQ5aosxg7qxqpiUwVJU7EltXAejPboDjZIIIS42pt1CHEtHO+R8tbQqBKy3mmntz6fJBW2rREuBLShaGahT5G6pUWbG31Fp1IUkgqaebdYcdaXdO9nz7TLYzjBplJth2pQ9vN8ERf6q7Z1yay0urSIzXPLnWNPeW2i46b4aFSVRUJarEBrxBLg+AyJjlzcn7V6HnhhmM4tFLr+4oP0t5z1JC0BSlOU15RAktqT65ZUEyWQlaShxtvv153Z62DZl2YSX5LQcrmVO8SY1cYaPeRW1qCW2azHQD6I8lau7EhJVCkHu1pcZdc9Famjt5sEt+ZyO3vAJ8/Xrn9mnFNVoRw0NvMf0fGmEqlJ+bboL9L8uh1Jt/RhbqG2CEdPQfQfo648yfTTRmquTyGp8eNj7MSLT0gJRz018ydfAiw144VektjCT7icdMdT/N/NptSTrbl8X+kYeERNkjQWPxr7ANOemFBp7Y5R06nH7D8MYP7nSG+rU8dBr43ufswvMAe4W91h9px18VAITgd/0k5+HkMDSU6q1/D6z93H24VGxb2D6+fx1xumkAkDyGP3/QTrjUfpuSfjmSQL68b47W0289PeevUAA89beOM9LQx5D4jJ/Zj4a9RN/j4v5nHvAt8fFvIYZtxncdnDBwGbdL3G4j9yKZabEtqb96tow1NVO/r8nQm0OO0uzLVaebnVaQgusIky3FRaPTflDDlUqcBp1txXFOqcSmNB2U6EXB7ttOrjxHEIRcbxGgK9EpJG+oA3woU6jzqu8WYTJcII7x1RKWWArgp12xCQdSEAKWoA7iSQcfnLe1W9sZvt7TC8EUWQzI2u4cbXqbsqxtm6ZUFP8Ay+Q066Il17i1FkNN3LdZjqQmPHQhFCt9HM1R4ipTk6q1CMK1XpNXWEq/AxG1bzUdJv62o7x1X7NyxsNAlA0SkEqUqZMvZZh0FpSkWfmuiz0pSbG2n4JlOvdtXFyLlazqtRAQlMO+kLDlwaMGDRgwaMGDRgwaMGDRgx0Vof67bX/wion+U4ulGj/22pf74wv4y1j0Sv7Gkf4B7/s1Yehxn/2F2r/4W3E/yXtnqy/ap/GHKf7xy/zi5iNdl39gVX/HWf4uMMN1VfEo4cvwpflQd/wcmf5Wompu7PH66dF/xGufmeZhlbQfxXmf4eD/ABtnDxtx7ctbeKn3NZKXmmrqtdUV+K66CiRT5kuE3MhSAG1KW9AltuORXgUPNrbL5S03OaaXGtDnik5e2uxM0ZPacTEzbkyaVQVOqSFocfisvMPAkfhKZUkrESWkWcjSWW3V+q2wX4zosqoZSdplXUC7SawzZ8JBKVJbdWhaLD5smOUl1okEONrWhOql7nBfceo0DhlrVGq0ZcSo06hVuNKYXg8jiI0jCkLSSh1l1JS6w82VNPsrbeaUptaVGPpkCZSuzDVqbUY7sSfAfMSZFeTuusSGNoEVt1tY1F0qSQCCUqFlJKkkEr7T7MraZEkx3EusPo71l1Bulba6C6pKgfEHgdQbggEEYjf1THExY6qx6zDt28bYrtQaU7BpVcps6WlCPFcTHjym1uutN8yPEfYQC8wjnRzutoTzozzBWoFQapNdotVfZMhimVWnVB6ON277UOWzIcaG/wCpdxDZQN71bnXS+OSfHXLgzIraw25JiyGEOG9kLeaW2lemvqlQNxqLXGuJJ7wtWJvHSqJXrQ3HqtDZbS81GkUKepymyHnERi5HkR2JEMSJcJLZC4SpUd5hUl1MgNFwo1e3N2V6XtpbpteybtBXCepkQtojwnVvMtuOOrfbcnwmJcadTJ5UvulvOtlZabaCWlBCVKg+k1OVk1cmBWKCHkSXt5TjyAlakpQEKSw8404zJYsN4JQoJC1LJUCogM534oO7lB+40K/q6q5bfiuOs0GrMMMMsF1bZIbqIZisPfdZcdpaiZr88rDcoQ58ptuQ5qrm1eh7S6C7SIOfKo/WYTCJDVDnplrmQXAO6MlLbrjTMgSd3uO9ExpMkthuxU0lJxJ2VpuW56Zb9CjNw31ltU1juQy8kne7sqSlS2+7J390sqLe9vaBRIw3TUP4duHNcJn5Vz/gzWf8bA1OHZ3/AF0KV+99Z/N72GVtA/FmV/h4v/bJxzXEr+Wm7v72tL9Src02tsf65+dP35d/7FnCjk/8WaN/iaf/AJ14dJeX9qk1/gZt/wD4i0tTZXP963l3/GWv9KarhmQ/1z5/+CV+ao2I5tVIxK+O82s/Kdt1/h1aX+X6fpy5M/HDKf5S0L86RcJ1Y/tTVP3um/xZ3Dg+Mb/XlZ3+CB/y5VtTZ2ofx/pn5LQfzlV8MzZn/aGT++j/APFoeGgardiRMGjBjrrDtZ+9bwoFrxwvNVnpbfU2QlxqCw25LqLyFKQ4lKmIEeS6lSm1pCkAlCh80r2V6BJzRmGj5fh3D9VnMxQsJK+5aUd6RIKRxTHjpdfXcgbrZuQLkcNTnN0ynzJ7vzIrC3bXtvqAs22D1ccKUDjqoaHEn02+Ils33ae28OjPIo8qiyGHpUCFJ+5tGebQ2ijQnHgXkMRnY0OfGTGWr5TGLlGkKk/I5DxVfCVtBpmT9oeTtmFNZQiht0dulzCQFKhTpTTXyC33hQFOOBuO2mYSotvCsd8sF9iwgxqgSqvl+r5lkrUqcuYqUzY275htS/TlboNkoKnVFoABSDE3B6i8MC4gbHcsrcWqlpgN0m4HHK1THG0cjPNJVzVGKEpSllp2PNU44IzXzWocmC4lDLb7bSagbX8nnJeeqtT2WO4pk5fyvRwkWa+T5y3Fd01YmyIcpEmEEn1gI4OqVJUqW8pVf5ZokWQte/JYHokwn53fspSN9Xi82W3iRoS4RoQQEQ1GGHNhfeGX8sVuf3tWv8ky9SxsO/XRyr/jE382TMNXO34sVX/BsfxpjDmN5bK3vrV8Kqm38+dFof3Op7TaY9fap7YmNl5MhfyZb6eXopvmWUDmAz1AyZo2pZR2wVXaBWp2VPl8UJ80v5PXDrogw0lqkU9qSURzUGO6AmNyC5dpO+5vL9be3izcsVbKMagw2Kr8nmaj0rvw9B794hUt9bYU4I6yq7Skbo3zZNk6WsPbiJmJpmy1Not0z4su7JTdFYbVztF+VVYcmnv1OZHZAS74IjNTUuPJb8JhMlmO894slgSFjbrIcp+yPK9EzNNYl5sdlUZa7LS489LgU+Q3VZ6DopTSC+I70kJSl16UiyQHClHHkdsSM2VObTWVtUpDUwD1SlCGpD7aorCtdFq7suIb1KUtKufVBMdGqT4mjBowYNGDBowYkQ4SPyc17/Cqv/5Es/Vuuzb+Km0//F6f+bK/iKNov9tMsf4WR/GIGI95f/pcr++Hv8YrVRcSsngPIfVjH0Y/cGjBiRfcv+1Wpv8Aglt9/wBttXVtdpH+902dfvjQPzPmLEU5c/XCzD/is7+OU7EdGqlYlbHcbZflGsP/AAwtz/K8TTkyb+N+Vfykof50i4Tqx/amqfvdN/izuJJ91rBuq8n6K7bt+TbQTBamiS1Fb5xNElMAMFZFWppBYVGfIHI6nEglKkqKxq9m1XZ3nzOdXps3KWaWaDEiU9UWVHcqdbgF6SZTzwfDdLhyWnB3S0N77qkuepuhJSAcQdlfMNCo0WSzVaWqe69IDrTgjQnwhsNIQUb0p1CkneSVWSCnW/G+GXb3WPfNlQKQa/fsq7aVU5LrSGJEpxpxmZHQHQpVPNRnofZ8MkpkpcJYdw282yXo65FUdqmSc9ZJbo7GbczJr0eqLluwm2qtVp7bL0IModW4xVGIxbXuS0Bt1ttYUFOI30kKBlPK9aolZVMXSaaYDkZLSXlKiRWFOIeKygJXGW4FAKaVvJUoEEJNjoQ3HUO4d+HEcLn5W6Z/wXVf8U3qa+z5+ujRf8Uq/wCbpGGZn/8AFib/AIWJ/GW8PF3A2urN03XUK3A3suazY0qNSGE27Tqc2/DguU+i0+mvutO/f9ReY1B+I5UXc0yMUuy3EqL6gZD1h87bGM9ZmzRV67Sc9OUqnVB1lyLTw/WkCMhuJHYWgIir9HAU60tz8HYHfuRvFWI/o2caJTaZEgyqEmU/HQtLkgohqLpU6tYJLqS4bJUlPrEnTTS2GQbtUq67Ou92jStw6pdCUwIsmLVpVSfhyXIzqn0hp6Ims1VuOpp9t/CGqhJQUKQ4VNuLWy3U7aNl6vZPzO/Qq1XvlubGixHfTETZb5SzJbL7bDgmKD7DiN9Siyq43XEupJS6DiVMvT4FWprc6HA9CZdddT3KmGkXW2rcUtJZBQsK3QN8a3SUHVGER1H2F/BowYNGDGZT6jPpM6JU6VOmUypQJDUuDUKfJehzoUphYcZkxJcdbb8d9pYC2nmXEONqAUlQIB15IWttSVoUpC0KCkLQopUlSTdKkqBBSoEAggggi4OPBbaHULbdQhxtxJQttaQtC0KBCkrSoFKkqBIKSCCCQRbE5vB77e3ih4eolJtPeCmQuIyxKethhmRcdSXQ9x6dTkYbUxHvJiHOarXgN5Wz98lLqE51Q8FyrttFssSrl/a7mOktNxKlu1yG36qDKcU3PbRw3EzQlZcSBfd9JafWNEpWlACRC2ZtheU6y+5Oo+/luc5dS0wmkO0x1w/+MXT1KbDSr8RDejNqJUpTanFFZszcOnt3vZ2bwx6YxXN1J2yVwS0MpkUTeOjO25FhyVIy62u6qc9WbREdC+ZLct+txkLQEqdSwtXhpkiNtKytVEJ3pTtOdUmymp7RQErNiQH2S8ypFyQlaltkgXUhHARXL2S5yo6lBENiqso0Q9TXwsrQCbEx3wy+le7qpCUOAH1ULc0UZn9r95NpN0KVHru226G3u4FGkNIcYqll3lbtz091LgCkFEui1GaweYdhz56EEApICh6dBmp34cyLLbP7ONIafSL34ltarEXFwbEHCd6BPgWbnQZkJYuCmXFejq9geQgkG2hFwRwOHCU59lQQoOtkHByFpI6hPYg4xnSa/wDsvEC3sBv9eFFgcfb9n3Yy61fFl2ZTH6zd922xatHhILsyq3HXqVRKbEbSCpTkibU5cWMwhKUklbrqUhIJJAGkeS420lSnXG2kixKnFpQkADW6lEAAanUjhfCzHZefV3bLLrzij6qGm1uLVf8AaoQkqPDkDxxGVvv7dj2XnD2mcxWuJ609yK3BDwVbuyCHt15zz7KuRURurWx49pMyS4C3yTbkiJbUFF5bSUqUGzLzHR45P9eIeUAfVjAvXIPJafwWt9Dv2044d0LKdflgWgLjpUQd+WoRwAdCVIWC8LcbBskg6A2xW/4y/wCFf7xXs3cNocFOz9O2foclD8Gmbr7pqgXduN8ncHKKpTbKhmRZFtVFI5vAZqlQvyMj5rq0c/4JDUnZxkObyIDCY6DcB57dcet+2CLd0hXgS6ByJNiHvTcgxWtxypyFSlghSo7G80xf9qpwkPOJvzSGSbaixIxVV3d3p3a39veqbk71bi3fuhfVZcU5ULnvSuTq7VHUqcW6IzLsx1xMKCytxfyanQkR4ERCvDjRmWwEhoPPvSXFOvuredV85biipR9pJ06DgMPyPGYitJYjMtsMo+a20hKED2JAFzzPE8zhMderHuwaMGDRgwaMGDRgwaMGDRgwaMGN7azzUe57ckPuJaYYr1IeedWeVDbTVQjrccWT0CUISVKPkATrupbiGqnTnXFBDbU6I44tRAShCJDalKUToAlIJJOgAvj0yQVR30pBUpTLoSkC5JKFAADmSdAOuHj8ZFTgSIW2dOYlsuTosm9ajIipV+GagVSBYTNOmLR0IjzXaXUW4zoyh1UOQEEltWLH9p6oQJ2YcrehTIszu6A644Yr7T4QiROdcYKy0pYT3rY7xsEgqQQsDdUkmOtmcd9in1QvMus709CUh1CkElthKXAAoAncUd1VuCgUnUEYY3qsWJLw5DhYlR4+6SG33kNuTKHNjRW1H8JJkJn0uUpllAypxxMWLJkqSkEpYjvOnCG1ETNsBlxIW1ChOTJUeI05GrEdDsl1DDSn36TMQwyHHFJQHHnClplBUC66pDSApxaEqZ2fGXXsszkstuOqS5EcKW0KWoIRKaUte6kFW6hIKlECyUgqNgCRttxtx5tj8QlbuagyRKairpcGqQkOlLE+O1S4TM6nyOigl5h1CvDUpClRJrKFlPM2pB7M5Z1mZW215hzPl6Y1I9GqyG3Ay+l2HUIyIkWPOgvLaK23GVqbcZWRvFl5tLrZS8yhSfVR6M1U8mQKZUGVN95FUpO+gpdjuF51xl9AVYpWkKSsXsFoUUqBQtQLptzL1ty5djLhuOmT2lU2r0WUxGdcSWVKmymXYqITjeV8kz5WsRnGOdam3gtClKDa1iw21HOuXM1bEa1VaXUYoTV26K2xBekR0VBuc3XqVJk092MlxSzMitMPuvIQFJLDSpKFKYKXDH2WKNUaXnWHFlR3SYipilvobcVHUwqDKbafS4UhIacWtCUlRBC1htQC7jEWeqD4nfHT2Wm2lXVQ03h4v3tGe0Kx4Sn0KMXCspUqKpElLSnORL6o623wyVlpxtfK4lZy8KKquUpOY1SUUIzo4qqolzITCKx3xa3QVXCfnFALgTvFsFe6Dx1AzBBlGnhtU4MOGKHbd2Xt07gVcgWvw3iE3tvEJvh9DHD7Z1bkNXVtLe9Utymyghz+oFSmVCChphaVPMR5in5E0FpQQX2p1QmKiSQpD7KThlu1KNieQq/JbrmzzaW3SI3quhhp9ipSIRI+al75VplRhrGpDM1Cn9dV2FzF6s512AhULMGWzLd1TvqQuO28OpHo0mO8DcArZIR/xb45nijvOgItKibdx6sqtXFAqNNl1NxS23nYrdPgTozj1UKVJVGq0p2QytlhTSXFRHpjziGWn4qpCX2gc20J7LmWskU+tNZlqdJlsS6pVm32ZSkqg056AlMiQwpxlUycuU4/JaQ6tbKmEh6616dGQqTORUalW5ENdOjSmnGo0VSFtgh6Ql8lDawlYaYS2lDS1JAWHFbmg1YtqqOJUwqmzF7xdv8AcCkV+oc4ppTIp1RcQlayzFnIDZfU23la2mHksuvhtDzqWUOOMR5D6GmHH5szzWzkrOtFr8pDjkGO85HnpaSFPJhTWVxZDrSCQFuMId79Le8nvS33W+jf3ghZkpS6zRpkBpSUvOIS4wV/MLzKw62lRFylKyncKgDuhW9uqtul696bH2PvFV2r+plySQ1VI8RuVKpb0eVEnogs/JWltPGLKSZEeJGRDUlDhQy3ADCmkrZdUm0+atlGRdqVXczrRc9QoTVRbjOVNuGmDPaccZYbZ71QXUIbtMlKZabElqSy4oPAuLaStakqjCl5preWIiaNMobzy45cTFLpeYUkLWpe7cMPIkNha1FtTakgoslKikA44DiJvy1qFYsLaW2p5mzIqKPTZrTTqX/uZS6A20wxEqb5WXW57ZgQI7cJ1CpCmkOSJS2CzHE5kbaM35SpmS6LsuyfNjVRqnriGdLhyGpcaOzAStYbXKYKmJFQnTXlS5fo6ihhaHUrSlbqUNrWTaRVpNZmZnq7LkZUgPdw06hTTji3yElSWlgLbjsMpDTW+N5YKSCQkqUxPVU8SljtdtpMeFuLYUyW83HixLzteTJkPLCGmI7FbguvPOrUQlDbbaVLWpRASlJJOBpfypIYiZpy1KkuoYjRq/RpEh91QQ2ywzUYzjrrijolDbaVLWo6BIJPDHBVELdplRbbSpbjkCWhCEi6lrXHcSlKQNSpSiAAOJNsSUbqbBMbr1Oj1maq5YZp1HTTmUwYUhDTrSpkqd4x8WhzSoqMrlSpLiUlKRhOcqN1tpmzjKm0quxa6vaLR6QY9LYpqY7S6VPQ6lqTLkpfDyq1DKd/0rd7sNqACAoLO9uphrLeY6rlyC7BGXpksOSlyS4oSo5SVttNFBQIT17d1fe3gfWI3dLlmG+mx8DaWJRZMeq1Fx6qyHWBTaux4UpxpppTjk2MpUKmqWxHWGmX0pjPJQuUwVvtFbbb1Ztp2zajZCZpLtKzrTs0LqLkpt+LHbitSYiWEtKbkKTFqVRCmHStbZU76OUuIAb78Kc7mSMtZjmV1ctMqjSKYI6WlIccU6pt0uFYLYLsaOQtASFWTv3BO9uWTvtx1EeHZh5/ChT6LTIt737VJsVpVGiphLSSpb1Pp6EIqMubLaSnLcWS43HEVaVFbztNloHJ4OHbL9nROXqS/m/Olbmw2XMu0pDcZl51lElDMtL7kuTGQ8pJU64iMintKb9ZSpi2CR3wSuN9oRny0UmjQmXlpqEolxaErLZW0UJabcUkEBILin1BXAMhwA7ht9H+MuuF575LZlOTFLrnydD9XkrdSwVqLSXi3EQ2t0IKQ4pCEoUvJSlIwB0K7UmaSpRTlvL4TvEpClT1KCb+qCsSE3IFgSAAeQA0x4DZjSwADUZ501sGACeendmwJ5XPmcdNvnJt/c3ZOibkNyY8GpU0wH2GeZK/lEuoSYlNq1vsuFaQ47GcednZT4kgN0Z4KjNoVKciq22Cq0TaDssytnpubTYtchy0RJFORKbVILksKZqVObZKjIC40iO1PZS6kAQC48VK71pS+PKMWbQMz1ShqZkuwnmi83ILag3ZkhUeQpVg2Q62tbCykn8OEti26oBg2qlYlfC7cNsqPE3ftlyS8hlC26swhTisBbz1LloabT5la1EJSkdSdSlsWkx4m03Kr0p9mM16XIa7191DLYcfgymWUFxxSUBTrq0NoBIKlrSkXKgC2M5NuO5aqqGm1uL7ltW42lS1bqH2lrISkEkJQlSlEDRIJOgw4jc/iEuCxt3maNGqEaoWXTWqaiu0aPRaCai29IS4ucG6uqAxWVzI7TrMluG/WERlOBEN5TDWQzLeeNsuYcqbXap8nVdVXyrAkU5lyiNyWl091pVIgCpNMOoQ4GpbUxUoh26yzLQpLiFJC2y06Jk6n1TKcb0iIIlUkIkLTNU2tMhKhKfMZS0KUnfaUyGhu+qFtEKSQSFY0fE5QaBc9s0Ddaj1dKkyGIkJlpx1ww6rGlOczbdPKvmCpxlLffdYSlLrkOLUPlBQqmIYTz7f4OV8wUuhbSqBXY8h2rKjUx6mmUhx99pMZ91EluL3y3IT1P7gQ6nHDYbS89HUsofUsyfZkJ+pwJM7Lk+C42mKHJKJPdFLaFFxCFNKd7tKXkSN/vozhUVFCHAm6AA2x3VWsSfg0YMGjBg0YMSFcHsqNMtC4aHHWuTVGbgqVQkQIrT0iW3An0u3IsaWWmWnVhh1+nzGfFCFIbcZ5XOUuNBy2XZrqNHapOfqVUavT6Y9Uk0pDImy40Za2VRaxGeeZRJdZD4YVIb7xKFeqVthZSFpJivaMxLVKoMqPEkSURlSlLLLTjqUrDkNxCFltCyguBtW6VDUJVu33SB7yODOlrU88Z14JKy46SqKvkSVFSiSBbAPKCevUHA7+evE9nbKP91qljziUmw8z+mUe3hgG0KraD9KcrkNHpf+rT9uGH3PSI9AuGs0WLUmavGplQkw2KlHSlDcxplwoQ94aHZDbThACX2mpMplp9LjbMqS0lD7lX6tCaptUqVOZmxqkzAny4bVRhq3ok9uNIcZRMiqN96PJSgPMqubtrSbniZNivKkxY8hbLkdb7DTyo7ws6wpxtK1MujSzjZJQsWHrJOgxotJ+PfiQPcutUr+hboDHy5gu1K37Jp0FAUSZU6A/QZE2K0QCFOxmafMceTkcgjrCiFFIVajaHU6c/2fNnURidDelCo0m8ZqQy5IT6HTK8zK3mUrLiPR3XWmnt5I7tbraVWK0gxfl+NIbz9mF1bDqGvRZQDi21pQe9lQFtWWQEnvEIUpFid5KVEXCTaPzVV8ShjstupDES/7JlSnmo0aPdlvPPyJDiGWGGW6rEU48884pLbTTaQVuOuKS22gFa1JSCQvZVkMRM0ZblSXUMRo1eo8iQ+6oJbZYZqEdx11xR0ShttKlrUdAkE8scNUbW7TKi02lS3HIMtttCRdS1rjuJSlIGpUpRAA5k4dhxXXdV6TWLSg0K46vSXhCqkqbEptTn01wtSE0huHIkMR3mFKQ4uPMbYdcSQVsyW0K5m3UiwfaWr29muhNUms7yWqAFSE02obyELeny3Gi76K8UBbjJQ4jf8AWUyptYuhaCWBs3g2pc5cqJbfnWbVIYsVBDLaV7neIuUpXdKraBYUk+sFAMxqlcrdbW05WqxVauthKksLqlQlz1spWQVpaVLedLaVEAqCCAogEgkDVZpEuVLUlUqTIkqQCEqkPOPKSCbkJLilEAnUgWBOJJbaaaBDTbbYJuQ2hKAT1ISBc+eNXrnx7MOB4Y5LEbdujB91LapMKoxY6DkuSJLzSfCjsNpBW6+5yq5Gm0qWrlOEnB1L+wmoQaZtLokiozIsCMpmpMekTH2o0cOvQH0MoW88pDSFOuENthShvuKShN1qSC0s8R35OW5zcdlx9wKjud20hTiyht9tS1BCAVEJSCpVgbJBUbAEh524fD9XL5u2oXM1cN6UVE6NR46adAYqzMVn7lUWn0guIbTT1BKpRgmW7hRy6+skk9TPuddkOW845oq2ZTtTplMNVdZd9BQmmykx+5iR4u6mR+mKN3gV3HeX7hu2/u2O7vFh0bNtRo9MiU0ZXlSRFStPfkyWi5vuuO3Lfyc7u239356r2vpewQi5OF2jwKkUVzdH7jz32G5CmLkiMKqT7ZK2kSuepVSiyHGFeCWW1fJXGwphxKZCylTbUSV/YtQaZNQwxtYyg6lyOh9Sqi83Ekb6nHUEBuJNqTam91tJStT6HCorBaSlKFuOyBnKfKZU4vKtXSUuKQBHQp1uwShWqnWYygq6jdIbKQLELJJCf//Z";


			//Attachment logoImageAtt = new Attachment(logoPath);

			//emailMessage.Attachments.Add(logoImageAtt);

			//logoImageAtt.ContentDisposition.Inline = true;
			//logoImageAtt.ContentDisposition.DispositionType = DispositionTypeNames.Inline;



			//var template = File.ReadAllText(path);

			//string logoImgId = "headerimg1";
			//logoImageAtt.ContentId = logoImgId;

			//this model is passed to the view (email)

			var body = new StringBuilder(EmailTemplates.GroupApplicationReferenceCode)
				//.Replace("#LOGO#", imgLogo)
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

		public async Task SendGroupApplicationsCompletedBatch(IEnumerable<GroupApplication> applications)
		{
			// Send email to admin

			var emailSettings = this.emailSettings.GroupApplicationsCompletedBatch;

			var body = new StringBuilder(EmailTemplates.GroupApplicationsCompletedBatch);
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
