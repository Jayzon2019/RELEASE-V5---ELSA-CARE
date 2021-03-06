using System;
using System.Collections;
using System.Collections.Generic;

namespace InLife.Store.Core.Settings
{
	public class EmailSettings
	{
		public EmailDetails ErrorNotification { get; set; }

		public EmailDetails SendPassword { get; set; }

		public EmailDetails QuoteRequestAdmin { get; set; }
		public EmailDetails QuoteRequest { get; set; }
		public EmailDetails OrderConfirmationAdmin { get; set; }
		public EmailDetails OrderConfirmation { get; set; }
		public EmailDetails PaymentReminderAdmin { get; set; }
		public EmailDetails PaymentReminder { get; set; }

		public EmailDetails GroupApplicationReferenceCode { get; set; }
		public EmailDetails GroupApplicationOtp { get; set; }
		public EmailDetails GroupApplicationCancel { get; set; }
		public EmailDetails GroupApplicationThankYou { get; set; }
		public EmailDetails GroupApplicationFeedbackAdmin { get; set; }
		public EmailDetails GroupApplicationCompleteAdmin { get; set; }

		public EmailDetails GroupApplicationPaymentProof { get; set; }

		public EmailDetails GroupApplicationsCompletedBatch { get; set; }
	}

	public class EmailDetails
	{
		public string SenderName { get; set; }
		public string SenderEmail { get; set; }
		public string[] Recipients { get; set; }
		public string Subject { get; set; }
		public string TemplateFile { get; set; }
	}
}
