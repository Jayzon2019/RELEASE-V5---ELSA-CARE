using System;

namespace InLife.Store.Core.Models
{
	public sealed class PrimeCareApplicationStatus : Enumeration<string>
	{
		public static PrimeCareApplicationStatus Quote       = new PrimeCareApplicationStatus("Quote",       "Quote");
		public static PrimeCareApplicationStatus Application = new PrimeCareApplicationStatus("Application", "Application");
		public static PrimeCareApplicationStatus Payment     = new PrimeCareApplicationStatus("Payment",     "Payment");
		public static PrimeCareApplicationStatus Complete    = new PrimeCareApplicationStatus("Complete",    "Complete");

		public PrimeCareApplicationStatus() { }

		private PrimeCareApplicationStatus(string id, string name) : base(id, name) { }

		public static PrimeCareApplicationStatus FromId(string id)
		{
			return Enumeration<string>.FromId<PrimeCareApplicationStatus>(id);
		}

		public static PrimeCareApplicationStatus FromName(string name)
		{
			return Enumeration<string>.FromName<PrimeCareApplicationStatus>(name);
		}
	}
}
