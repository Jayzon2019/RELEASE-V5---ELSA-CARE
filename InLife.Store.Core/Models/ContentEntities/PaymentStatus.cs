using System;
using System.Collections.Generic;
using System.Text;

namespace InLife.Store.Core.Models.ContentEntities
{
	public class PaymentStatus: BaseContentEntity
	{
		public Guid PayMentId { get; set; }
		public string PayMentMode { get; set; }
	}
}
