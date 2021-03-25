using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class Feedback : BaseContentEntity
	{
		public string RefId { get; set; }
		public string FeedbackType { get; set; }
		public string Comment { get; set; }
		
	}
}
