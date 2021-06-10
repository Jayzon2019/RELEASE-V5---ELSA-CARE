using System;
using System.IO;
using System.Threading.Tasks;
using InLife.Store.Core.Models;

namespace InLife.Store.Core.Business
{
	public interface IGroupApplicationProcessing
	{
		GroupApplication GetApplication(string refcode);

		Task<GroupApplication> RequestQuote(GroupQuoteForm form, string url);
		Task<GroupApplication> UpdateQuote(string refcode, GroupQuoteForm form);
		Task<GroupApplication> SaveApplication(string refcode, GroupApplicationForm form);

		Task<GroupApplication> UploadFile(string refcode, string documentType, string contentType, string filename, Stream stream);

		Task<GroupApplication> SetPaymentMode(string refcode, PaymentMode mode);

		Task<GroupApplication> Feedback(string refcode, GroupFeedbackForm form);

		Task<GroupApplication> Cancel(string refcode, GroupCancelForm form);

		// Temporary OTP Solution
		Task<bool> RequestOtp(string refcode);
		GroupApplication RequestSession(string refcode, string otp);
		bool VerifySession(string refcode, string session);

		Task ProcessCompletedApplications();
	}
}
