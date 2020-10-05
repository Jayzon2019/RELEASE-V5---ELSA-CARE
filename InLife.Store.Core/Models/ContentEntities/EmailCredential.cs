using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class EmailCredential : Entity<int>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Smtp { get; set; }
		public int? Port { get; set; }
		public bool? IsBodyHtml { get; set; }
		public bool? EnableSsl { get; set; }
	}
}
