using System;

namespace InLife.Store.Core.Models
{
	public sealed class PrimeSecureApplicationStatus : Enumeration<string>
	{
		public static PrimeSecureApplicationStatus Quote       = new PrimeSecureApplicationStatus("Quote", "Quote");
		public static PrimeSecureApplicationStatus Application = new PrimeSecureApplicationStatus("Application", "Application");
		public static PrimeSecureApplicationStatus Payment     = new PrimeSecureApplicationStatus("Payment", "Payment");
		public static PrimeSecureApplicationStatus Complete    = new PrimeSecureApplicationStatus("Complete", "Complete");

		public PrimeSecureApplicationStatus() { }

		private PrimeSecureApplicationStatus(string id, string name) : base(id, name) { }

		public static PrimeSecureApplicationStatus FromId(string id)
		{
			return Enumeration<string>.FromId<PrimeSecureApplicationStatus>(id);
		}

		public static PrimeSecureApplicationStatus FromName(string name)
		{
			return Enumeration<string>.FromName<PrimeSecureApplicationStatus>(name);
		}
	}
}
