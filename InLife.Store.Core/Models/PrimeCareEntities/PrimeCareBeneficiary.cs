using System;

namespace InLife.Store.Core.Models
{
	public class PrimeCareBeneficiary : Entity<Guid>
	{
		//public Guid ApplicationId { get; set; }
		public virtual PrimeCareApplication Application { get; set; }

		//public Guid PersonId { get; set; }
		public virtual PrimeCarePerson Person { get; set; }

		public int Priority { get; set; }
		public string Relationsip { get; set; }
		public string Designation { get; set; }
	}
}
