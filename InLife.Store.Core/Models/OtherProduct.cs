using System;

namespace InLife.Store.Core.Models
{
	public class OtherProduct : Entity<int>
	{
		public virtual Order Order { get; set; }

		public string  CompanyName { get; set; }

		public decimal BasicFaceAmount { get; set; }

		public decimal DreadDiseaseFaceAmount { get; set; }

		public decimal AccidentalFaceAmount { get; set; }

		public int     IssueYear { get; set; }
	}
}
