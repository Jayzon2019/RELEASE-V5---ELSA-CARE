using System;

namespace InLife.Store.Core.Models
{
	public class PrimeCareOtherInsurance : Entity<Guid>
	{
		//public virtual PrimeCareApplication Application { get; set; }

		public string  CompanyName { get; set; }

		public decimal FaceAmount { get; set; }

		public decimal DiseaseFaceAmount { get; set; }

		public decimal AccidentalFaceAmount { get; set; }

		public int IssueYear { get; set; }
	}
}
