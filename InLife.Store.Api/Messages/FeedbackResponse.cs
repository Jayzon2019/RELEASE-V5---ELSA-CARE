using System;

namespace InLife.Store.Api.Messages
{
	public class FeedbackResponse
	{
		public int Id { get; set; }

		public string RefId { get; set; }

		public string FeedbackType { get; set; }

		public string Comment { get; set; }

	}
}
