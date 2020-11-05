using System;
using System.Net.Mail;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using InLife.Store.Core.Models;
using InLife.Store.Core.Settings;
using InLife.Store.Core.Services;
using System.Net;
using System.IO;

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

		public async Task SendAsync(MailAddress sender, MailAddressCollection recipients, string subject, string body)
		{
			MailMessage mail = new MailMessage()
			{
				From = sender,
				Subject = subject,
				Body = body,
				IsBodyHtml = true
			};

			foreach (var recipient in recipients)
			{
				mail.To.Add(recipient);
			}

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
			var body = this.LoadEmailTemplate(emailSettings.TemplateFile);

			body = body
				.Replace("#ID#", model.Id.ToString())
				.Replace("#TITLE#", model.Title)
				.Replace("#DETAIL#", model.Detail)
				.Replace("#SOURCE#", model.Source);

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
			var emailSettings = this.emailSettings.SendPassword;
			var body = this.LoadEmailTemplate(emailSettings.TemplateFile);

			body = body
				.Replace("#RECIPIENT-NAME#", recipient.DisplayName)
				.Replace("#PASSWORD#", password);

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

		#endregion

		#region Business

		public async Task SendQuoteRequestAsync(Quote model)
		{
			// Send email to admin

			var emailSettings = this.emailSettings.QuoteRequestAdmin;
			var body = this.LoadEmailTemplate(emailSettings.TemplateFile);

			//body = body
			//	.Replace("#REQUEST-ID#", model.Id.ToString())
			//	.Replace("#PLAN-CODE#", model.PlanCode)
			//	.Replace("#PLAN-NAME#", model.PlanName)
			//	.Replace("#PLAN-FACE-AMOUNT#", model.PlanFaceAmount.ToString())
			//	.Replace("#PLAN-PREMIUM#", model.PlanPremium.ToString())
			//	.Replace("#OWNER-NAME-PREFIX#", model.Owner.NamePrefix)
			//	.Replace("#OWNER-NAME-SUFFIX#", model.Owner.NameSuffix)
			//	.Replace("#OWNER-NAME-FNAME#", model.Owner.FirstName)
			//	.Replace("#OWNER-NAME-MNAME#", model.Owner.MiddleName)
			//	.Replace("#OWNER-NAME-LNAME#", model.Owner.LastName);

			var adminSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
			var adminRecipients = new MailAddressCollection();
			var adminSubject = emailSettings.Subject;

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

			// Send email to user

			emailSettings = this.emailSettings.QuoteRequest;
			body = this.LoadEmailTemplate(emailSettings.TemplateFile);

			//body = body
			//	.Replace("#REQUEST-ID#", model.Id.ToString())
			//	.Replace("#PLAN-CODE#", model.PlanCode)
			//	.Replace("#PLAN-NAME#", model.PlanName)
			//	.Replace("#PLAN-FACE-AMOUNT#", model.PlanFaceAmount.ToString())
			//	.Replace("#PLAN-PREMIUM#", model.PlanPremium.ToString())
			//	.Replace("#OWNER-NAME-PREFIX#", model.Owner.NamePrefix)
			//	.Replace("#OWNER-NAME-SUFFIX#", model.Owner.NameSuffix)
			//	.Replace("#OWNER-NAME-FNAME#", model.Owner.FirstName)
			//	.Replace("#OWNER-NAME-MNAME#", model.Owner.MiddleName)
			//	.Replace("#OWNER-NAME-LNAME#", model.Owner.LastName);

			var userSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
			var userRecipients = new MailAddressCollection();
			var userSubject = emailSettings.Subject;
			var userEmail = model.Customer.EmailAddress;
			var userFullName = $"{model.Customer.NamePrefix} {model.Customer.FirstName} {model.Customer.MiddleName} {model.Customer.LastName} {model.Customer.NameSuffix}";

			userRecipients.Add(new MailAddress(userEmail, userFullName));

			await this.SendAsync
			(
				userSender,
				userRecipients,
				userSubject,
				body
			);
		}
		
		public async Task SendOrderConfirmationAsync(Order model)
		{
			// Send email to admin

			var emailSettings = this.emailSettings.OrderConfirmationAdmin;
			var body = this.LoadEmailTemplate(emailSettings.TemplateFile);

			body = body
				.Replace("#REQUEST-ID#", model.Id.ToString())
				.Replace("#PLAN-CODE#", model.PlanCode)
				.Replace("#PLAN-NAME#", model.PlanName)
				.Replace("#PLAN-FACE-AMOUNT#", model.PlanFaceAmount.ToString())
				.Replace("#PLAN-PREMIUM#", model.PlanPremium.ToString())
				.Replace("#OWNER-NAME-PREFIX#", model.Owner.NamePrefix)
				.Replace("#OWNER-NAME-SUFFIX#", model.Owner.NameSuffix)
				.Replace("#OWNER-NAME-FNAME#", model.Owner.FirstName)
				.Replace("#OWNER-NAME-MNAME#", model.Owner.MiddleName)
				.Replace("#OWNER-NAME-LNAME#", model.Owner.LastName);

			var adminSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
			var adminRecipients = new MailAddressCollection();
			var adminSubject = emailSettings.Subject;

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

			// Send email to user

			emailSettings = this.emailSettings.OrderConfirmation;
			body = this.LoadEmailTemplate(emailSettings.TemplateFile);

			body = body
				.Replace("#REQUEST-ID#", model.Id.ToString())
				.Replace("#PLAN-CODE#", model.PlanCode)
				.Replace("#PLAN-NAME#", model.PlanName)
				.Replace("#PLAN-FACE-AMOUNT#", model.PlanFaceAmount.ToString())
				.Replace("#PLAN-PREMIUM#", model.PlanPremium.ToString())
				.Replace("#OWNER-NAME-PREFIX#", model.Owner.NamePrefix)
				.Replace("#OWNER-NAME-SUFFIX#", model.Owner.NameSuffix)
				.Replace("#OWNER-NAME-FNAME#", model.Owner.FirstName)
				.Replace("#OWNER-NAME-MNAME#", model.Owner.MiddleName)
				.Replace("#OWNER-NAME-LNAME#", model.Owner.LastName);

			var userSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
			var userRecipients = new MailAddressCollection();
			var userSubject = emailSettings.Subject;
			var userEmail = model.Owner.EmailAddress;
			var userFullName = $"{model.Owner.NamePrefix} {model.Owner.FirstName} {model.Owner.MiddleName} {model.Owner.LastName} {model.Owner.NameSuffix}";

			userRecipients.Add(new MailAddress(userEmail, userFullName));

			await this.SendAsync
			(
				userSender,
				userRecipients,
				userSubject,
				body
			);
		}

		public async Task SendPaymentReminderAsync(Order model)
		{
			// Send email to admin

			var emailSettings = this.emailSettings.PaymentReminderAdmin;
			var body = this.LoadEmailTemplate(emailSettings.TemplateFile);

			body = body
				.Replace("#REQUEST-ID#", model.Id.ToString())
				.Replace("#PLAN-CODE#", model.PlanCode)
				.Replace("#PLAN-NAME#", model.PlanName)
				.Replace("#PLAN-FACE-AMOUNT#", model.PlanFaceAmount.ToString())
				.Replace("#PLAN-PREMIUM#", model.PlanPremium.ToString())
				.Replace("#OWNER-NAME-PREFIX#", model.Owner.NamePrefix)
				.Replace("#OWNER-NAME-SUFFIX#", model.Owner.NameSuffix)
				.Replace("#OWNER-NAME-FNAME#", model.Owner.FirstName)
				.Replace("#OWNER-NAME-MNAME#", model.Owner.MiddleName)
				.Replace("#OWNER-NAME-LNAME#", model.Owner.LastName);

			var adminSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
			var adminRecipients = new MailAddressCollection();
			var adminSubject = emailSettings.Subject;

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

			// Send email to user

			emailSettings = this.emailSettings.PaymentReminder;
			body = this.LoadEmailTemplate(emailSettings.TemplateFile);

			body = body
				.Replace("#REQUEST-ID#", model.Id.ToString())
				.Replace("#PLAN-CODE#", model.PlanCode)
				.Replace("#PLAN-NAME#", model.PlanName)
				.Replace("#PLAN-FACE-AMOUNT#", model.PlanFaceAmount.ToString())
				.Replace("#PLAN-PREMIUM#", model.PlanPremium.ToString())
				.Replace("#OWNER-NAME-PREFIX#", model.Owner.NamePrefix)
				.Replace("#OWNER-NAME-SUFFIX#", model.Owner.NameSuffix)
				.Replace("#OWNER-NAME-FNAME#", model.Owner.FirstName)
				.Replace("#OWNER-NAME-MNAME#", model.Owner.MiddleName)
				.Replace("#OWNER-NAME-LNAME#", model.Owner.LastName);

			var userSender = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName);
			var userRecipients = new MailAddressCollection();
			var userSubject = emailSettings.Subject;
			var userEmail = model.Owner.EmailAddress;
			var userFullName = $"{model.Owner.NamePrefix} {model.Owner.FirstName} {model.Owner.MiddleName} {model.Owner.LastName} {model.Owner.NameSuffix}";

			userRecipients.Add(new MailAddress(userEmail, userFullName));

			await this.SendAsync
			(
				userSender,
				userRecipients,
				userSubject,
				body
			);
		}

		#endregion

		private string LoadEmailTemplate(string filename)
		{
			var directory = $"EmailTemplates"; // TODO: Retrieve this from appsettings
			var path = Path.Combine(this.hostingEnvironment.ContentRootPath, directory, filename);

			if (!System.IO.File.Exists(path))
				throw new FileNotFoundException("Couldn't find EmailTemplate file: {path}");

			string body;
			try
			{
				using var reader = System.IO.File.OpenText(path);
				body = reader.ReadToEnd();
			}
			catch (Exception ex)
			{
				throw new FileLoadException("Couldn't load EmailTemplate file: {path}", ex);
			}

			return body;
		}
	}
}
