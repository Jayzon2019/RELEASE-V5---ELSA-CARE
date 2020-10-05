using System;
using System.Collections.Generic;

using InLife.Store.Core.Models.Enumerations;

namespace InLife.Store.Core.Models
{
	public class IdentityDocument : Entity<Guid>
	{
		public IdentityDocumentType Type { get; set; }

		public string Number { get; set; }

		public string Image { get; set; }
	}
}
