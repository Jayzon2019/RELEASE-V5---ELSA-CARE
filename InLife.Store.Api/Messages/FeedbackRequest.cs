using System;

namespace InLife.Store.Api.Messages
{
	public class FeedbackRequest
	{

		public string RefId { get; set; }

		public string FeedbackType { get; set; }

		public string Comment { get; set; }

	}
}
