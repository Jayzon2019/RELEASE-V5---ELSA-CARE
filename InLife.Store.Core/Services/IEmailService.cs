using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Collections.ObjectModel;
using InLife.Store.Core.Models;
using System.IO;

namespace InLife.Store.Core.Services
{
	public interface IEmailService
	{
		#region General

		Task SendAsync(MailAddress sender, MailAddressCollection recipients, string subject, string body, Collection<Attachment> attachments = null);

		Task SendErrorNotificationAsync(ErrorLog errorLog);

		#endregion

		#region Identity

		Task SendPasswordAsync(MailAddress recipient, string password);

		#endregion

		#region Business - General

		//Task SendQuoteRequestAsync(Quote quote);

		//Task SendOrderConfirmationAsync(Order order);

		#endregion

		#region Business - Group

		Task SendGroupApplicationReferenceCode(GroupApplication application);

		Task SendGroupApplicationOtp(GroupApplication application);

		Task SendGroupApplicationCancel(GroupApplication application);

		Task SendGroupApplicationThankYou(GroupApplication application, string[] benefits);

		Task SendGroupApplicationFeedback(GroupApplication application);

		Task SendGroupApplicationPaymentProof(GroupApplication application, string contentType, Stream stream);

		Task SendGroupApplicationsCompletedBatch(IEnumerable<GroupApplication> applications);

		#endregion
	}
}
