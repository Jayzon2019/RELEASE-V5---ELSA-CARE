using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InLife.Store.Core.Models
{
	public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity
	{
		public DateTimeOffset TransactionDate { get; set; }

		[NotMapped]
		public virtual User TransactionBy { get; set; }

		[Column("TransactionBy")]
		public string _TransactionBy { get; set; }

		[NotMapped]
		public virtual UserRole TransactionByRole
		{
			get { return UserRole.FromId(_TransactionByRole); }
			set { _TransactionByRole = value.Id; }
		}

		[Column("TransactionByRole")]
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
