using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Mail;

using InLife.Store.Core.Models;

namespace InLife.Store.Core.Services
{
	public interface IEmailService
	{
		#region General

		Task SendAsync(MailAddress sender, MailAddressCollection recipients, string subject, string body);

		Task SendErrorNotificationAsync(ErrorLog errorLog);

		#endregion

		#region Identity

		Task SendPasswordAsync(MailAddress recipient, string password);

		#endregion

		#region Business

		Task SendQuoteRequestAsync(Quote quote);

		Task SendOrderConfirmationAsync(Order order);

		#endregion
	}
}
