using System;

namespace InLife.Store.Core.Models
{
	public sealed class FeedbackRating : Enumeration<int>
	{
		public static FeedbackRating Poor  = new FeedbackRating(1, "Poor");
		public static FeedbackRating Fair  = new FeedbackRating(2, "Fair");
		public static FeedbackRating Good  = new FeedbackRating(3, "Good");
		public static FeedbackRating Great = new FeedbackRating(4, "Great");

		public FeedbackRating() { }

		private FeedbackRating(int id, string name) : base(id, name) { }

		public static FeedbackRating FromId(int id)
		{
			return Enumeration<int>.FromId<FeedbackRating>(id);
		}

		public static FeedbackRating FromName(string name)
		{
			return Enumeration<int>.FromName<FeedbackRating>(name);
		}
	}
}
