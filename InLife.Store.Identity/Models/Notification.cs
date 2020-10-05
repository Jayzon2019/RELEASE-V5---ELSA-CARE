using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLife.Store.Identity.Models
{
	public class Message
	{
		public string Body { get; set; }

		public string Subject { get; set; }

		public string Recipient { get; set; }

		public string Sender { get; set; }

	}
}
