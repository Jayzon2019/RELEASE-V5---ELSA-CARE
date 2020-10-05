using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InLife.Store.Core.Models
{
	public class Transaction
	{
		public DateTimeOffset TransactionDate { get; set; }

		public virtual User TransactionBy { get; set; }
		public string _TransactionBy { get; set; }

		public virtual UserRole TransactionByRole
		{
			get { return UserRole.FromId(_TransactionByRole); }
			set { _TransactionByRole = value.Id; }
		}
		public string _TransactionByRole { get; set; }

		[MaxLength(256)]
		public string TransactionAgent { get; set; }

		[MaxLength(128)]
		public string TransactionRemoteAddress { get; set; }

		[MaxLength(256)]
		public string TransactionDetails { get; set; }

		[MaxLength(512)]
		public string TransactionUserNotes { get; set; }
	}
}
