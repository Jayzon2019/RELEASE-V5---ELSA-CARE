using System;

namespace InLife.Store.Core.Models
{
	public class Address : Entity<int>
	{
		public string PhoneNumber { get; set; }

		public string Address1 { get; set; }

		public string Address2 { get; set; }

		public string Address3 { get; set; }

		public string City { get; set; }

		public string Region { get; set; }

		public string ZipCode { get; set; }

		public string Country { get; set; }
	}
}
