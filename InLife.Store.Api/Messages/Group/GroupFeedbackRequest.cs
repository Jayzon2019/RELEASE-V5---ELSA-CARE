using System;
using System.ComponentModel.DataAnnotations;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class GroupFeedbackRequest
	{
		public GroupFeedbackForm Map(GroupFeedbackForm model = null)
		{
			if (model == null)
				model = new GroupFeedbackForm();

			model.Rating = Rating;
			model.Message = Message;

			return model;
		}

		[Required]
		public int Rating { get; set; }

		[StringLength(2000)]
		public string Message { get; set; }

	}
}
