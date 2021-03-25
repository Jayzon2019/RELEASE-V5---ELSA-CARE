using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLife.Store.Api.Messages
{
	public class PaymentStatusResponse
	{
		public Guid PayMentId { get; set; }
		public string PayMentMode { get; set; }
	}
}
