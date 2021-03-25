using System;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class GroupQuoteResponse : BaseGroupResponse
	{
		public GroupQuoteResponse() : base()
		{
		}

		public GroupQuoteResponse(GroupApplication model) : base(model)
		{
			Session = model.Session;
		}

		public string Session { get; set; }
	}
}
