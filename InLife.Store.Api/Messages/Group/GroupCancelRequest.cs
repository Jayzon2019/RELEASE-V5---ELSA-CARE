using System;
using System.ComponentModel.DataAnnotations;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class GroupCancelRequest
	{
		public GroupCancelForm Map(GroupCancelForm model = null)
		{
			if (model == null)
				model = new GroupCancelForm();

			model.Reason = Reason;
			model.Comments = Comments;

			return model;
		}

		[Required]
		[StringLength(2000)]
		public string Reason { get; set; }

		[Required]
		[StringLength(2000)]
		public string Comments { get; set; }

	}
}
