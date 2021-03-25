using System;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class GroupRequestSessionResponse : BaseGroupResponse
	{
		public GroupRequestSessionResponse() : base()
		{
		}

		public GroupRequestSessionResponse(GroupApplication model) : base(model)
		{
			Session = model.Session;
		}

		public string Session { get; set; }
	}
}
